namespace WebExpress.Pages
{
    public class PageApi : Page
    {
        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageApi()
        {

        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Init()
        {
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {

        }

        /// <summary>
        /// In String konvertieren
        /// </summary>
        /// <returns>Das Objekt als String</returns>
        public override string ToString()
        {
            return Content;
        }
    }
}
