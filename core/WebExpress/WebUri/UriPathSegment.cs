namespace WebExpress.WebUri
{
    /// <summary>
    /// Static path segment.
    /// </summary>
    public class UriPathSegment : IUriPathSegment
    {
        /// <summary>
        /// Returns or sets the path text.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Returns or sets the display text.
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// Returns or sets the tag.
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The path text.</param>
        /// <param name="tag">The tag or null</param>
        public UriPathSegment(string value, object tag = null)
        {
            Value = value;
            Display = value;
            Tag = tag;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The path text.</param>
        /// <param name="display">The display text.</param>
        /// <param name="tag">The tag or null</param>
        public UriPathSegment(string value, string display, object tag = null)
        {
            Value = value;
            Display = display;
            Tag = tag;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="segment">The path segment to copy.</param>
        public UriPathSegment(IUriPathSegment segment)
        {
            Value = segment.Value;
            Display = segment.Display;
            Tag = segment.Tag;
        }

        /// <summary>
        /// Converts the segment to a string.
        /// </summary>
        /// <returns>The string representation of the segment.</returns>
        public override string ToString()
        {
            return Value ?? Tag?.ToString();
        }
    }
}