using System.Collections.Generic;
using System.Linq;

namespace WebExpress.WebApp.WebIndex.Wql
{
    /// <summary>
    /// Interface of a wql expression.
    /// </summary>
    public interface IWqlExpressionNodeApply<T> : IWqlExpressionNode<T> where T : IIndexItem
    {
        /// <summary>
        /// Applies the filter to the index.
        /// </summary>
        /// <returns>The data ids from the index.</returns>
        IEnumerable<int> Apply();

        /// <summary>
        /// Applies the filter to the unfiltered data object.
        /// </summary>
        /// <param name="unfiltered">The unfiltered data.</param>
        /// <returns>The filtered data.</returns>
        IQueryable<T> Apply(IQueryable<T> unfiltered);

        /// <summary>
        /// Returns the sql query string.
        /// </summary>
        /// <returns>The sql part of the node.</returns>
        string GetSqlQueryString();
    }
}