namespace WebExpress.Message
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
            //Content = "<html></html>";

            //HeaderFields.ContentType = "text/html";
            //HeaderFields.ContentLength = Content.ToString().Length;
            HeaderFields.Location = location;
        }
    }
}
