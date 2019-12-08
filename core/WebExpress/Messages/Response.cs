namespace WebExpress.Messages
{
    /// <summary>
    /// siehe RFC 2616 Tz. 6
    /// </summary>
    public class Response
    {
        private const string VERSION = "HTTP/1.1";

        /// <summary>
        /// Setzt oder liefert die Optionen
        /// </summary>
        public ResponseHeaderFields HeaderFields { get; private set; }

        /// <summary>
        /// Setzt oder liefert den Content
        /// </summary>
        public object Content { get; set; }

        /// <summary>
        /// Liefert oder setzt den Statuscode
        /// </summary>
        public int Status { get; protected set; }

        /// <summary>
        /// Liefert oder setzt den Statustext
        /// </summary>
        protected string Reason { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        protected Response()
        {
            HeaderFields = new ResponseHeaderFields();
        }

        /// <summary>
        /// In Stringform umwandeln
        /// </summary>
        /// <returns>Der Header in Stringform</returns>
        public virtual string GetHeader()
        {
            return string.Format
            (
                "{0} {1} {2}\r\n{3}\r\n",
                VERSION,
                Status,
                Reason,
                HeaderFields.ToString()
            );
        }
    }
}
