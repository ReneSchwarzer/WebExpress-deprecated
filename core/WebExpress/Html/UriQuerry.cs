namespace WebExpress.Html
{
    /// <summary>
    /// Statisches Pfadsegment
    /// </summary>
    public class UriQuerry
    {
        /// <summary>
        /// Liefert oder setzt den Tag
        /// </summary>
        public string Key { get; protected set; }

        /// <summary>
        /// Liefert oder setzt den Wert
        /// </summary>
        public string Value { get; protected set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="key">Der Schlüssel</param>
        /// <param name="value">Der Wert</param>
        public UriQuerry(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}