namespace WebExpress.Message
{
    public class Cookie
    {
        /// <summary>
        /// Liefert oder setzt den Namen
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Liefert oder setzt den Wert
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="cookie">Der Cookie</param>
        public Cookie(string cookie)
        {
            var split = cookie.Split('=');
            Name = split[0];
            Value = split.Length > 1 ? split[1] : "";
        }
    }
}
