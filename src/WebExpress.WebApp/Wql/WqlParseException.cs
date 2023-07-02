using System;

namespace WebExpress.WebApp.Wql
{
    public class WqlParseException : Exception
    {
        /// <summary>
        /// Returns the token that caused the exception.
        /// </summary>
        public WqlToken Token { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">The massage.</param>
        /// <param name="token">The token that caused the exception.</param>
        public WqlParseException(string message, WqlToken token)
            : base(message)
        {
            Token = token;
        }
    }
}