using System;

namespace WebExpress.WebApp.Wql
{
    public class WqpParseException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">The massage.</param>
        public WqpParseException(string message)
            : base(message)
        {
        }
    }
}
