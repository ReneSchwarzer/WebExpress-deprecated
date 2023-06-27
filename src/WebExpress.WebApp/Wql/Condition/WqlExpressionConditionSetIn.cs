using System;
using System.Linq;

namespace WebExpress.WebApp.Wql.Condition
{
    /// <summary>
    /// Describes the condition value expression of a wql statement.
    /// </summary>
    public class WqlExpressionConditionSetIn : WqlExpressionConditionSet
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="op">The operator.</param>
        public WqlExpressionConditionSetIn()
            : base("in")
        {
        }

        /// <summary>
        /// Applies the filter to the unfiltered data object.
        /// </summary>
        /// <param name="unfiltered">The unfiltered data.</param>
        /// <returns>The filtered data.</returns>
        public override IQueryable<T> Apply<T>(IQueryable<T> unfiltered)
        {
            var property = GetPropertyInfo<T>();
            var filtered = unfiltered.Where
            (
                x => Parameters.Select(y => y.GetValue()).Contains(property.GetValue(x))
            );

            return filtered.AsQueryable();
        }
    }
}