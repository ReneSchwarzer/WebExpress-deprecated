namespace WebExpress.WebApp.WebIndex
{
    public class IndexTermToken
    {
        /// <summary>
        /// Returns the position of the token in the input value.
        /// </summary>
        public uint Position { get; internal set; }

        /// <summary>
        /// Returns the token value.
        /// </summary>
        public string Value { get; internal set; }
    }
}
