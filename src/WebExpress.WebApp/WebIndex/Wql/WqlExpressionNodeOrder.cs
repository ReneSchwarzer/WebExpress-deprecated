using System.Collections.Generic;
using System.Linq;
using WebExpress.WebApp.WebIndex;

namespace WebExpress.WebApp.WebIndex.Wql
{
    /// <summary>
    /// Describes the order expression of a wql statement.
    /// </summary>
    public class WqlExpressionNodeOrder<T> : IWqlExpressionNode<T> where T : IIndexItem
    {
        /// <summary>
        /// Returns the order attribute expressions.
        /// </summary>
        public IReadOnlyList<WqlExpressionNodeOrderAttribute<T>> Attributes { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        internal WqlExpressionNodeOrder()
        {
        }

        /// <summary>
        /// Applies the filter to the unfiltered data object.
        /// </summary>
        /// <param name="unfiltered">The unfiltered data.</param>
        /// <returns>The filtered data.</returns>
        public IQueryable<T> Apply(IQueryable<T> unfiltered)
        {
            var filtered = unfiltered;

            foreach (var attribute in Attributes)
            {
                filtered = attribute.Apply(filtered);
            }

            return filtered.AsQueryable();
        }

        /// <summary>
        /// Converts the order expression to a string.
        /// </summary>
        /// <returns>The order expression as a string.</returns>
        public override string ToString()
        {
            return string.Format("order by {0}", string.Join(", ", Attributes)).Trim();
        }
    }
}