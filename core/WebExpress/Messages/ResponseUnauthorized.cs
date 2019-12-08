namespace WebExpress.Messages
{
    /// <summary>
    /// siehe RFC 2616 Tz. 6
    /// </summary>
    public class ResponseUnauthorized : Response
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ResponseUnauthorized()
        {
            Status = 401;
            Reason = "OK";

            HeaderFields.WWWAuthenticate = true;
        }
    }
}
