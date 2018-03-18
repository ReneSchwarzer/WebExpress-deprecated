using System;
using System.Collections.Generic;
using System.Text;

namespace WebExpress.Messages
{
    /// <summary>
    /// siehe RFC 2616 Tz. 6
    /// </summary>
    public class ResponseForbidden : Response
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ResponseForbidden()
        {
            var content = "<html><head><title>403</title></head><body>403 - Forbidden</body></html>";
            Status = 403;
            Reason = "Forbidden";

            HeaderFields.ContentType = "text/html";
            HeaderFields.ContentLength = content.Length;
            Content = content;
        }
    }
}
