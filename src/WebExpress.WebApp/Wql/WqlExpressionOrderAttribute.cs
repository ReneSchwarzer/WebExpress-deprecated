using System.Linq;

namespace WebExpress.WebApp.Wql
{
    /// <summary>
    /// Describes the order attribute of a wql statement.
    /// </summary>
    public class WqlExpressionOrderAttribute : IWqlExpression
    {
        /// <summary>
        /// Returns the attribute expressions.
        /// </summary>
        public WqlExpressionAttribute Attribute { get; internal set; }

        /// <summary>
        /// Returns the descending expressions.
        /// </summary>
        public bool Descending { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        internal WqlExpressionOrderAttribute()
        {
        }

        /// <summary>
        /// Applies the filter to the unfiltered data object.
        /// </summary>
        /// <param name="unfiltered">The unfiltered data.</param>
        /// <returns>The filtered data.</returns>
        public IQueryable<T> Apply<T>(IQueryable<T> unfiltered)
        {
            var filtered = unfiltered;
            var property = typeof(T).GetProperties()
                .Where(x => x.Name.Equals(Attribute.Name, System.StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            if (property == null)
            {
                throw new WqlParseException($"Order attribute '{Attribute.Name}' is not known.");
            }

            Attribute.Name = property.Name;

            if (Descending)
            {
                filtered = filtered.OrderByDescending(x => property.GetValue(x));
            }
            else
            {
                filtered = filtered.OrderBy(x => property.GetValue(x));
            }

            return filtered.AsQueryable();
        }

        /// <summary>
        /// Converts the order expression to a string.
        /// </summary>
        /// <returns>The order expression as a string.</returns>
        public override string ToString()
        {
            return string.Format("{0} {1}", Attribute.ToString(), Descending ? "desc" : "asc").Trim();
        }
    }
}