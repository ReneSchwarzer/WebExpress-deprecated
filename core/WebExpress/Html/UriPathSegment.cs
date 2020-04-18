namespace WebExpress.Html
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
            Tag = tag;
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