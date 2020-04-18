using System;
using WebExpress.Workers;

namespace WebExpress.Pages
{
    public class SiteMapPage
    {
        /// <summary>
        /// Liefert oder setzt die ID de Seite
        /// </summary>
        public UriSegmentID ID { get; protected set; }

        /// <summary>
        /// Liefert oder setzt das Pfadsegment
        /// </summary>
        public string Segment { get; protected set; }

        /// <summary>
        /// Ermittelt den Anzeigestring der Uri
        /// </summary>
        public string Display { get; protected set; }

        /// <summary>
        /// Liefert oder setzt die Rückruffunktion 
        /// zum Erzeugen einer neuen Worker-Instanz
        /// </summary>
        public Func<UriPage, IWorker> Create { get; protected set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="name">Die ID der Seite</param>
        /// <param name="segment">Das Pfadsegment</param>
        /// <param name="display">Der Anzeigestring</param>
        /// <param name="create">Die Rückruffunktion</param>
        public SiteMapPage(UriSegmentID id, string segment,string display, Func<UriPage, IWorker> create)
        {
            ID = id;
            Segment = segment;
            Display = display;
            Create = create;
        }
    }
}