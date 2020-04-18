using WebExpress.Messages;
using WebExpress.Pages;

namespace WebExpress.Workers
{
    /// <summary>
    /// Arbeitet eine Weiterleitungs Anfrage ab. Dies erfolgt nebenläufig
    /// </summary>
    public class WorkerRedirect : Worker
    {
        /// <summary>
        /// Liefert oder setzt die Umleitungs-Uri
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Liefert oder setzt die Umleitungsart permanently moved oder temporarily moved
        /// </summary>
        public bool TemporarilyMoved { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="uri">Die Uri des Workers</param>
        /// <param name="location">Die Umleitungs-Uri</param>
        /// <param name="temporarilyMoved">Die Umleitungsart</param>
        public WorkerRedirect(UriPage uri, string location, bool temporarilyMoved)
            : base(uri)
        {
            Location = location;
            TemporarilyMoved = temporarilyMoved;
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Antwort</returns>
        public override Response Process(Request request)
        {
            Response response;

            if (!TemporarilyMoved)
            {
                response = new ResponseRedirectPermanentlyMoved(Location);
            }
            else
            {
                response = new ResponseRedirectTemporarilyMoved(Location);
            }

            return response;
        }
    }
}
