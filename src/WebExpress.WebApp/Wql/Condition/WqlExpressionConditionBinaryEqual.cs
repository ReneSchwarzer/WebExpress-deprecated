using System;
using System.Linq;

namespace WebExpress.WebApp.Wql.Condition
{
    public class WqlExpressionConditionBinaryEqual : WqlExpressionConditionBinary
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="op">The operator.</param>
        public WqlExpressionConditionBinaryEqual()
            : base("=")
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
                x => property.GetValue(x).ToString().Equals
                (
                    Parameter.GetValue().ToString(),
                    StringComparison.OrdinalIgnoreCase
                )
            );

            return filtered.AsQueryable();
        }
    }
}
