namespace WebExpress.Html
{
    /// <summary>
    /// Hiermit können zusätzliche Medienspuren (z.B. Untertitel) für Elemente wie <video> oder<audio> angegeben werden. 
    /// </summary>
    public class HtmlElementMultimediaTrack : HtmlElement, IHtmlElementMultimedia
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementMultimediaTrack()
            : base("track", false)
        {

        }
    }
}
