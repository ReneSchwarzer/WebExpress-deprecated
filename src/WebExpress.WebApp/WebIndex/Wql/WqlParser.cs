using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using WebExpress.WebApp.WebIndex.Wql.Condition;
using WebExpress.WebApp.WebIndex.Wql.Function;

namespace WebExpress.WebApp.WebIndex.Wql
{
    /// <summary>
    /// The Parser class is a class that implements a parser for the WQL query language. The parser reads an 
    /// input string that contains a WQL query and returns a WQL object that represents the structure of the query.
    /// To use the parser, call the Parse method with the string to be parsed to get a WQL object. This object 
    /// contains the structure of the WQL query and can be used to evaluate or process the query.
    /// The parser implements the following bnf:
    /// <code>
    /// WQL                  ::= Filter Order Partitioning | ε
    /// Filter               ::= "(" Filter ")" | Filter LogicalOperator Filter | Condition | ε
    /// Condition            ::= Attribute BinaryOperator Parameter | Attribute SetOperator "(" Parameter ParameterNext ")"
    /// LogicalOperator      ::= "and" | "or"
    /// Attribute            ::= Name
    /// Function             ::= Name "(" Parameter ParameterNext ")" | Name "(" ")"
    /// Parameter            ::= Function | DoubleValue | """ StringValue """ | "'" StringValue "'"  | StringValue
    /// ParameterNext        ::= "," Parameter ParameterNext | ε
    /// BinaryOperator       ::= "=" | ">" | "<![CDATA[<]]>" | ">=" | "<![CDATA[<=]]>" | "!=" | "~" | "is" | "is not" | "was"
    /// SetOperator          ::= "in" | "not in" | "was in"
    /// Order                ::= "order" "by" Attribute DescendingOrder OrderNext | ε
    /// OrderNext            ::= "," Attribute DescendingOrder OrderNext | ε
    /// DescendingOrder      ::= "asc" | "desc" | ε
    /// Partitioning         ::= Partitioning Partitioning | PartitioningOperator Number | ε
    /// PartitioningOperator ::= "take" | "skip"
    /// Name                 ::= [A-Za-z_.][A-Za-z0-9_.]+
    /// StringValue          ::= [A-Za-z0-9_@<>=~$%/!+.,;:\-]+
    /// DoubleValue          ::= [+-]?[0-9]*[.]?[0-9]+
    /// Number               ::= [0-9]+
    /// </code>
    /// </summary>
    public class WqlParser<T> : IWqlParser<T> where T : IIndexItem
    {
        private static readonly Regex NumberRegex = new Regex("^[0-9]+$", RegexOptions.Compiled);
        private static readonly Regex DoubleRegex = new Regex("^[+-]?[0-9]*[.]?[0-9]+$", RegexOptions.Compiled);

        /// <summary>
        /// Returns an enumeration of the conditions.
        /// </summary>
        private IDictionary<string, Type> Conditions { get; set; } = new SortedDictionary<string, Type>(new WqlParserLengthComparer());

        /// <summary>
        /// Returns an enumeration of the functions.
        /// </summary>
        private IDictionary<string, Type> Functions { get; set; } = new SortedDictionary<string, Type>(new WqlParserLengthComparer());

        /// <summary>
        /// Returns the culture in which to run the wql.
        /// </summary>
        public CultureInfo Culture { get; set; } = CultureInfo.InvariantCulture;

        /// <summary>
        /// Returns an enumeration of the existing attributes.
        /// </summary>
        protected IEnumerable<string> Attributes { get; private set; }

