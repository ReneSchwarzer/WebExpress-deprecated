namespace WebExpress.WebApp.Wql
{
    /// <summary>
    /// Describes the binary operator expression of a wql statement.
    /// </summary>
    public enum WqlExpressionOperatorBinary
    {
        Equal,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        NotEqual,
        Like,
        Is,
        IsNot,
        Was
    }

    /// <summary>
    /// Extension method to convert the binary operator to a string.
    /// </summary>
    public static class BinaryOperatorExtensions
    {
        /// <summary>
        /// Convert the operator to a string
        /// </summary>
        /// <param name="op">The operator value.</param>
        /// <returns>A converted string.</returns>
        public static string ValueToString(this WqlExpressionOperatorBinary op)
        {
            return op switch
            {
                WqlExpressionOperatorBinary.Equal => "=",
                WqlExpressionOperatorBinary.GreaterThan => ">",
                WqlExpressionOperatorBinary.LessThan => "<",
                WqlExpressionOperatorBinary.GreaterThanOrEqual => ">=",
                WqlExpressionOperatorBinary.LessThanOrEqual => "<=",
                WqlExpressionOperatorBinary.NotEqual => "!=",
                WqlExpressionOperatorBinary.Like => "~",
                WqlExpressionOperatorBinary.Is => "is",
                WqlExpressionOperatorBinary.IsNot => "is not",
                WqlExpressionOperatorBinary.Was => "was",
                _ => "",
            };
        }
    }
}