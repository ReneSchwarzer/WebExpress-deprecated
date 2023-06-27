using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WebExpress.WebApp.Wql.Condition;
using WebExpress.WebApp.Wql.Function;

namespace WebExpress.WebApp.Wql
{
    /// <summary>
    /// The Parser class is a class that implements a parser for the WQL query language. The parser reads an 
    /// input string that contains a WQL query and returns a WQL object that represents the structure of the query.
    /// To use the parser, call the Parse method with the string to be parsed to get a WQL object. This object 
    /// contains the structure of the WQL query and can be used to evaluate or process the query.
    /// The parser implements the following BNF:
    /// <code>
    /// WQL                  ::= Filter Order Partitioning | ε
    /// Filter               ::= "(" Filter ")" | Filter LogicalOperator Filter | Condition | ε
    /// Condition            ::= Attribute Operator Value | Attribute Operator "(" Parameter ParameterNext ")" | Attribute Operator Function
    /// LogicalOperator      ::= "and" | "or"
    /// Attribute            ::= Name
    /// Function             ::= Name "(" Parameter ParameterNext ")"
    /// Parameter            ::= Value | Function
    /// ParameterNext        ::= "," Parameter ParameterNext | ε
    /// BinaryOperator       ::= "=" | ">" | "<![CDATA[<]]>" | ">=" | "<![CDATA[<=]]>" | "!=" | "~" | "is" | "is not" | "was"
    /// SetOperator          ::= "in" | "not in" | "was in"
    /// Order                ::= "order" "by" Attribute DescendingOrder OrderNext | ε
    /// OrderNext            ::= "," Attribute DescendingOrder OrderNext | ε
    /// DescendingOrder      ::= "asc" | "desc" | ε
    /// Partitioning         ::= Partitioning Partitioning | PartitioningOperator Number | ε
    /// PartitioningOperator ::= "take" | "skip"
    /// Name                 ::= [A-Za-z_] [A-Za-z0-9_]+
    /// Value                ::= """ [A-Za-z0-9_@<>=~$%/!.,;:\-]+ """ | "'" [A-Za-z0-9_@<>=~$%/!.,;:\-]+ "'" | Number
    /// Number               ::= [0-9]+
    /// </code>
    /// </summary>
    public static class WqlParser
    {
        private static readonly Regex NameRegex = new Regex("^[A-Za-z_][A-Za-z0-9_]*$", RegexOptions.Compiled);
        private static readonly Regex NumberRegex = new Regex("^[0-9]+$", RegexOptions.Compiled);
        private static readonly Regex ValueRegex = new Regex("^\"[A-Za-z0-9_@<>=~$%/!.,;:\\\\-]+\"|'[A-Za-z0-9_@<>=~$%/!.,;:\\\\-]+'$", RegexOptions.Compiled);

        /// <summary>
        /// Returns an enumeration of the conditions.
        /// </summary>
        private static IDictionary<string, Type> Conditions { get; } = new Dictionary<string, Type>();

        /// <summary>
        /// Parses a given WQL query.
        /// </summary>
        /// <param name="input">An input string that contains a WQL query.</param>
        /// <returns>A WQL object that represents the structure of the query.</returns>
        public static WqlStatement Parse(string input)
        {
            var tokens = Tokenize(input);
            var wql = new WqlStatement();

            if (string.IsNullOrWhiteSpace(input))
            {
                return wql;
            }

            wql.Filter = ParseFilter(tokens);
            wql.Order = ParseOrder(tokens);
            wql.Partitioning = ParsePartitioning(tokens);

            return wql;
        }

        /// <summary>
        /// Parses the filter expression.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <returns>The filter.</returns>
        private static IWqlExpressionFilter ParseFilter(Queue<string> tokenQueue)
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

                    return new WqlExpressionFilterBinary
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

                    return new WqlExpressionFilterBinary
                    {
                        LeftFilter = new WqlExpressionFilter { Condition = condition },
                        LogicalOperator = logicalOperator,
                        RightFilter = ParseFilter(tokenQueue)
                    };
                }

