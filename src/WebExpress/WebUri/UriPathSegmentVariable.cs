using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WebExpress.WebUri
{
    /// <summary>
    /// variable path segment.
    /// </summary>
    public abstract class UriPathSegmentVariable : IUriPathSegmentVariable
    {
        /// <summary>
        /// Returns the Id.
        /// </summary>
        public string Id => VariableName?.ToLower();

        /// <summary>
        /// Returns or sets the variable name.
        /// </summary>
        public string VariableName { get; set; }

        /// <summary>
        /// Returns or sets the path text.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Returns or sets the display text.
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// Returns or sets the regex expression.
        /// </summary>
        public string Expression { get; protected set; }

        /// <summary>
        /// Returns or sets the tag.
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Checks for empty path segment.
        /// </summary>
        public bool IsEmpty => string.IsNullOrWhiteSpace(VariableName) || VariableName.Equals("/");

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="tag">The tag or null</param>
        public UriPathSegmentVariable(string name, object tag = null)
            : this(name, null, tag)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="display">The display text.</param>
        /// <param name="tag">The tag or null</param>
        public UriPathSegmentVariable(string name, string display, object tag = null)
        {
            VariableName = name;
            Display = display;
            Tag = tag;
        }

        /// <summary>
        /// Returns the variable.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The variable value pair.</returns>
        public abstract IDictionary<string, string> GetVariable(string value);

        /// <summary>
        /// Checks whether the node matches the path element.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>True if the path element matched, false otherwise.</returns>
        public bool IsMatched(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }
            else if (string.IsNullOrWhiteSpace(Expression) && Value.Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else if (Regex.IsMatch(value, Expression, RegexOptions.IgnoreCase))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Make a deep copy.
        /// </summary>
        /// <returns>The copy.</returns>
        public abstract IUriPathSegment Copy();

        /// <summary>
        /// Compare the object.
        /// </summary>
        /// <param name="obj">The comparison object.</param>
        /// <returns>true if equals, false otherwise</returns>
        public virtual bool Equals(IUriPathSegment obj)
        {
            if (obj == null)
            {
                return false;
            }
            else if (obj is UriPathSegmentVariable segment)
            {
                return VariableName.Equals(segment.VariableName, StringComparison.OrdinalIgnoreCase) &&
                    Expression.Equals(segment.Expression);
            }

            return false;
        }

        /// <summary>
        /// Converts the segment to a string.
        /// </summary>
        /// <returns>A string that represents the current segment.</returns>
        public override string ToString()
        {
            return Value ?? $"${{{VariableName}}}";
        }
    }
}