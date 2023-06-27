using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WebExpress.WebApp.Wql.Condition
{
    /// <summary>
    /// Describes the condition value expression of a wql statement.
    /// </summary>
    public abstract class WqlExpressionConditionSet : IWqlExpressionCondition
    {
        /// <summary>
        /// Returns the attribute expression.
        /// </summary>
        public WqlExpressionAttribute Attribute { get; internal set; }

        /// <summary>
        /// Returns the operator expression.
        /// </summary>
        public string Operator { get; internal set; }

        /// <summary>
        /// Returns the parameter expressions.
        /// </summary>
        public List<WqlExpressionParameter> Parameters { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="op">The operator.</param>
        protected WqlExpressionConditionSet(string op)
        {
            Operator = op;
        }

        /// <summary>
        /// Applies the filter to the unfiltered data object.
        /// </summary>
        /// <param name="unfiltered">The unfiltered data.</param>
        /// <returns>The filtered data.</returns>
        public abstract IQueryable<T> Apply<T>(IQueryable<T> unfiltered);

        /// <summary>
        /// Returns the property info object, which is derived from the attribute name.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <returns>The property info object.</returns>
        /// <exception cref="WqlParseException">The exception that is thrown when an error occurs.</exception>
        protected PropertyInfo GetPropertyInfo<T>()
        {
            var property = typeof(T).GetProperties()
                .Where(x => x.Name.Equals(Attribute.Name, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            if (property == null)
            {
                throw new WqlParseException($"Attribute '{Attribute.Name}' is not known.");
            }

            Attribute.Name = property.Name;

            return property;
        }

        /// <summary>
        /// Converts the condition expression to a string.
        /// </summary>
        /// <returns>The condition expression as a string.</returns>
        public override string ToString()
        {
            return string.Format("{0} {1} ({2})", Attribute.ToString(), Operator.ToLower(), string.Join(", ", Parameters)).Trim();
        }
    }
}