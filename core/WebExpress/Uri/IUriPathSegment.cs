namespace WebExpress.Uri
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
        /// Liefert oder setzt den Anzeigetext
        /// </summary>
        string Display { get; set; }

        /// <summary>
        /// Liefert den Tag
        /// </summary>
        object Tag { get; }
    }
}