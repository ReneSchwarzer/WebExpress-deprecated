using System.Collections.Generic;

namespace WebExpress.WebUri
{
    /// <summary>
    /// The path segment of a uri.
    /// </summary>
    public interface IUriPathSegmentVariable : IUriPathSegment
    {
        /// <summary>
        /// Returns the variable name.
        /// </summary>
        public string VariableName { get; }

        /// <summary>
        /// Returns the regex expression.
        /// </summary>
        public string Expression { get; }

        /// <summary>
        /// Returns the variable.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The variable value pair.</returns>
        IDictionary<string, string> GetVariable(string value);
    }
}