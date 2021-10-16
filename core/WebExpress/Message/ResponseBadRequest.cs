﻿namespace WebExpress.Message
{
    /// <summary>
    /// siehe RFC 2616 Tz. 6
    /// </summary>
    public class ResponseBadRequest : Response
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ResponseBadRequest()
        {
            var content = "<html><head><title>404</title></head><body>404 - Bad Request</body></html>";
            Status = 400;
            Reason = "Bad Request";

            HeaderFields.ContentType = "text/html";
            HeaderFields.ContentLength = content.Length;
            Content = content;
        }
    }
}
