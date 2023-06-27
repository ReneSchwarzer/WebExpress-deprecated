using System.Linq;

namespace WebExpress.WebApp.Wql
{
    /// <summary>
    /// Interface of a wql expression.
    /// </summary>
    public interface IWqlExpression
    {
        /// <summary>
        /// Applies the filter to the unfiltered data object.
        /// </summary>
        /// <param name="unfiltered">The unfiltered data.</param>
        /// <returns>The filtered data.</returns>
        IQueryable<T> Apply<T>(IQueryable<T> unfiltered);
    }
}