using System;
using System.Collections.Generic;
using System.Text;

namespace WebExpress.Messages
{
    /// <summary>
    /// siehe RFC 2616 Tz. 6
    /// </summary>
    public class ResponseInternalServerError : Response
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ResponseInternalServerError()
        {
            Status = 500;
            Reason = "Internal Server Error";
        }
    }
}
