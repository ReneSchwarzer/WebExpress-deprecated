using System;

namespace WebExpress.WebApp.Wql
{
    public class WqlParseException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">The massage.</param>
        public WqlParseException(string message)
            : base(message)
        {
        }
    }
}