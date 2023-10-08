namespace WebExpress.WebApp.WebIndex.Wql
{
    public class WqlToken
    {
        /// <summary>
        /// Returns the starting position of the token in the raw statement.
        /// </summary>
        public int Offset { get; internal set; }

        /// <summary>
        /// Returns the length of the token (start + length = end) in the raw statement.
        /// </summary>
        public int Length { get { return Value?.Length ?? 0; } }

        /// <summary>
        /// Returns the token value.
        /// </summary>
        public string Value { get; internal set; }

        /// <summary>
        /// Checks if the token is empty.
        /// </summary>
        /// <returns>True if no value is stored, false otherwise.</returns>
        public bool IsEmpty()
        {
            return Value == null || Value.Length == 0;
        }

        /// <summary>
        /// Adds a character at the end.
        /// </summary>
        /// <param name="c">The character to add.</param>
        internal void Append(char c)
        {
            Value += c;
        }

        /// <summary>
        /// Converts the token to a string.
        /// </summary>
        /// <returns>The token as a string.</returns>
        public override string ToString()
        {
            return Value;
        }
    }
}
