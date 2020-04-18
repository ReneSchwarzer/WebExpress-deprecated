namespace WebExpress.Pages
{
    /// <summary>
    /// Anzeige eines Textes
    /// </summary>
    public class UriPathSegmentDynamicDisplayText : IUriPathSegmentDynamicDisplayItem
    {
        /// <summary>
        /// Liefert oder setzt den Wert
        /// </summary>
        public string Value { get; protected set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="value">Der Wert</param>
        public UriPathSegmentDynamicDisplayText(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Wandelt den Text in einen String um
        /// </summary>
        /// <returns>Die Stringrepräsentation des Textes</returns>
        public override string ToString()
        {
            return Value;
        }
    }
}