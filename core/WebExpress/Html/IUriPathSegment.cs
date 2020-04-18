namespace WebExpress.Html
{
    /// <summary>
    /// Das Pfadsegment
    /// </summary>
    public interface IUriPathSegment
    {
        /// <summary>
        /// Liefert den Pfadtext
        /// </summary>
        string Value { get; }

        /// <summary>
        /// Liefert den Tag
        /// </summary>
        public object Tag { get; }
    }
}