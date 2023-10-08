using System.Collections.Generic;

namespace WebExpress.WebApp.WebIndex
{
    public static class IndexTermTokenizer
    {
        /// <summary>
        /// Tokenize an input string into an enumeration of terms.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>An enumeration of terms.</returns>
        public static IEnumerable<IndexTermToken> Tokenize(string input)
        {
            var currentToken = "";
            var position = 0;

            if (input == null)
            {
                yield break;
            }

            for (int i = 0; i < input.Length; i++)
            {
                var c = input[i];

                if (char.IsWhiteSpace(c))
                {
                    if (currentToken.Length > 0)
                    {
                        yield return new IndexTermToken()
                        {
                            Position = position,
                            Value = currentToken
                        };
                    }

                    currentToken = "";
                    position++;
                }
                else if (c == '<' || c == '>' || c == '(' || c == ')' || c == '!')
                {
                    if (currentToken.Length > 0)
                    {
                        yield return new IndexTermToken()
                        {
                            Position = position,
                            Value = currentToken
                        };
                    }

                    currentToken = "";
                    position++;
                }
                else
                {
                    currentToken += c;
                }
            }

            if (currentToken.Length > 0)
            {
                yield return new IndexTermToken()
                {
                    Position = position,
                    Value = currentToken
                };
            }
        }
    }
}
