using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebExpress.WebApp.WebIndex
{
    public static class IndexTermTokenizer
    {
        private static char[] delimiters = new char[] { ' ', '?', '!', ':', '<', '>', '=', '%', '(', ')' };

        /// <summary>
        /// Tokenize an input string into an enumeration of terms.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>An enumeration of terms.</returns>
        public static IEnumerable<IndexTermToken> Tokenize(string input)
        {
            var currentToken = new StringBuilder();
            var position = (uint)0;

            if (input == null || input.Length == 0)
            {
                yield break;
            }

            foreach (var c in input)
            {
                if (char.IsWhiteSpace(c) || delimiters.Contains(c))
                {
                    if (currentToken.Length > 0)
                    {
                        yield return new IndexTermToken()
                        {
                            Position = position,
                            Value = IndexTermNormalizer.Normalize(currentToken.ToString())
                        };
                    }

                    currentToken = new StringBuilder();
                    position++;
                }
                else
                {
                    currentToken.Append(c);
                }
            }

            if (currentToken.Length > 0)
            {
                yield return new IndexTermToken()
                {
                    Position = position,
                    Value = IndexTermNormalizer.Normalize(currentToken.ToString())
                };
            }
        }
    }
}
