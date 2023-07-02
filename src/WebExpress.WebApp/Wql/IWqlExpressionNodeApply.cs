using System.Linq;

namespace WebExpress.WebApp.Wql
{
    /// <summary>
    /// Interface of a wql expression.
    /// </summary>
    public interface IWqlExpressionNodeApply : IWqlExpressionNode
    {
        /// <summary>
        /// Applies the filter to the unfiltered data object.
        /// </summary>
        /// <param name="unfiltered">The unfiltered data.</param>
        /// <returns>The filtered data.</returns>
        IQueryable<T> Apply<T>(IQueryable<T> unfiltered);

        /// <summary>
        /// Returns the sql query string.
        /// </summary>
        /// <returns>The sql part of the node.</returns>
        string GetSqlQueryString();
    }
}