                return new WqlExpressionFilter
                {
                    Condition = condition
                };
            }

            var leftFilter = ParseFilter(tokenQueue);
            if (leftFilter != null)
            {
                var logicalOperator = ParseLogicalOperator(tokenQueue);
                var rightFilter = ParseFilter(tokenQueue);

                return new WqlExpressionFilterBinary
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
        private static IWqlExpressionCondition ParseCondition(Queue<string> tokenQueue)
        {
            if (PeekToken(tokenQueue, NameRegex))
            {
                var attribute = ParseAttribute(tokenQueue);

                var condition = Conditions
                    .Where(x => PeekToken(tokenQueue, x.Key.Split(' ')))
                    .FirstOrDefault();

                if (condition.Value != null)
                {
                    var instance = Activator.CreateInstance(condition.Value) as IWqlExpressionCondition;

                    if (instance is WqlExpressionConditionBinary binary)
                    {
                        binary.Attribute = attribute;
                        ReadToken(tokenQueue, condition.Key.Split(' '));
                        binary.Parameter = ParseParameter(tokenQueue);

                        return binary;
                    }
                    else if (instance is WqlExpressionConditionSet set)
                    {
                        var parameters = new List<WqlExpressionParameter>();

                        ReadToken(tokenQueue, condition.Key.Split(' '));
                        ReadToken(tokenQueue, "(");
                        parameters.Add(ParseParameter(tokenQueue));

                        while (PeekToken(tokenQueue, ","))
                        {
                            ReadToken(tokenQueue, ",");
                            parameters.Add(ParseParameter(tokenQueue));
                        }

                        ReadToken(tokenQueue, ")");

                        set.Attribute = attribute;
                        set.Parameters = parameters;

                        return set;
                    }
                    else
                    {
                        throw new WqlParseException("Expected binary or set condition");
                    }
                }
                else
                {
                    throw new WqlParseException("Expected condition");
                }

                //if (IsBinaryOperator(tokenQueue))
                //{
                //    var op = ParseBinaryOperator(tokenQueue);
                //   

                //    return new WqlExpressionConditionBinary
                //    {
                //        Attribute = attribute,
                //        Operator = op,
                //        Parameter = parameter
                //    };
                //}
                //else if (IsSetOperator(tokenQueue))
                //{
                //    var parameters = new List<WqlExpressionParameter>();
                //    var op = ParseSetOperator(tokenQueue);

                //    ReadToken(tokenQueue, "(");
                //    parameters.Add(ParseParameter(tokenQueue));

                //    while (PeekToken(tokenQueue, ","))
                //    {
                //        ReadToken(tokenQueue, ",");
                //        parameters.Add(ParseParameter(tokenQueue));
                //    }

                //    ReadToken(tokenQueue, ")");

                //    return new WqlExpressionConditionSet
                //    {
                //        Attribute = attribute,
                //        Operator = op,
                //        Parameters = parameters
                //    };
                //}
            }

            return null;
        }

        /// <summary>
        /// Parses the logical operator expression.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <returns>The logical operator.</returns>
        private static WqlExpressionLogicalOperator ParseLogicalOperator(Queue<string> tokenQueue)
        {
            if (PeekToken(tokenQueue, "and"))
            {
                ReadToken(tokenQueue, "and");

                return WqlExpressionLogicalOperator.And;
            }
            else if (PeekToken(tokenQueue, "or"))
            {
                ReadToken(tokenQueue, "or");

                return WqlExpressionLogicalOperator.Or;
            }
            else
            {
                throw new WqlParseException("Expected 'and' or 'or'");
            }
        }

        /// <summary>
        /// Parses the attribute expression.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <returns>The attribute.</returns>
        private static WqlExpressionAttribute ParseAttribute(Queue<string> tokenQueue)
        {
            var name = ReadToken(tokenQueue, NameRegex);

            return new WqlExpressionAttribute { Name = name };
        }

        /// <summary>
        /// Parses the function expression.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <returns>The function.</returns>
        private static WqlExpressionFunction ParseFunction(Queue<string> tokenQueue)
        {
            var name = ReadToken(tokenQueue, NameRegex);
            var parameters = new List<WqlExpressionParameter>();

            ReadToken(tokenQueue, "(");
            parameters.Add(ParseParameter(tokenQueue));

            while (PeekToken(tokenQueue, ","))
            {
                ReadToken(tokenQueue, ",");
                parameters.Add(ParseParameter(tokenQueue));
            }

            ReadToken(tokenQueue, ")");

            return new WqlExpressionFunction { Name = name, Parameters = parameters };
        }

        /// <summary>
        /// Parses the parameter expression.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <returns>The parameter.</returns>
        private static WqlExpressionParameter ParseParameter(Queue<string> tokenQueue)
        {
            if (PeekToken(tokenQueue, NameRegex))
            {
                return new WqlExpressionParameter { Function = ParseFunction(tokenQueue) };
            }
            else
            {
                return new WqlExpressionParameter { Value = ParseValue(tokenQueue) };
            }
        }

        /// <summary>
        /// Parses the order expression.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <returns>The order.</returns>
        private static WqlExpressionOrder ParseOrder(Queue<string> tokenQueue)
        {
            if (PeekToken(tokenQueue, "order", "by"))
            {
                var attributes = new List<WqlExpressionOrderAttribute>();

                ReadToken(tokenQueue, "order");
                ReadToken(tokenQueue, "by");
                attributes.Add(ParseOrderAttribute(tokenQueue));

                while (PeekToken(tokenQueue, ","))
                {
                    ReadToken(tokenQueue, ",");
                    attributes.Add(ParseOrderAttribute(tokenQueue));
                }

                return new WqlExpressionOrder { Attributes = attributes };
            }
            else if (PeekToken(tokenQueue, "orderby"))
            {
                var attributes = new List<WqlExpressionOrderAttribute>();

                ReadToken(tokenQueue, "orderby");
                attributes.Add(ParseOrderAttribute(tokenQueue));

                while (PeekToken(tokenQueue, ","))
                {
                    ReadToken(tokenQueue, ",");
                    attributes.Add(ParseOrderAttribute(tokenQueue));
                }

                return new WqlExpressionOrder { Attributes = attributes };
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
        /// <returns>The order attribute.</returns>
        private static WqlExpressionOrderAttribute ParseOrderAttribute(Queue<string> tokenQueue)
        {
            var attribute = ParseAttribute(tokenQueue);
            var descending = ParseDescendingOrder(tokenQueue);

            return new WqlExpressionOrderAttribute { Attribute = attribute, Descending = descending };
        }

        /// <summary>
        /// Parses the descending order expression.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <returns>The descending order.</returns>
        private static bool ParseDescendingOrder(Queue<string> tokenQueue)
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
        private static WqlExpressionPartitioning ParsePartitioning(Queue<string> tokenQueue)
        {
            var function = new List<WqlExpressionPartitioningFunction>();

            if (!PeekToken(tokenQueue, "take") && !PeekToken(tokenQueue, "skip"))
            {
                return null;
            }

            while (PeekToken(tokenQueue, "take") || PeekToken(tokenQueue, "skip"))
            {
                var op = ParsePartitioningOperator(tokenQueue);
                var number = ParseNumber(tokenQueue);

                function.Add(new WqlExpressionPartitioningFunction()
                {
                    Operator = op,
                    Value = number
                });
            }

            return new WqlExpressionPartitioning()
            {
                PartitioningFunctions = function
            };
        }

        /// <summary>
        /// Parses the partitioning operator expression.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <returns>The partitioning operator.</returns>
        private static WqlExpressionPartitioningOperator ParsePartitioningOperator(Queue<string> tokenQueue)
        {
            if (PeekToken(tokenQueue, "take"))
            {
                ReadToken(tokenQueue, "take");
                return WqlExpressionPartitioningOperator.Take;
            }
            else if (PeekToken(tokenQueue, "skip"))
            {
                ReadToken(tokenQueue, "skip");
                return WqlExpressionPartitioningOperator.Skip;
            }
            else
            {
                throw new WqlParseException("Expected 'take' or 'skip'");
            }
        }

        /// <summary>
        /// Parses the value expression.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <returns>The value.</returns>
        private static WqlExpressionValue ParseValue(Queue<string> tokenQueue)
        {
            if (PeekToken(tokenQueue, ValueRegex))
            {
                var token = ReadToken(tokenQueue, ValueRegex);
                return new WqlExpressionValue { StringValue = token.Substring(1, token.Length - 2) };
            }
            else
            {
                var number = ParseNumber(tokenQueue);
                return new WqlExpressionValue { NumberValue = number };
            }
        }

        /// <summary>
        /// Parses the number expression.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <returns>The number.</returns>
        private static int ParseNumber(Queue<string> tokenQueue)
        {
            var token = ReadToken(tokenQueue, NumberRegex);

            return int.Parse(token);
        }

        /// <summary>
        /// Breaks the input string into tokens.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>The token queue.</returns>
        private static Queue<string> Tokenize(string input)
        {
            var tokens = new Queue<string>();
            var currentToken = "";

            for (int i = 0; i < input.Length; i++)
            {
                var c = input[i];
                if (char.IsWhiteSpace(c))
                {
                    if (currentToken.Length > 0)
                    {
                        tokens.Enqueue(currentToken);
                        currentToken = "";
                    }
                }
                else if (c == ',' || c == '(' || c == ')' || c == '=')
                {
                    if (currentToken.Length > 0)
                    {
                        tokens.Enqueue(currentToken);
                        currentToken = "";
                    }
                    tokens.Enqueue(c.ToString());
                }
                else if (c == '"' || c == '\'')
                {
                    if (currentToken.Length > 0)
                    {
                        tokens.Enqueue(currentToken);
                        currentToken = "";
                    }
                    var startChar = c;
                    currentToken += c;
                    i++;
                    while (i < input.Length && input[i] != startChar)
                    {
                        currentToken += input[i];
                        i++;
                    }
                    if (i < input.Length)
                    {
                        currentToken += input[i];
                    }
                    else
                    {
                        throw new WqlParseException("Unterminated string");
                    }
                }
                else
                {
                    currentToken += c;
                }
            }

            if (currentToken.Length > 0)
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
        private static bool PeekToken(Queue<string> tokenQueue, string currentToken)
        {
            return tokenQueue.Count > 0 && tokenQueue.Peek()?.ToLower() == currentToken?.ToLower();
        }

        /// <summary>
        /// Checks the next n token.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <param name="tokens">The tokens to check.</param>
        /// <returns>True if the token is the current and next one, false otherwise.</returns>
        private static bool PeekToken(Queue<string> tokenQueue, params string[] tokens)
        {
            var elements = tokenQueue.Take(tokens.Count());

            if (elements.Count() != tokens.Count())
            {
                return false;
            }

            return !elements.Except(tokens).Any();
        }

        /// <summary>
        /// Checks the next token.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <param name="regex">The token to check.</param>
        /// <returns>True if the token is the current one, false otherwise.</returns>
        private static bool PeekToken(Queue<string> tokenQueue, Regex regex)
        {
            return tokenQueue.Count > 0 && regex.IsMatch(tokenQueue.Peek()?.ToLower());
        }

        /// <summary>
        /// Consumes the current token.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <param name="token">The token to be consumed.</param>
        /// <returns>The value of the token.</returns>
        private static string ReadToken(Queue<string> tokenQueue, string token)
        {
            if (PeekToken(tokenQueue, token))
            {
                return tokenQueue.Dequeue();
            }
            else
            {
                throw new WqlParseException($"Expected '{token}'");
            }
        }

        /// <summary>
        /// Consumes the next n token.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <param name="tokens">The tokens to be consumed.</param>
        private static void ReadToken(Queue<string> tokenQueue, params string[] tokens)
        {
            foreach (var token in tokens)
            {
                if (PeekToken(tokenQueue, token))
                {
                    tokenQueue.Dequeue();
                }
                else
                {
                    throw new WqlParseException($"Expected '{token}'");
                }
            }
        }

        /// <summary>
        /// Consumes the current token.
        /// </summary>
        /// <param name="tokenQueue">The token queue with the remaining tokens.</param>
        /// <param name="regex">The token to be consumed.</param>
        /// <returns>The value of the token.</returns>
        private static string ReadToken(Queue<string> tokenQueue, Regex regex)
        {
            if (PeekToken(tokenQueue, regex))
            {
                return tokenQueue.Dequeue();
            }
            else
            {
                throw new WqlParseException($"Expected token matching '{regex}'");
            }
        }

        /// <summary>
        /// Registers a condition expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="WqlParseException"></exception>
        public static void Register<T>() where T : IWqlExpressionCondition, new()
        {
            var op = new T().Operator;

            if (!Conditions.ContainsKey(op))
            {
                Conditions.Add(op, typeof(T));

                return;
            }

            throw new WqlParseException($"Condition '{op}' cannot be registered because it already exists.");
        }

        /// <summary>
        /// Removes a condition expression.
        /// </summary>
        /// <param name="op">The operator to be derisgistrated.</param>
        public static void Remove(string op)
        {
            if (Conditions.ContainsKey(op))
            {
                Conditions.Remove(op);

                return;
            }
        }
    }
}
