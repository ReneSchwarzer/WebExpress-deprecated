namespace WebExpress.WebApp.Wql.Condition
{
    /// <summary>
    /// Interface of a condition expression.
    /// </summary>
    public interface IWqlExpressionCondition : IWqlExpression
    {
        /// <summary>
        /// Returns the attribute expression.
        /// </summary>
        WqlExpressionAttribute Attribute { get; }

        /// <summary>
        /// Returns the operator expression.
        /// </summary>
        string Operator { get; }
    }
}