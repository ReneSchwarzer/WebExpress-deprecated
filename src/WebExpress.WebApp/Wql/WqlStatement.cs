using System.Linq;

namespace WebExpress.WebApp.Wql
{
    public class WqlStatement : IWqlExpression
    {
        /// <summary>
        /// Returns the filter expression.
        /// </summary>
        public IWqlExpressionFilter Filter { get; internal set; }

        /// <summary>
        /// Returns the order expression.
        /// </summary>
        public WqlExpressionOrder Order { get; internal set; }

        /// <summary>
        /// Returns the partitioning expression.
        /// </summary>
        public WqlExpressionPartitioning Partitioning { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        internal WqlStatement()
        {
        }

        /// <summary>
        /// Applies the filter to the unfiltered data object.
        /// </summary>
        /// <param name="unfiltered">The unfiltered data.</param>
        /// <returns>The filtered data.</returns>
        public IQueryable<T> Apply<T>(IQueryable<T> unfiltered)
        {
            var filtered = unfiltered;

            if (Filter != null)
            {
                filtered = Filter.Apply(filtered);
            }

            if (Order != null)
            {
                filtered = Order.Apply(filtered);
            }

            if (Partitioning != null)
            {
                filtered = Partitioning.Apply(filtered);
            }

            return filtered.AsQueryable();
        }

        /// <summary>
        /// Converts the WQL expression to a string.
        /// </summary>
        /// <returns>The WQL expression as a string.</returns>
        public override string ToString()
        {
            return string.Format
            (
                "{0} {1} {2}", 
                Filter != null ? Filter.ToString() : "", 
                Order != null ? Order.ToString() : "", 
                Partitioning != null ? Partitioning.ToString() : ""
            ).Trim();
        }
    }
}