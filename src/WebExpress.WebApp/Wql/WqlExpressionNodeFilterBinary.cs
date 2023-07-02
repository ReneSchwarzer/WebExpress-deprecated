using System.Collections.Generic;
using System.Linq;

namespace WebExpress.WebApp.Wql
{
    /// <summary>
    /// Describes the filter expression of a wql statement.
    /// </summary>
    public class WqlExpressionNodeFilterBinary : WqlExpressionNodeFilter
    {
        /// <summary>
        /// Returns the left filter expressions.
        /// </summary>
        public WqlExpressionNodeFilter LeftFilter { get; internal set; }

        /// <summary>
        /// Returns the logical operator expressions.
        /// </summary>
        public WqlExpressionLogicalOperator LogicalOperator { get; internal set; }

        /// <summary>
        /// Returns the right filter expressions.
        /// </summary>
        public WqlExpressionNodeFilter RightFilter { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        internal WqlExpressionNodeFilterBinary()
        {
        }

        /// <summary>
        /// Applies the filter to the unfiltered data object.
        /// </summary>
        /// <param name="unfiltered">The unfiltered data.</param>
        /// <returns>The filtered data.</returns>
        public override IQueryable<T> Apply<T>(IQueryable<T> unfiltered)
        {
            var filtered = unfiltered;
            var leftFiltered = LeftFilter.Apply(filtered);
            var rightFiltered = RightFilter.Apply(filtered);

            switch (LogicalOperator)
            {
                case WqlExpressionLogicalOperator.And:
                    filtered = leftFiltered.Intersect(rightFiltered);
                    break;

                case WqlExpressionLogicalOperator.Or:
                    filtered = leftFiltered.Union(rightFiltered);
                    break;
                default:
                    break;
            }

            return filtered.AsQueryable();
        }

        /// <summary>
        /// Converts the filter expression to a string.
        /// </summary>
        /// <returns>The filter expression as a string.</returns>
        public override string ToString()
        {
            return string.Format
            (
                "({0} {1} {2})",
                LeftFilter,
                LogicalOperator.ToString().ToLower(),
                RightFilter
            ).Trim();
        }
    }
}