namespace WebExpress.WebApp.WebResource
{
    public class Pagination
    {
        /// <summary>
        /// Liefert oder setzt die Anzahl der Einträge pro Seiten
        /// </summary>
        public int Count { get; set; } = 50;

        /// <summary>
        /// Liefert oder setzt die Gesamtanzahl der Seiten
        /// </summary>
        public int Total { get; set; } = 1;

        /// <summary>
        /// Liefert oder setzt die Seitennummer der aktuellen Seite
        /// </summary>
        public int Current { get; set; } = 0;
    }
}
