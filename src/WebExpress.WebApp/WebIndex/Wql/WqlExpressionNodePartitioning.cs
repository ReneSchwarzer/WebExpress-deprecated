using System.Collections.Generic;
using System.Linq;
using WebExpress.WebApp.WebIndex;

namespace WebExpress.WebApp.WebIndex.Wql
{
    /// <summary>
    /// Describes the partitioning expression of a wql statement.
    /// </summary>
    public class WqlExpressionNodePartitioning<T> : IWqlExpressionNode<T> where T : IIndexItem
    {
        /// <summary>
        /// Returns the partitioning function expressions.
        /// </summary>
        public IReadOnlyList<WqlExpressionNodePartitioningFunction<T>> PartitioningFunctions { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        internal WqlExpressionNodePartitioning()
        {
        }

        /// <summary>
        /// Applies the filter to the unfiltered data object.
        /// </summary>
        /// <param name="unfiltered">The unfiltered data.</param>
        /// <returns>The filtered data.</returns>
        public IQueryable<Q> Apply<Q>(IQueryable<Q> unfiltered)
        {
            var filtered = unfiltered;

            foreach(var function in PartitioningFunctions)
            {
                filtered = function.Apply(filtered);
            }

            return filtered.AsQueryable();
        }

        /// <summary>
        /// Converts the partitioning expression to a string.
        /// </summary>
        /// <returns>The partitioning expression as a string.</returns>
        public override string ToString()
        {
            return string.Format("{0}", string.Join(" ", PartitioningFunctions)).Trim();
        }
    }
}