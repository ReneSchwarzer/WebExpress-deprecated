using System.Collections.Generic;
using System.Linq;

namespace WebExpress.WebApp.WebIndex.Wql
{
    /// <summary>
    /// Describes the filter expression of a wql statement.
    /// </summary>
    public class WqlExpressionNodeFilterBinary<T> : WqlExpressionNodeFilter<T> where T : IIndexItem
    {
        /// <summary>
        /// Returns the left filter expressions.
        /// </summary>
        public WqlExpressionNodeFilter<T> LeftFilter { get; internal set; }

        /// <summary>
        /// Returns the logical operator expressions.
        /// </summary>
        public WqlExpressionLogicalOperator LogicalOperator { get; internal set; }

        /// <summary>
        /// Returns the right filter expressions.
        /// </summary>
        public WqlExpressionNodeFilter<T> RightFilter { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        internal WqlExpressionNodeFilterBinary()
        {
        }

        /// <summary>
        /// Applies the filter to the index.
        /// </summary>
        /// <returns>The data ids from the index.</returns>
        public override IEnumerable<int> Apply()
        {
            var filtered = Enumerable.Empty<int>();
            var leftFiltered = LeftFilter.Apply();
            var rightFiltered = RightFilter.Apply();

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

            return filtered;
        }

        /// <summary>
        /// Applies the filter to the unfiltered data object.
        /// </summary>
        /// <param name="unfiltered">The unfiltered data.</param>
        /// <returns>The filtered data.</returns>
        public override IQueryable<T> Apply(IQueryable<T> unfiltered)
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
        /// Returns the sql query string.
        /// </summary>
        /// <returns>The sql part of the node.</returns>
        public override string GetSqlQueryString()
        {
            var leftFiltered = LeftFilter.GetSqlQueryString();
            var rightFiltered = RightFilter.GetSqlQueryString();

            switch (LogicalOperator)
            {
                case WqlExpressionLogicalOperator.And:
                    return $"{leftFiltered} and {rightFiltered}";

                case WqlExpressionLogicalOperator.Or:
                    return $"{leftFiltered} or {rightFiltered}";

                default:
                    break;
            }

            return "";
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