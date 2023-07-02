using System.Collections;
using System.Linq;

namespace WebExpress.WebApp.Wql.Condition
{
    public class WqlExpressionNodeFilterConditionBinaryLessThan : WqlExpressionNodeFilterConditionBinary
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="op">The operator.</param>
        public WqlExpressionNodeFilterConditionBinaryLessThan()
            : base("<")
        {

        }

        /// <summary>
        /// Applies the filter to the unfiltered data object.
        /// </summary>
        /// <param name="unfiltered">The unfiltered data.</param>
        /// <returns>The filtered data.</returns>
        public override IQueryable<T> Apply<T>(IQueryable<T> unfiltered)
        {
            var comparer = new Comparer(Culture);
            var property = Attribute.Property;
            var value = Parameter.GetValue();
            var filtered = unfiltered.Where
            (
                x => comparer.Compare(property.GetValue(x), value) < 0
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

            return $"{property.Name} like '{value}'";
        }
    }
}
