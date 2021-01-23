namespace WebExpress.Uri
{
    /// <summary>
    /// Statisches Pfadsegment
    /// </summary>
    public class UriPathSegment : IUriPathSegment
    {
        /// <summary>
        /// Liefert oder setzt den Pfadtext
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Liefert den Anzeigetext
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// Liefert oder setzt den Tag
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="value">Der Pfadtext</param>
        /// <param name="tag">Der Tag oder null</param>
        public UriPathSegment(string value, object tag = null)
        {
            Value = value;
            Display = value;
            Tag = tag;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="segment">Das zu kopierende Pfadsegment</param>
        public UriPathSegment(IUriPathSegment segment)
        {
            Value = segment.Value;
            Display = segment.Display;
            Tag = segment.Tag;
        }

        /// <summary>
        /// Wandelt das Segment in einen String um
        /// </summary>
        /// <returns>Die Stringrepräsentation des Segmentes</returns>
        public override string ToString()
        {
            return Value;
        }
    }
}