using System.Linq;

namespace WebExpress.WebApp.Wql
{
    /// <summary>
    /// Describes the partitioning expression of a wql statement.
    /// </summary>
    public class WqlExpressionPartitioningFunction : IWqlExpression
    {
        /// <summary>
        /// Returns the operator expressions.
        /// </summary>
        public WqlExpressionPartitioningOperator Operator { get; internal set; }

        /// <summary>
        /// Returns the value expressions.
        /// </summary>
        public int Value { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        internal WqlExpressionPartitioningFunction()
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

            if (Operator == WqlExpressionPartitioningOperator.Skip)
            {
                filtered = filtered.Skip(Value);
            }

            if (Operator == WqlExpressionPartitioningOperator.Take)
            {
                filtered = filtered.Take(Value);
            }

            return filtered.AsQueryable();
        }

        /// <summary>
        /// Converts the partitioning expression to a string.
        /// </summary>
        /// <returns>The partitioning expression as a string.</returns>
        public override string ToString()
        {
            return string.Format("{0} {1}", Operator.ToString().ToLower(), Value).Trim();
        }
    }
}