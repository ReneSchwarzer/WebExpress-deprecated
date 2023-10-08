using System.Collections.Generic;
using WebExpress.WebApp.WebIndex;

namespace WebExpress.WebApp.WebIndex.Wql.Condition
{
    /// <summary>
    /// Describes the condition value expression of a wql statement.
    /// </summary>
    public abstract class WqlExpressionNodeFilterConditionSet<T> : WqlExpressionNodeFilterCondition<T> where T : IIndexItem
    {
        /// <summary>
        /// Returns the parameter expressions.
        /// </summary>
        public IEnumerable<WqlExpressionNodeParameter<T>> Parameters { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="token">One or more tokens that determine the operation. Multiple tokens are separated by spaces.</param>
        protected WqlExpressionNodeFilterConditionSet(string token)
            : base(token)
        {
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