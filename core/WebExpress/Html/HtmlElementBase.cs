using System.Text;

namespace WebServer.Html
{
    /// <summary>
    /// Stellt die Basis für relative Verweise da. 
    /// </summary>
    public class HtmlElementBase : HtmlElement
    {
        /// <summary>
        /// Liefert oder setzt die Ziel-Url
        /// </summary>
        public string Href
        {
            get => GetAttribute("href");
            set
            {
                var url = value;

                if (!string.IsNullOrWhiteSpace(url) && !url.EndsWith("/"))
                {
                    url += url + "/";
                }

                SetAttribute("href", url);
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementBase()
            : base("base")
        {
            CloseTag = false;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="url">Die Url</param>
        public HtmlElementBase(string url)
            : this()
        {
            Href = url;
        }
    }
}
