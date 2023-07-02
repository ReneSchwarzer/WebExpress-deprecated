namespace WebExpress.WebApp.Wql.Condition
{
    /// <summary>
    /// Describes the binary condition expression of a wql statement.
    /// </summary>
    public abstract class WqlExpressionNodeFilterConditionBinary : WqlExpressionNodeFilterCondition
    {
        /// <summary>
        /// Returns the parameter expression.
        /// </summary>
        public WqlExpressionNodeParameter Parameter { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="token">One or more tokens that determine the operation. Multiple tokens are separated by spaces.</param>
        protected WqlExpressionNodeFilterConditionBinary(string token)
            : base(token)
        {
        }

        /// <summary>
        /// Converts the condition expression to a string.
        /// </summary>
        /// <returns>The condition expression as a string.</returns>
        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Attribute.ToString(), Operator.ToLower(), Parameter.ToString()).Trim();
        }
    }
}