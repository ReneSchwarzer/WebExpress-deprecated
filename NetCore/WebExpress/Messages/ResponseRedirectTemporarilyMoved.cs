using System;
using System.Collections.Generic;
using System.Text;

namespace WebExpress.Messages
{
    /// <summary>
    /// siehe RFC 2616 Tz. 6
    /// </summary>
    public class ResponseRedirectTemporarilyMoved : Response
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ResponseRedirectTemporarilyMoved(string location)
        {
            Status = 302;
            Reason = "temporarily moved";

            HeaderFields.Location = location;
        }
    }
}
