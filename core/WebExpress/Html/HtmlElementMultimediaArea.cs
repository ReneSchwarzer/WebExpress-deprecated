using System;

namespace WebExpress.Html
{
    /// <summary>
    /// Definiert in Verbindung mit dem <map>-Element eine Image Map.
    /// </summary>
    public class HtmlElementMultimediaArea : HtmlElement, IHtmlElementMultimedia
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementMultimediaArea()
            : base("area", false)
        {

        }
    }
}
