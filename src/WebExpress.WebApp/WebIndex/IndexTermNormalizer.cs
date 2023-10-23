using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace WebExpress.WebApp.WebIndex
{
    public static class IndexTermNormalizer
    {
        /// <summary>
        /// Converts an input string into a standardized form.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>The normalized form of the input string.</returns>
        public static IEnumerable<IndexTermToken> Normalize(IEnumerable<IndexTermToken> input)
        {
            foreach (var token in input)
            {
                token.Value = Normalize(token.Value);
                yield return token;
            }
        }

        /// <summary>
        /// Converts an input string into a standardized form.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>The normalized form of the input string.</returns>
        public static string Normalize(string input)
        {
            var normalized = input.Normalize(NormalizationForm.FormKD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().ToLowerInvariant();
        }
    }
}
