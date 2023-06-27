using System.Collections.Generic;

namespace WebExpress.WebApp.Wql.Function
{
    /// <summary>
    /// Describes the function expression of a wql statement.
    /// </summary>
    public class WqlExpressionFunction
    {
        /// <summary>
        /// Returns the function name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Returns the parameter expressions.
        /// </summary>
        public List<WqlExpressionParameter> Parameters { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        internal WqlExpressionFunction()
        {
        }

        /// <summary>
        /// Executes the function.
        /// </summary>
        /// <returns>The return value.</returns>
        public object Execute()
        {
            return "";
        }

        /// <summary>
        /// Converts the function expression to a string.
        /// </summary>
        /// <returns>The function expression as a string.</returns>
        public override string ToString()
        {
            return string.Format("{0}({1})", Name, string.Join(", ", Parameters)).Trim();
        }
    }
}