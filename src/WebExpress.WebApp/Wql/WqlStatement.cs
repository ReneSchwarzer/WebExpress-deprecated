using System.Linq;

namespace WebExpress.WebApp.Wql
{
    public class WqlStatement
    {
        public IFilter Filter { get; internal set; }
        public Order Order { get; internal set; }
        public Partitioning Partitioning { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        internal WqlStatement()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="wqlString">The query in string form.</param>
        public WqlStatement(string wqlString)
        {
            var wql = Parser.Parse(wqlString);
        }

        /// <summary>
        /// Applies the filter to the unfiltered data object.
        /// </summary>
        /// <param name="unfiltered">The unfiltered data.</param>
        /// <returns>The filtered data.</returns>
        public IQueryable<T> Apply<T>(IQueryable<T> unfiltered)
        {
            return unfiltered.AsQueryable();
        }
    }
}
