using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace WebExpress.WebApp.WebIndex.Wql.Condition
{
    /// <summary>
    /// Interface of a condition expression.
    /// </summary>
    public abstract class WqlExpressionNodeFilterCondition<T> : IWqlExpressionNodeFilterCondition, IWqlExpressionNodeApply<T> where T : IIndexItem
    {
        /// <summary>
        /// Returns the attribute expression.
        /// </summary>
        public WqlExpressionNodeAttribute<T> Attribute { get; internal set; }

        /// <summary>
        /// Returns the operator expression.
        /// </summary>
        public string Operator { get; internal set; }

        /// <summary>
        /// Returns the culture in which to run the wql.
        /// </summary>
        public CultureInfo Culture { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="token">One or more tokens that determine the operation. Multiple tokens are separated by spaces.</param>
        protected WqlExpressionNodeFilterCondition(string token)
        {
            Operator = token;
        }

        /// <summary>
        /// Applies the filter to the index.
        /// </summary>
        /// <returns>The data ids from the index.</returns>
        public abstract IEnumerable<int> Apply();

        /// <summary>
        /// Applies the filter to the unfiltered data object.
        /// </summary>
        /// <param name="unfiltered">The unfiltered data.</param>
        /// <returns>The filtered data.</returns>
        public abstract IQueryable<T> Apply(IQueryable<T> unfiltered);

        /// <summary>
        /// Returns the sql query string.
        /// </summary>
        /// <returns>The sql part of the node.</returns>
        public abstract string GetSqlQueryString();
    }
}