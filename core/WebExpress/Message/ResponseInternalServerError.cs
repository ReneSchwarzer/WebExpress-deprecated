namespace WebExpress.Message
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
            var content = "<html><head><title>404</title></head><body>500 - Internal Server Error</body></html>";
            Status = 500;
            Reason = "Internal Server Error";

            HeaderFields.ContentType = "text/html";
            HeaderFields.ContentLength = content.Length;
            Content = content;
        }
    }
}
