using System.Globalization;

namespace WebExpress.WebApp.WebIndex.Wql
{
    /// <summary>
    /// Describes the error in a wql statement.
    /// </summary>
    public class WqlExpressionError
    {
        /// <summary>
        /// Returns the start position in the raw statement.
        /// </summary>
        public int Position { get; internal set; }

        /// <summary>
        /// Returns the length of the broken token in the raw statement.
        /// </summary>
        public int Length { get; internal set; }

        /// <summary>
        /// Returns the culture in which to run the wql.
        /// </summary>
        public CultureInfo Culture { get; internal set; }

        /// <summary>
        /// Returns the errors that arose during parsing.
        /// </summary>
        public string Message { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        internal WqlExpressionError()
        {
        }

        /// <summary>
        /// Converts the filter expression to a string.
        /// </summary>
        /// <returns>The filter expression as a string.</returns>
        public override string ToString()
        {
            return string.Format("{0}", Message).Trim();
        }
    }
}