using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WebExpress.WebApp.Wql
{
    /// <summary>
    /// The Parser class is a class that implements a parser for the WQL query language. The parser reads an 
    /// input string that contains a WQL query and returns a WQL object that represents the structure of the query.
    /// To use the parser, call the Parse method with the string to be parsed to get a WQL object. This object 
    /// contains the structure of the WQL query and can be used to evaluate or process the query.
    /// </summary>
    public class Parser
    {
        private static readonly Regex NameRegex = new Regex("^[A-Za-z_][A-Za-z0-9_]*$", RegexOptions.Compiled);
        private static readonly Regex NumberRegex = new Regex("^[0-9]+$", RegexOptions.Compiled);
        private static readonly Regex ValueRegex = new Regex("^\"[A-Za-z0-9_@<>=~$%/!.,;:\\\\-]+\"|'[A-Za-z0-9_@<>=~$%/!.,;:\\\\-]+'$", RegexOptions.Compiled);

        private readonly string[] _tokens;
        private int _index;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="input">The wql input string.</param>
        private Parser(string input)
        {
            _tokens = Tokenize(input);
            _index = 0;
        }

        /// <summary>
        /// Parses a given WQL query.
        /// </summary>
        /// <param name="input"> An input string that contains a WQL query.</param>
        /// <returns>A WQL object that represents the structure of the query.</returns>
        public static WqlStatement Parse(string input)
        {
            var wql = new WqlStatement();
            var parser = new Parser(input);

            if (string.IsNullOrWhiteSpace(input))
            {
                return wql;
            }

            wql.Filter = parser.ParseFilter();
            wql.Order = parser.ParseOrder();
            wql.Partitioning = parser.ParsePartitioning();

            return wql;
        }

        private IFilter ParseFilter()
        {
            if (PeekToken("order") | PeekToken("take") | PeekToken("skip"))
            {
                return null;
            }

            if (PeekToken("("))
            {
                ReadToken("(");
                var filter = ParseFilter();
                ReadToken(")");

                return filter;
            }

            var leftFilter = ParseFilter();
            if (leftFilter != null)
            {
                var logicalOperator = ParseLogicalOperator();
                var rightFilter = ParseFilter();

                return new FilterOr
                {
                    LeftCondition = leftFilter,
                    LogicalOperator = logicalOperator,
                    RightCondition = rightFilter
                };
            }

            var condition = ParseCondition();
            return condition != null ? new Filter { Condition = condition } : null;
        }

        private ICondition ParseCondition()
        {
            if (PeekToken(NameRegex))
            {
                var attribute = ParseAttribute();
                var op = ParseOperator();
                if (PeekToken(NameRegex))
                {
                    var function = ParseFunction();

                    return new ConditionFunction
                    {
                        Attribute = attribute,
                        Operator = op,
                        Function = function
                    };
                }
                else
                {
                    var value = ParseValue();

                    return new ConditionValue
                    {
                        Attribute = attribute,
                        Operator = op,
                        Value = value
                    };
                }
            }



            return null;
        }

        private LogicalOperator ParseLogicalOperator()
        {
            if (PeekToken("and"))
            {
                ReadToken("and");
                return LogicalOperator.And;
            }
            else if (PeekToken("or"))
            {
                ReadToken("or");
                return LogicalOperator.Or;
            }
            else
            {
                throw new Exception("Expected 'and' or 'or'");
            }
        }

        private Attribute ParseAttribute()
        {
            var name = ReadToken(NameRegex);
            return new Attribute { Name = name };
        }

        private Function ParseFunction()
        {
            var name = ReadToken(NameRegex);
            ReadToken("(");
            var parameters = new List<Parameter>();
            parameters.Add(ParseParameter());
            while (PeekToken(","))
            {
                ReadToken(",");
                parameters.Add(ParseParameter());
            }
            ReadToken(")");
            return new Function { Name = name, Parameters = parameters };
        }

        private Parameter ParseParameter()
        {
            if (PeekToken(NameRegex))
            {
                return new Parameter { Function = ParseFunction() };
            }
            else
            {
                return new Parameter { Value = ParseValue() };
            }
        }

        private Operator ParseOperator()
        {
            if (PeekToken("="))
            {
                ReadToken("=");
                return Operator.Equal;
            }
            else if (PeekToken(">"))
            {
                ReadToken(">");
                return Operator.GreaterThan;
            }
            else if (PeekToken("<"))
            {
                ReadToken("<");
                return Operator.LessThan;
            }
            else if (PeekToken(">="))
            {
                ReadToken(">=");
                return Operator.GreaterThanOrEqual;
            }
            else if (PeekToken("<="))
            {
                ReadToken("<=");
                return Operator.LessThanOrEqual;
            }
            else if (PeekToken("!="))
            {
                ReadToken("!=");
                return Operator.NotEqual;
            }
            else if (PeekToken("~"))
            {
                ReadToken("~");
                return Operator.Like;
            }
            else if (PeekToken("is"))
            {
                ReadToken("is");
                if (PeekToken("not"))
                {
                    ReadToken("not");
                    return Operator.IsNot;
                }
                else
                {
                    return Operator.Is;
                }
            }
            else if (PeekToken("in"))
            {
                ReadToken("in");
                return Operator.In;
            }
            else if (PeekToken("not"))
            {
                ReadToken("not");
                ReadToken("in");
                return Operator.NotIn;
            }
            else if (PeekToken("was"))
            {
                ReadToken("was");
                return Operator.Was;
            }
            else
            {
                throw new Exception("Expected operator");
            }
        }

        private Order ParseOrder()
        {
            if (PeekToken("order"))
            {
                ReadToken("order");
                ReadToken("by");
                var attributes = new List<OrderAttribute>();
                attributes.Add(ParseOrderAttribute());
                while (PeekToken(","))
                {
                    ReadToken(",");
                    attributes.Add(ParseOrderAttribute());
                }
                return new Order { Attributes = attributes };
            }
            else
            {
                return null;
            }
        }

        private OrderAttribute ParseOrderAttribute()
        {
            var attribute = ParseAttribute();
            var descending = ParseDescendingOrder();
            return new OrderAttribute { Attribute = attribute, Descending = descending };
        }

        private bool ParseDescendingOrder()
        {
            if (PeekToken("asc"))
            {
                ReadToken("asc");
                return false;
            }
            else if (PeekToken("desc"))
            {
                ReadToken("desc");
                return true;
            }
            else
            {
                return false;
            }
        }

        private Partitioning ParsePartitioning()
        {
            var partitioning = new Partitioning();
            while (PeekToken("take") || PeekToken("skip"))
            {
                var op = ParsePartitioningOperator();
                var number = ParseNumber();
                if (op == PartitioningOperator.Take)
                {
                    partitioning.Take = number;
                }
                else
                {
                    partitioning.Skip = number;
                }
            }
            return partitioning;
        }

        private PartitioningOperator ParsePartitioningOperator()
        {
            if (PeekToken("take"))
            {
                ReadToken("take");
                return PartitioningOperator.Take;
            }
            else if (PeekToken("skip"))
            {
                ReadToken("skip");
                return PartitioningOperator.Skip;
            }
            else
            {
                throw new Exception("Expected 'take' or 'skip'");
            }
        }

        private int ParseNumber()
        {
            var token = ReadToken(NumberRegex);
            return int.Parse(token);
        }


        private static string[] Tokenize(string input)
        {
            var tokens = new List<string>();
            var currentToken = "";
            for (int i = 0; i < input.Length; i++)
            {
                var c = input[i];
                if (char.IsWhiteSpace(c))
                {
                    if (currentToken.Length > 0)
                    {
                        tokens.Add(currentToken);
                        currentToken = "";
                    }
                }
                else if (c == ',' || c == '(' || c == ')')
                {
                    if (currentToken.Length > 0)
                    {
                        tokens.Add(currentToken);
                        currentToken = "";
                    }
                    tokens.Add(c.ToString());
                }
                else if (c == '"' || c == '\'')
                {
                    if (currentToken.Length > 0)
                    {
                        tokens.Add(currentToken);
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
                        throw new Exception("Unterminated string");
                    }
                }
                else
                {
                    currentToken += c;
                }
            }
            if (currentToken.Length > 0)
            {
                tokens.Add(currentToken);
            }
            return tokens.ToArray();
        }

        private bool PeekToken(string token)
        {
            return _index < _tokens.Length && _tokens[_index] == token;
        }

        private bool PeekToken(Regex regex)
        {
            return _index < _tokens.Length && regex.IsMatch(_tokens[_index]);
        }

        private string ReadToken(string token)
        {
            if (PeekToken(token))
            {
                return _tokens[_index++];
            }
            else
            {
                throw new Exception($"Expected '{token}'");
            }
        }

        private string ReadToken(Regex regex)
        {
            if (PeekToken(regex))
            {
                return _tokens[_index++];
            }
            else
            {
                throw new Exception($"Expected token matching '{regex}'");
            }
        }

        private Value ParseValue()
        {
            if (PeekToken(ValueRegex))
            {
                var token = ReadToken(ValueRegex);
                return new Value { StringValue = token.Substring(1, token.Length - 2) };
            }
            else
            {
                var number = ParseNumber();
                return new Value { NumberValue = number };
            }
        }
    }
}
