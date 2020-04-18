namespace WebExpress.Html
{
    public class HtmlElementLink : HtmlElement
    {
        /// <summary>
        /// Liefert oder setzt die Url
        /// </summary>
        public string Href
        {
            get => GetAttribute("href");
            set => SetAttribute("href", value);
        }

        /// <summary>
        /// Liefert oder setzt den Typ
        /// </summary>
        public string Rel
        {
            get => GetAttribute("rel");
            set => SetAttribute("rel", value);
        }

        /// <summary>
        /// Liefert oder setzt den Typ
        /// </summary>
        public string Type
        {
            get => GetAttribute("type");
            set => SetAttribute("type", value);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementLink()
            : base("link", false)
        {
        }
    }
}
