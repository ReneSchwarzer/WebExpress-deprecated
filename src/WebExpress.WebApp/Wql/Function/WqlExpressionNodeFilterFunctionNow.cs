using System;

namespace WebExpress.WebApp.Wql.Function
{
    /// <summary>
    /// Describes the function expression of a wql statement.
    /// Returns the current date and time.
    /// </summary>
    public class WqlExpressionNodeFilterFunctionNow : WqlExpressionNodeFilterFunction
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public WqlExpressionNodeFilterFunctionNow()
            :base("now")
        {
        }

        /// <summary>
        /// Executes the function.
        /// </summary>
        /// <returns>The return value.</returns>
        public override object Execute()
        {
            return DateTime.Now;
        }
    }
}