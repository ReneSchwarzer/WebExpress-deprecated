using WebExpress.WebApp.Wql.Function;

namespace WebExpress.WebApp.Wql
{
    /// <summary>
    /// Describes the parameter expression of a wql statement.
    /// </summary>
    public class WqlExpressionParameter
    {
        /// <summary>
        /// Returns the value expressions.
        /// </summary>
        public WqlExpressionValue Value { get; internal set; }

        /// <summary>
        /// Returns the function expressions.
        /// </summary>
        public WqlExpressionFunction Function { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        internal WqlExpressionParameter()
        {
        }

        /// <summary>
        /// Returns the value.
        /// </summary>
        /// <returns>The value.</returns>
        public object GetValue()
        {
            return Function != null ? Function.Execute() : Value.GetValue();
        }

        /// <summary>
        /// Converts the function expression to a string.
        /// </summary>
        /// <returns>The function expression as a string.</returns>
        public override string ToString()
        {
            return string.Format("{0}", Value != null ? Value.ToString() : Function.ToString()).Trim();
        }
    }
}