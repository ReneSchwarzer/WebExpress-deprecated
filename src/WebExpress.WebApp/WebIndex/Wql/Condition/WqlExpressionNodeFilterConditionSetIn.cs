using System.Linq;

namespace WebExpress.WebApp.WebIndex.Wql.Condition
{
    /// <summary>
    /// Describes the condition value expression of a wql statement.
    /// </summary>
    public class WqlExpressionNodeFilterConditionSetIn<T> : WqlExpressionNodeFilterConditionSet<T> where T : IIndexItem
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="op">The operator.</param>
        public WqlExpressionNodeFilterConditionSetIn()
            : base("in")
        {
        }

        /// <summary>
        /// Applies the filter to the index.
        /// </summary>
        /// <returns>The data ids from the index.</returns>
        public override IQueryable<int> Apply()
        {
            var property = Attribute?.Property;
            //var value = Parameter.GetValue();

            //var filtered = unfiltered.Where
            //(
            //    x => property != null && property.GetValue(x).Equals(value)
            //);

            return null; //filtered.AsQueryable();
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
                x => values.Contains(property.GetValue(x))
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

            return $"{property.Name} in {string.Join(", ", values.Select(x => $"'{x}'"))}";
        }
    }
}