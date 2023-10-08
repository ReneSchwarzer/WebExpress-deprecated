using System.Collections.Generic;
using System.Linq;
using WebExpress.WebApp.WebIndex;
using WebExpress.WebApp.WebIndex.Wql.Function;

namespace WebExpress.WebApp.WebIndex.Wql
{
    /// <summary>
    /// Describes the parameter expression of a wql statement.
    /// </summary>
    public class WqlExpressionNodeParameter<T> : IWqlExpressionNode<T> where T : IIndexItem
    {
        /// <summary>
        /// Returns the value expressions.
        /// </summary>
        public WqlExpressionNodeValue<T> Value { get; internal set; }

        /// <summary>
        /// Returns the function expressions.
        /// </summary>
        public WqlExpressionNodeFilterFunction<T> Function { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        internal WqlExpressionNodeParameter()
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