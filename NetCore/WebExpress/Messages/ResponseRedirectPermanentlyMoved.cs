using System;
using System.Collections.Generic;
using System.Text;

namespace WebExpress.Messages
{
    /// <summary>
    /// siehe RFC 2616 Tz. 6
    /// </summary>
    public class ResponseRedirectPermanentlyMoved : Response
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ResponseRedirectPermanentlyMoved(string location)
        {
            Status = 301;
            Reason = "permanently moved";

            HeaderFields.Location = location;
        }
    }
}
