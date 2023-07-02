using System.Linq;

namespace WebExpress.WebApp.Wql.Condition
{
    public class WqlExpressionNodeFilterConditionBinaryLike : WqlExpressionNodeFilterConditionBinary
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="op">The operator.</param>
        public WqlExpressionNodeFilterConditionBinaryLike()
            : base("~")
        {
        }

        /// <summary>
        /// Applies the filter to the unfiltered data object.
        /// </summary>
        /// <param name="unfiltered">The unfiltered data.</param>
        /// <returns>The filtered data.</returns>
        public override IQueryable<T> Apply<T>(IQueryable<T> unfiltered)
        {
            var property = Attribute.Property;
            var value = Parameter.GetValue().ToString();
            var filtered = unfiltered.Where
            (
                x => property.GetValue(x).ToString().Contains
                (
                    value
                )
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
            var value = Parameter.Value;

            return $"{property.Name} like '%{value}%'";
        }
    }
}
