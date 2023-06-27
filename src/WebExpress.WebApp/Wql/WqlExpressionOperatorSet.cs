namespace WebExpress.WebApp.Wql
{
    /// <summary>
    /// Describes the operator expression of a wql statement.
    /// </summary>
    public enum WqlExpressionOperatorSet
    {
        In,
        NotIn,
        WasIn
    }

    /// <summary>
    /// Extension method to convert the set operator to a string.
    /// </summary>
    public static class SetOperatorExtensions
    {
        /// <summary>
        /// Convert the operator to a string
        /// </summary>
        /// <param name="op">The operator value.</param>
        /// <returns>A converted string.</returns>
        public static string ValueToString(this WqlExpressionOperatorSet op)
        {
            return op switch
            {
                WqlExpressionOperatorSet.In => "in",
                WqlExpressionOperatorSet.NotIn => "not in",
                WqlExpressionOperatorSet.WasIn => "was in",
                _ => "",
            };
        }
    }
}