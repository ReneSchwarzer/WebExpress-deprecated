using System.Linq;

namespace WebExpress.WebApp.WebIndex.Wql.Condition
{
    /// <summary>
    /// Describes the condition value expression of a wql statement.
    /// </summary>
    public class WqlExpressionNodeFilterConditionSetNotIn<T> : WqlExpressionNodeFilterConditionSet<T> where T : IIndexItem
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="op">The operator.</param>
        public WqlExpressionNodeFilterConditionSetNotIn()
            : base("not in")
        {
        }

        /// <summary>
        /// Applies the filter to the index.
        /// </summary>
        /// <returns>The data ids from the index.</returns>
        public override IQueryable<int> Apply()
        {
            return null;
        }

        /// <summary>
        /// Applies the filter to the unfiltered data object.
        /// </summary>
        /// <param name="unfiltered">The unfiltered data.</param>
        /// <returns>The filtered data.</returns>
        public override IQueryable<T> Apply(IQueryable<T> unfiltered)
        {
            var property = Attribute.Property;
            var values = Parameters.Select(y => y.GetValue());
            var filtered = unfiltered.Where
            (
                x => !values.Contains(property.GetValue(x))
            );

            return filtered;
        }

        /// <summary>
        /// Returns the sql query string.
        /// </summary>
        /// <returns>The sql part of the node.</returns>
        public override string GetSqlQueryString()
        {
            var property = Attribute?.Property;
            var values = Parameters;

            return $"{property.Name} not in {string.Join(", ", values.Select(x => $"'{x}'"))}";
        }


    }
}