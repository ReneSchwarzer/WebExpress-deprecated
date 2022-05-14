using System.Linq;

namespace WebExpress.WebApp.Wql
{
    public class WqlStatement
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public WqlStatement()
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="wql">Die Abfrage in Stringform</param>
        public WqlStatement(string wql)
        {

        }

        /// <summary>
        /// Wendet den Filter auf das ungefilterte Datenobjekt an
        /// </summary>
        /// <param name="unfiltered">Die ungefilterten Daten</param>
        /// <returns>Die gefilterten Daten</returns>
        public IQueryable<T> Apply<T>(IQueryable<T> unfiltered)
        {
            return unfiltered.AsQueryable();
        }
    }
}
