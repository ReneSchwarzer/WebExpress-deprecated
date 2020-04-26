using System;

namespace WebExpress.Html
{
    /// <summary>
    /// Definiert in Verbindung mit dem <area>-Element eine Image Map.
    /// </summary>
    public class HtmlElementMultimediaMap : HtmlElement, IHtmlElementMultimedia
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementMultimediaMap()
            : base("map", false)
        {

        }
    }
}