        /// <summary>
        /// Returns the index document.
        /// </summary>
        protected IIndexDocument<T> IndexDocument { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        internal WqlParser()
        {
            Attributes = typeof(T).GetProperties().Select(x => x.Name);

            RegisterCondition<WqlExpressionNodeFilterConditionBinaryEqual<T>>();
            RegisterCondition<WqlExpressionNodeFilterConditionBinaryLike<T>>();
            RegisterCondition<WqlExpressionNodeFilterConditionBinaryGreaterThan<T>>();
            RegisterCondition<WqlExpressionNodeFilterConditionBinaryGreaterThanOrEqual<T>>();
            RegisterCondition<WqlExpressionNodeFilterConditionBinaryLessThan<T>>();
            RegisterCondition<WqlExpressionNodeFilterConditionBinaryLessThanOrEqual<T>>();
            RegisterCondition<WqlExpressionNodeFilterConditionSetIn<T>>();
            RegisterCondition<WqlExpressionNodeFilterConditionSetNotIn<T>>();

            RegisterFunction<WqlExpressionNodeFilterFunctionDay<T>>();
            RegisterFunction<WqlExpressionNodeFilterFunctionNow<T>>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="indexFiled">The index field.</param>
        internal WqlParser(IIndexDocument<T> indexFiled)
            : this()
        {
            IndexDocument = indexFiled;
            Attributes = indexFiled.Fields;
        }

        /// <summary>
        /// Parses a given wql query.
        /// </summary>
        /// <param name="input">An input string that contains a wql query.</param>
        /// <param name="culture">The culture in which to run the wql.</param>
        /// <returns>A wql object that represents the structure of the query.</returns>
        public IWqlStatement<T> Parse(string input)
        {
            var tokens = Tokenize(input);
            var wql = new WqlStatement<T>(input)
            {
                Culture = Culture,
                IndexDocument = IndexDocument
            };

            if (string.IsNullOrWhiteSpace(input))
            {
                return wql;
            }

            try
            {
                wql.Filter = ParseFilter(tokens);
                wql.Order = ParseOrder(tokens);
                wql.Partitioning = ParsePartitioning(tokens);

                if (tokens.Any())
                {
                    throw new WqlParseException
                    (
                        "webexpress.webapp:wql.unexpected_token",
                        PeekToken(tokens)
                    );
                }
            }
            catch (WqlParseException ex)
            {
                wql.Error = new WqlExpressionError()
                {
                    Culture = Culture,
                    Message = ex.Message,
                    Position = ex.Token?.Offset ?? 0,
                    Length = ex.Token?.Length ?? 0
                };
            }

            return wql;
        }

        /// <summary>
        /// Parses the filter expression.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <returns>The filter.</returns>
        private WqlExpressionNodeFilter<T> ParseFilter(Queue<WqlToken> tokenQueue)
        {
            if (PeekToken(tokenQueue, "order") || PeekToken(tokenQueue, "orderby") || PeekToken(tokenQueue, "take") || PeekToken(tokenQueue, "skip"))
            {
                return null;
            }

            if (PeekToken(tokenQueue, "("))
            {
                ReadToken(tokenQueue, "(");
                var filter = ParseFilter(tokenQueue);
                ReadToken(tokenQueue, ")");

                if (PeekToken(tokenQueue, "and") || PeekToken(tokenQueue, "or"))
                {
                    var logicalOperator = ParseLogicalOperator(tokenQueue);

                    return new WqlExpressionNodeFilterBinary<T>
                    {
                        LeftFilter = filter,
                        LogicalOperator = logicalOperator,
                        RightFilter = ParseFilter(tokenQueue)
                    };
                }

                return filter;
            }

            var condition = ParseCondition(tokenQueue);

            if (condition != null)
            {
                if (PeekToken(tokenQueue, "and") || PeekToken(tokenQueue, "or"))
                {
                    var logicalOperator = ParseLogicalOperator(tokenQueue);

                    return new WqlExpressionNodeFilterBinary<T>
                    {
                        LeftFilter = new WqlExpressionNodeFilter<T> { Condition = condition },
                        LogicalOperator = logicalOperator,
                        RightFilter = ParseFilter(tokenQueue)
                    };
                }

                return new WqlExpressionNodeFilter<T>
                {
                    Condition = condition
                };
            }

            var leftFilter = ParseFilter(tokenQueue);
            if (leftFilter != null)
            {
                var logicalOperator = ParseLogicalOperator(tokenQueue);
                var rightFilter = ParseFilter(tokenQueue);

                return new WqlExpressionNodeFilterBinary<T>
                {
                    LeftFilter = leftFilter,
                    LogicalOperator = logicalOperator,
                    RightFilter = rightFilter
                };
            }

            return null;
        }

        /// <summary>
        /// Parses the condition expression.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <returns>The condition.</returns>
        private WqlExpressionNodeFilterCondition<T> ParseCondition(Queue<WqlToken> tokenQueue)
        {
            var attribute = ParseAttribute(tokenQueue);
            var conditionToken = PeekToken(tokenQueue);
            var condition = Conditions
                    .Where(x => PeekToken(tokenQueue, x.Key.Split(' ')))
                    .FirstOrDefault();

            try
            {
                var instance = Activator.CreateInstance(condition.Value) as WqlExpressionNodeFilterCondition<T>;

                ReadToken(tokenQueue, condition.Key.Split(' '));

                if (instance is WqlExpressionNodeFilterConditionBinary<T> binary)
                {
                    binary.Culture = Culture;
                    binary.Attribute = attribute;

                    binary.Parameter = ParseParameter(tokenQueue);

                    return binary;
                }
                else if (instance is WqlExpressionNodeFilterConditionSet<T> set)
                {
                    var parameters = new List<WqlExpressionNodeParameter<T>>();

                    ReadToken(tokenQueue, "(");
                    parameters.Add(ParseParameter(tokenQueue));

                    while (PeekToken(tokenQueue, ","))
                    {
                        ReadToken(tokenQueue, ",");
                        parameters.Add(ParseParameter(tokenQueue));
                    }

                    ReadToken(tokenQueue, ")");

                    set.Culture = Culture;
                    set.Attribute = attribute;
                    set.Parameters = parameters;

                    return set;
                }

                throw new WqlParseException
                (
                    "webexpress.webapp:wql.expected_binary_or_set_condition",
                    conditionToken
                );
            }
            catch (WqlParseException ex)
            {
                throw new WqlParseException
                (
                    ex.Message,
                    conditionToken
                );
            }
            catch (Exception)
            {
                throw new WqlParseException
                (
                    "webexpress.webapp:wql.condition_unknown",
                    conditionToken
                );
            }
        }

        /// <summary>
        /// Parses the logical operator expression.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <returns>The logical operator.</returns>
        private WqlExpressionLogicalOperator ParseLogicalOperator(Queue<WqlToken> tokenQueue)
        {
            var logicalOperatorToken = PeekToken(tokenQueue);

            if (logicalOperatorToken?.Value?.ToLower() == "and")
            {
                ReadToken(tokenQueue, "and");

                return WqlExpressionLogicalOperator.And;
            }
            else if (logicalOperatorToken?.Value.ToLower() == "or")
            {
                ReadToken(tokenQueue, "or");

                return WqlExpressionLogicalOperator.Or;
            }

            throw new WqlParseException
            (
                "webexpress.webapp:wql.expected_logicaloperator",
                logicalOperatorToken
            );
        }

        /// <summary>
        /// Parses the attribute expression.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <returns>The attribute.</returns>
        private WqlExpressionNodeAttribute<T> ParseAttribute(Queue<WqlToken> tokenQueue)
        {
            var attributeToken = PeekToken(tokenQueue);
            var attribute = Attributes
                .Where(x => x.Equals(attributeToken?.Value, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            ReadToken(tokenQueue);

            if (attribute != null)
            {
                return new WqlExpressionNodeAttribute<T>
                {
                    Name = attribute,
                    Property = typeof(T).GetProperty(attribute),
                    ReverseIndex = IndexDocument.GetReverseIndex(attribute)
                };
            }

            throw new WqlParseException
            (
                "webExpress.webapp:wql.attribute_unknown",
                attributeToken
            );
        }

        /// <summary>
        /// Parses the function expression.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <returns>The function.</returns>
        private WqlExpressionNodeFilterFunction<T> ParseFunction(Queue<WqlToken> tokenQueue)
        {
            var parameters = new List<WqlExpressionNodeParameter<T>>();
            var function = Functions
                    .Where(x => PeekToken(tokenQueue, x.Key))
                    .FirstOrDefault();
            var name = ReadToken(tokenQueue);

            try
            {
                var instance = Activator.CreateInstance(function.Value) as WqlExpressionNodeFilterFunction<T>;

                ReadToken(tokenQueue, "(");

                if (PeekToken(tokenQueue, ")"))
                {
                    ReadToken(tokenQueue, ")");
                }
                else
                {
                    parameters.Add(ParseParameter(tokenQueue));

                    while (PeekToken(tokenQueue, ","))
                    {
                        ReadToken(tokenQueue, ",");
                        parameters.Add(ParseParameter(tokenQueue));
                    }

                    ReadToken(tokenQueue, ")");
                }

                instance.Parameters = parameters;

                return instance;
            }
            catch (Exception)
            {
                throw new WqlParseException
                (
                    "webexpress.webapp:wql.function_unknown",
                    name
                );
            }
        }

        /// <summary>
        /// Parses the parameter expression.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <param name="culture">The culture in which to run the wql.</param>
        /// <returns>The parameter.</returns>
        private WqlExpressionNodeParameter<T> ParseParameter(Queue<WqlToken> tokenQueue)
        {
            var functionOrValueToken = PeekToken(tokenQueue);
            var function = Functions
                    .Where(x => PeekToken(tokenQueue, x.Key))
                    .FirstOrDefault();

            if (PeekToken(tokenQueue, function.Key ?? functionOrValueToken?.Value, "("))
            {
                return new WqlExpressionNodeParameter<T>
                {
                    Function = ParseFunction(tokenQueue)
                };
            }
            else if (PeekToken(tokenQueue, DoubleRegex))
            {
                return new WqlExpressionNodeParameter<T>
                {
                    Value = new WqlExpressionNodeValue<T>()
                    {
                        Culture = Culture,
                        NumberValue = ParseDoubleValue(tokenQueue)
                    }
                };
            }
            else if (functionOrValueToken?.Value == "\"")
            {
                ReadToken(tokenQueue);
                var parameter = new WqlExpressionNodeParameter<T>
                {
                    Value = new WqlExpressionNodeValue<T>()
                    {
                        Culture = Culture,
                        StringValue = ParseStringValue(tokenQueue)
                    }
                };

                if (!PeekToken(tokenQueue, "\""))
                {
                    throw new WqlParseException
                    (
                        "webexpress.webapp:wql.expected_terminated_string_token",
                        functionOrValueToken
                    );
                }

                ReadToken(tokenQueue);

                return parameter;
            }
            else if (functionOrValueToken?.Value == "'")
            {
                ReadToken(tokenQueue);
                var parameter = new WqlExpressionNodeParameter<T>
                {
                    Value = new WqlExpressionNodeValue<T>()
                    {
                        Culture = Culture,
                        StringValue = ParseStringValue(tokenQueue)
                    }
                };

                if (!PeekToken(tokenQueue, "'"))
                {
                    throw new WqlParseException
                    (
                        "webexpress.webapp:wql.expected_terminated_string_token",
                        functionOrValueToken
                    );
                }

                ReadToken(tokenQueue);

                return parameter;
            }
            else
            {
                return new WqlExpressionNodeParameter<T>
                {
                    Value = new WqlExpressionNodeValue<T>()
                    {
                        Culture = Culture,
                        StringValue = ParseStringValue(tokenQueue)
                    }
                };
            }

            throw new WqlParseException
            (
                "webexpress.webapp:wql.expected_function_or_value",
                functionOrValueToken
            );
        }

        /// <summary>
        /// Parses the order expression.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <returns>The order.</returns>
        private WqlExpressionNodeOrder<T> ParseOrder(Queue<WqlToken> tokenQueue)
        {
            if (PeekToken(tokenQueue, "order", "by"))
            {
                var attributes = new List<WqlExpressionNodeOrderAttribute<T>>();
                var i = 0;

                ReadToken(tokenQueue, "order");
                ReadToken(tokenQueue, "by");
                attributes.Add(ParseOrderAttribute(tokenQueue, i++));

                while (PeekToken(tokenQueue, ","))
                {
                    ReadToken(tokenQueue, ",");
                    attributes.Add(ParseOrderAttribute(tokenQueue, i++));
                }

                return new WqlExpressionNodeOrder<T> { Attributes = attributes };
            }
            else if (PeekToken(tokenQueue, "orderby"))
            {
                var attributes = new List<WqlExpressionNodeOrderAttribute<T>>();
                var i = 0;

                ReadToken(tokenQueue, "orderby");
                attributes.Add(ParseOrderAttribute(tokenQueue, i++));

                while (PeekToken(tokenQueue, ","))
                {
                    ReadToken(tokenQueue, ",");
                    attributes.Add(ParseOrderAttribute(tokenQueue, i++));
                }

                return new WqlExpressionNodeOrder<T> { Attributes = attributes };
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Parses the order attribute expression.
        /// </summary>
        /// <param name="tokenQueue">The tokens.</param>
        /// <param name="position">The position of the attribute within the order by statement.</param>
        /// <returns>The order attribute.</returns>
        private WqlExpressionNodeOrderAttribute<T> ParseOrderAttribute(Queue<WqlToken> tokenQueue, int position)
        {
            var attribute = ParseAttribute(tokenQueue);
            var descending = ParseDescendingOrder(tokenQueue);

            return new WqlExpressionNodeOrderAttribute<T>
            {
                Attribute = attribute,
                Descending = descending,
                Position = position
            };
        }

        /// <summary>
        /// Parses the descending order expression.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <returns>The descending order.</returns>
        private bool ParseDescendingOrder(Queue<WqlToken> tokenQueue)
        {
            if (PeekToken(tokenQueue, "asc"))
            {
                ReadToken(tokenQueue, "asc");
                return false;
            }
            else if (PeekToken(tokenQueue, "desc"))
            {
                ReadToken(tokenQueue, "desc");
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Parses the partitioning expression.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <returns>The partitioning.</returns>
        private WqlExpressionNodePartitioning<T> ParsePartitioning(Queue<WqlToken> tokenQueue)
        {
            var function = new List<WqlExpressionNodePartitioningFunction<T>>();

            if (!PeekToken(tokenQueue, "take") && !PeekToken(tokenQueue, "skip"))
            {
                return null;
            }

            while (PeekToken(tokenQueue, "take") || PeekToken(tokenQueue, "skip"))
            {
                var op = ParsePartitioningOperator(tokenQueue);
                var number = ParseNumber(tokenQueue);

                function.Add(new WqlExpressionNodePartitioningFunction<T>()
                {
                    Operator = op,
                    Value = number
                });
            }

            return new WqlExpressionNodePartitioning<T>()
            {
                PartitioningFunctions = function
            };
        }

        /// <summary>
        /// Parses the partitioning operator expression.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <returns>The partitioning operator.</returns>
        private WqlExpressionNodePartitioningOperator ParsePartitioningOperator(Queue<WqlToken> tokenQueue)
        {
            var partitioningOperatorToken = PeekToken(tokenQueue);

            if (partitioningOperatorToken?.Value == "take")
            {
                ReadToken(tokenQueue, "take");
                return WqlExpressionNodePartitioningOperator.Take;
            }
            else if (partitioningOperatorToken?.Value == "skip")
            {
                ReadToken(tokenQueue, "skip");
                return WqlExpressionNodePartitioningOperator.Skip;
            }

            throw new WqlParseException
            (
                "webexpress.webapp:wql.expected_skip_or_take",
                partitioningOperatorToken
            );
        }

        /// <summary>
        /// Parses the value expression.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <returns>The value.</returns>
        private string ParseStringValue(Queue<WqlToken> tokenQueue)
        {
            var valueToken = ReadToken(tokenQueue);

            return valueToken.Value;
        }

        /// <summary>
        /// Parses the number expression.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <returns>The number.</returns>
        private double ParseDoubleValue(Queue<WqlToken> tokenQueue)
        {
            var token = ReadToken(tokenQueue, DoubleRegex);

            try
            {
                return Convert.ToDouble(token.Value, Culture);
            }
            catch (Exception)
            {
                throw new WqlParseException
                (
                    "webexpress.webapp:wql.parse.exceptionr",
                    token
                );
            }
        }

        /// <summary>
        /// Parses the number expression.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <returns>The number.</returns>
        private int ParseNumber(Queue<WqlToken> tokenQueue)
        {
            var token = ReadToken(tokenQueue, NumberRegex);

            return int.Parse(token?.Value);
        }

        /// <summary>
        /// Breaks the input string into tokens.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>The token queue.</returns>
        private Queue<WqlToken> Tokenize(string input)
        {
            var tokens = new Queue<WqlToken>();
            var currentToken = new WqlToken();

            for (int i = 0; i < input.Length; i++)
            {
                var c = input[i];

                if (char.IsWhiteSpace(c))
                {
                    if (!currentToken.IsEmpty())
                    {
                        tokens.Enqueue(currentToken);

                        while (i < input.Length - 1 && input[i + 1] == ' ')
                        {
                            i++;
                        }
                    }

                    currentToken = new WqlToken() { Offset = i + 1 };
                }
                else if (c == ',' || c == '(' || c == ')')
                {
                    if (!currentToken.IsEmpty())
                    {
                        tokens.Enqueue(currentToken);
                    }

                    tokens.Enqueue(new WqlToken() { Value = c.ToString(), Offset = i });
                    currentToken = new WqlToken() { Offset = i + 1 };
                }
                else if (c == '=' || c == '~' || c == '<' || c == '>' || c == '!' || c == '%')
                {
                    if (!currentToken.IsEmpty())
                    {
                        var lastCharacter = currentToken.Value.LastOrDefault();

                        if (!(lastCharacter == '=' ||
                            lastCharacter == '~' ||
                            lastCharacter == '<' ||
                            lastCharacter == '>' ||
                            lastCharacter == '!' ||
                            lastCharacter == '%'))
                        {
                            tokens.Enqueue(currentToken);

                            currentToken = new WqlToken() { Offset = i + 1 };
                        }
                    }

                    currentToken.Append(c);
                }
                else if (c == '"' || c == '\'')
                {
                    var startChar = c;
                    i++;

                    if (!currentToken.IsEmpty())
                    {
                        tokens.Enqueue(currentToken);
                        currentToken = new WqlToken() { Offset = i + 1 };
                    }

                    currentToken.Append(c);
                    tokens.Enqueue(currentToken);
                    currentToken = new WqlToken() { Offset = i + 1 };

                    while (i < input.Length && input[i] != startChar)
                    {
                        currentToken.Append(input[i]);
                        i++;
                    }

                    if (i < input.Length)
                    {
                        tokens.Enqueue(currentToken);
                        currentToken = new WqlToken() { Offset = i + 1 };

                        currentToken.Append(input[i]);
                        tokens.Enqueue(currentToken);
                        currentToken = new WqlToken() { Offset = i + 1 };
                    }
                    else
                    {
                        throw new WqlParseException
                        (
                            "webexpress.webapp:wql.unterminated_string",
                            new WqlToken() { Value = input.Substring(i), Offset = i }
                        );
                    }
                }
                else
                {
                    currentToken.Append(c);
                }
            }

            if (!currentToken.IsEmpty())
            {
                tokens.Enqueue(currentToken);
            }

            return tokens;
        }

        /// <summary>
        /// Checks the current token.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <param name="currentToken">The token to check.</param>
        /// <returns>True if the token is the current one, false otherwise.</returns>
        private bool PeekToken(Queue<WqlToken> tokenQueue, string currentToken)
        {
            return tokenQueue.Count > 0 && tokenQueue.Peek()?.Value?.ToLower() == currentToken?.ToLower();
        }

        /// <summary>
        /// Checks the next n token.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <param name="tokens">The tokens to check.</param>
        /// <returns>True if the token is the current and next one, false otherwise.</returns>
        private bool PeekToken(Queue<WqlToken> tokenQueue, params string[] tokens)
        {
            var elements = tokenQueue.Take(tokens.Count());

            if (elements.Count() != tokens.Count())
            {
                return false;
            }

            return !elements.Select(x => x.Value).Except(tokens).Any();
        }

        /// <summary>
        /// Checks the next token.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <param name="regex">The token to check.</param>
        /// <returns>True if the token is the current one, false otherwise.</returns>
        private bool PeekToken(Queue<WqlToken> tokenQueue, Regex regex)
        {
            return tokenQueue.Count > 0 && regex.IsMatch(tokenQueue.Peek()?.Value?.ToLower());
        }

        /// <summary>
        /// Returns the next token without consuming it.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <returns>The next token or null.</returns>
        private WqlToken PeekToken(Queue<WqlToken> tokenQueue)
        {
            return tokenQueue.Any() ? tokenQueue.Peek() : null;
        }

        /// <summary>
        /// Consumes the current token.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <returns>The value of the token.</returns>
        private WqlToken ReadToken(Queue<WqlToken> tokenQueue)
        {
            return tokenQueue.Any() ? tokenQueue.Dequeue() : null;
        }

        /// <summary>
        /// Consumes the current token.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <param name="token">The token to be consumed.</param>
        /// <returns>The value of the token.</returns>
        private WqlToken ReadToken(Queue<WqlToken> tokenQueue, string token)
        {
            if (PeekToken(tokenQueue, token))
            {
                return tokenQueue.Dequeue();
            }

            throw new WqlParseException
            (
                "webexpress.webapp:wql.expected_token",
                PeekToken(tokenQueue)
            );
        }

        /// <summary>
        /// Consumes the next n token.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <param name="tokens">The tokens to be consumed.</param>
        private void ReadToken(Queue<WqlToken> tokenQueue, params string[] tokens)
        {
            foreach (var token in tokens)
            {
                if (PeekToken(tokenQueue, token))
                {
                    tokenQueue.Dequeue();
                }
                else
                {
                    throw new WqlParseException
                    (
                         "webexpress.webapp:wql.expected_token",
                         PeekToken(tokenQueue)
                    );
                }
            }
        }

        /// <summary>
        /// Consumes the current token.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <param name="regex">The token to be consumed.</param>
        /// <returns>The value of the token.</returns>
        private WqlToken ReadToken(Queue<WqlToken> tokenQueue, Regex regex)
        {
            if (PeekToken(tokenQueue, regex))
            {
                return tokenQueue.Dequeue();
            }

            throw new WqlParseException
            (
                "webexpress.webapp:wql.expected_token_matching",
                PeekToken(tokenQueue)
            );
        }

        /// <summary>
        /// Registers a condition expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="WqlParseException"></exception>
        public void RegisterCondition<C>() where C : WqlExpressionNodeFilterCondition<T>, new()
        {
            var op = new C().Operator;

            if (!Conditions.ContainsKey(op))
            {
                Conditions.Add(op, typeof(C));

                return;
            }

            throw new Exception($"Condition '{op}' cannot be registered because it already exists.");
        }

        /// <summary>
        /// Registers a function expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="WqlParseException"></exception>
        public void RegisterFunction<F>() where F : WqlExpressionNodeFilterFunction<T>, new()
        {
            var name = new F().Name?.ToLower();

            if (!Functions.ContainsKey(name))
            {
                Functions.Add(name, typeof(T));

                return;
            }

            throw new Exception($"Function '{name}' cannot be registered because it already exists.");
        }

        /// <summary>
        /// Removes a condition expression.
        /// </summary>
        /// <param name="op">The operator to be derisgistrated.</param>
        public void RemoveCondition(string op)
        {
            if (Conditions.ContainsKey(op))
            {
                Conditions.Remove(op);

                return;
            }
        }

        /// <summary>
        /// Removes a function expression.
        /// </summary>
        /// <param name="name">The function name to be derisgistrated.</param>
        public void RemoveFunction(string name)
        {
            if (Functions.ContainsKey(name))
            {
                Functions.Remove(name);

                return;
            }
        }
    }
}
