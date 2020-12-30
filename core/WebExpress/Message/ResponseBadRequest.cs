namespace WebExpress.Message
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
            Status = 400;
            Reason = "Bad Request";
        }
    }
}
