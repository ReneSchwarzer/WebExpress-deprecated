using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WebExpress.WebUri
{
    /// <summary>
    /// Static path segment.
    /// </summary>
    public class UriPathSegmentVariableGuid : UriPathSegmentVariable
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The path text.</param>
        /// <param name="tag">The tag or null</param>
        public UriPathSegmentVariableGuid(string name, object tag = null)
            : this(name, null, tag)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The path text.</param>
        /// <param name="display">The display text.</param>
        /// <param name="tag">The tag or null</param>
        public UriPathSegmentVariableGuid(string name, string display, object tag = null)
            : base(name, display, tag)
        {
            VariableName = name;
            Expression = @"^(\{){0,1}(([0-9a-fA-F]{8})\-([0-9a-fA-F]{4})\-([0-9a-fA-F]{4})\-([0-9a-fA-F]{4})\-([0-9a-fA-F]{12}))(\}){0,1}$";
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="segment">The path segment to copy.</param>
        public UriPathSegmentVariableGuid(UriPathSegmentVariableGuid segment)
            : base(segment.VariableName, segment.Display, segment.Tag)
        {
            Expression = segment.Expression;
        }

        /// <summary>
        /// Returns the variable.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The variable value pair.</returns>
        public override IDictionary<string, string> GetVariable(string value)
        {
            var match = Regex.Match(value, Expression, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            if (match.Success)
            {
                var dict = new Dictionary<string, string>
                    {
                        { VariableName, match.Groups[2].ToString() }
                    };

                return dict;
            }

            return new Dictionary<string, string>();
        }

        /// <summary>
        /// Make a deep copy.
        /// </summary>
        /// <returns>The copy.</returns>
        public override IUriPathSegment Copy()
        {
            return new UriPathSegmentVariableGuid(this) { Value = Value };
        }

        /// <summary>
        /// Converts the segment to a string.
        /// </summary>
        /// <returns>A string that represents the current segment.</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}