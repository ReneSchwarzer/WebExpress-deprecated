namespace WebExpress.WebUri
{
    /// <summary>
    /// The path segment of a uri.
    /// </summary>
    public interface IUriPathSegment
    {
        /// <summary>
        /// Returns the path text.
        /// </summary>
        string Value { get; }

        /// <summary>
        /// Returns or sets the display text.
        /// </summary>
        string Display { get; set; }

        /// <summary>
        /// Returns the tag.
        /// </summary>
        object Tag { get; }
    }
}