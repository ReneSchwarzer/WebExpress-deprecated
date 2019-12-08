namespace WebServer.Html
{
    public class HtmlElementImg : HtmlElement
    {
        /// <summary>
        /// Liefert oder setzt den ToolTip
        /// </summary>
        public string Alt
        {
            get => GetAttribute("alt");
            set => SetAttribute("alt", value);
        }

        /// <summary>
        /// Liefert oder setzt die Bild-Url
        /// </summary>
        public string Src
        {
            get => GetAttribute("src");
            set => SetAttribute("src", value);
        }

        /// <summary>
        /// Liefert oder setzt das Ziel
        /// </summary>
        public string Target
        {
            get => GetAttribute("target");
            set => SetAttribute("target", value);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementImg()
            : base("img", false)
        {

        }
    }
}
