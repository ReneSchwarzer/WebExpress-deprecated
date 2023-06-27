namespace WebExpress.WebApp.Wql
{
    /// <summary>
    /// Describes the value expression of a wql statement.
    /// </summary>
    public class WqlExpressionValue
    {
        /// <summary>
        /// Returns the value as string.
        /// </summary>
        public string StringValue { get; internal set; }

        /// <summary>
        /// Returns the value as int.
        /// </summary>
        public int? NumberValue { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        internal WqlExpressionValue()
        {
        }

        /// <summary>
        /// Returns the value.
        /// </summary>
        /// <returns>The value.</returns>
        public object GetValue()
        {
            return NumberValue.HasValue ? NumberValue.Value : StringValue;
        }

        /// <summary>
        /// Converts the value to a string.
        /// </summary>
        /// <param name="value">The value.</param>
        public static explicit operator string(WqlExpressionValue value)
        {
            return value.GetValue().ToString();
        }

        /// <summary>
        /// Converts the value expression to a string.
        /// </summary>
        /// <returns>The value expression as a string.</returns>
        public override string ToString()
        {
            return string.Format
            (
                "{0}",
                NumberValue.HasValue ? NumberValue.Value.ToString() : "'" + StringValue + "'"
            ).Trim();
        }
    }
}