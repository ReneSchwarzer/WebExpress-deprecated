namespace WebExpress.Message
{
    /// <summary>
    /// siehe RFC 2616 Tz. 6
    /// </summary>
    public class ResponseOK : Response
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ResponseOK()
        {
            Status = 200;
            Reason = "OK";
        }
    }
}
