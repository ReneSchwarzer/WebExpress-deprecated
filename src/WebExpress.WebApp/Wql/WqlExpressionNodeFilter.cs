using System.Linq;
using WebExpress.WebApp.Wql.Condition;

namespace WebExpress.WebApp.Wql
{
    /// <summary>
    /// Describes the filter expression of a wql statement.
    /// </summary>
    public class WqlExpressionNodeFilter : IWqlExpressionNodeApply
    {
        /// <summary>
        /// Returns the condition expression.
        /// </summary>
        public WqlExpressionNodeFilterCondition Condition { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        internal WqlExpressionNodeFilter()
        {
        }

        /// <summary>
        /// Applies the filter to the unfiltered data object.
        /// </summary>
        /// <param name="unfiltered">The unfiltered data.</param>
        /// <returns>The filtered data.</returns>
        public virtual IQueryable<T> Apply<T>(IQueryable<T> unfiltered)
        {
            return Condition?.Apply(unfiltered) ?? unfiltered;
        }

        /// <summary>
        /// Returns the sql query string.
        /// </summary>
        /// <returns>The sql part of the node.</returns>
        public string GetSqlQueryString()
        {
            var sql = Condition.GetSqlQueryString();

            return sql;
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