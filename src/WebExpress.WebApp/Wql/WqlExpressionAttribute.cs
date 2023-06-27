namespace WebExpress.WebApp.Wql
{
    /// <summary>
    /// Describes the attribute expression of a wql statement.
    /// </summary>
    public class WqlExpressionAttribute
    {
        /// <summary>
        /// Returns the name of the attribute.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        internal WqlExpressionAttribute()
        {
        }

        /// <summary>
        /// Converts the attribute expression to a string.
        /// </summary>
        /// <returns>The attribute expression as a string.</returns>
        public override string ToString()
        {
            return string.Format("{0}", Name).Trim();
        }
    }
}