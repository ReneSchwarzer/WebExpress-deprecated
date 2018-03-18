using System;
using System.Collections.Generic;
using System.Text;

namespace WebExpress.Messages
{
    /// <summary>
    /// siehe RFC 2616 Tz. 6
    /// </summary>
    public class ResponseNotFound : Response
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ResponseNotFound()
        {
            var content = "<html><head><title>404</title></head><body>404 - Not Found</body></html>";
            Status = 404;
            Reason = "Not Found";

            HeaderFields.ContentType = "text/html";
            HeaderFields.ContentLength = content.Length;
            Content = content;
        }
    }
}
