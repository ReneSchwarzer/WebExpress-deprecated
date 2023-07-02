using System;
using System.Linq;

namespace WebExpress.WebApp.Wql.Function
{
    /// <summary>
    /// Describes the function expression of a wql statement.
    /// Returns the current date.
    /// </summary>
    public class WqlExpressionNodeFilterFunctionDay : WqlExpressionNodeFilterFunction
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public WqlExpressionNodeFilterFunctionDay()
            :base("day")
        {
        }

        /// <summary>
        /// Executes the function.
        /// </summary>
        /// <returns>The return value.</returns>
        public override object Execute()
        {
            var parameters = Parameters.Select(x => x.GetValue());
            var param = parameters.FirstOrDefault();

            if (param != null)
            {
                return DateTime.Now.Date.AddDays(Convert.ToDouble(param));
            }
            
            return DateTime.Now.Date;
        }
    }
}