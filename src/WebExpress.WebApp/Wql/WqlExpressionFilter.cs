using System.Linq;
using WebExpress.WebApp.Wql.Condition;

namespace WebExpress.WebApp.Wql
{
    /// <summary>
    /// Describes the filter expression of a wql statement.
    /// </summary>
    public class WqlExpressionFilter : IWqlExpressionFilter
    {
        /// <summary>
        /// Returns the condition expression.
        /// </summary>
        public IWqlExpressionCondition Condition { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        internal WqlExpressionFilter()
        {
        }

        /// <summary>
        /// Applies the filter to the unfiltered data object.
        /// </summary>
        /// <param name="unfiltered">The unfiltered data.</param>
        /// <returns>The filtered data.</returns>
        public IQueryable<T> Apply<T>(IQueryable<T> unfiltered)
        {
            return Condition.Apply(unfiltered);
        }

        /// <summary>
        /// Converts the filter expression to a string.
        /// </summary>
        /// <returns>The filter expression as a string.</returns>
        public override string ToString()
        {
            return string.Format("{0}", Condition).Trim();
        }
    }
}