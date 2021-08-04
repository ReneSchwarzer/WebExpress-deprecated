using WebExpress.Html;

namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Erstellt eine Slideshowelement
    /// </summary>
    public class ControlCarouselItem
    {
        /// <summary>
        /// Liefert oder setzt die Überschrift
        /// </summary>
        public string Headline { get; set; }

        /// <summary>
        /// Liefert oder setzt den Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Liefert oder setzt das Slideshowelement, wie zum Beispiel ein Bild
        /// </summary>
        public IControl Control { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlCarouselItem()
        {
        }
        
    }
}
