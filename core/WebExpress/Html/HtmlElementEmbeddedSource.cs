using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Ermöglicht es Autoren, alternative Medienressourcen (z.B. verschiedene Audio- oder Videoformate) 
    /// für Medienelemente wie <video> oder <audio> anzugeben.
    /// </summary>
    public class HtmlElementEmbeddedSource : HtmlElement, IHtmlElementEmbedded
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementEmbeddedSource()
            : base("source", false)
        {

        }
    }
}
