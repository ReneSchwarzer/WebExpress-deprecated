using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;

namespace WebExpress.Message
{
    /// <summary>
    /// siehe RFC 2616
    /// </summary>
    public class RequestHeaderFields
    {
        /// <summary>
        /// Der Host
        /// </summary>
        public string Host { get; private set; }

        /// <summary>
        /// Keep-Alive | close
        /// </summary>
        public string Connection { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Content-Länge
        /// </summary>
        public long ContentLength { get; private set; }

        /// <summary>
        /// Liefert oder setzt den Content-Typ
        /// </summary>
        public string ContentType { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Sprache des Content
        /// </summary>
        public string ContentLanguage { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Kodierung des Content
        /// </summary>
        public Encoding ContentEncoding { get; private set; }

        /// <summary>
        /// Liefert oder setzt den User-Educationen
        /// </summary>
        public string UserEducation { get; private set; }

        /// <summary>
        /// Liefert oder setzt den User-Agent
        /// </summary>
        public string UserAgent { get; private set; }

        /// <summary>
        /// Liefert oder setzt die erlaubten Medientypen
        /// </summary>
        public ICollection<string> Accept { get; private set; }

        /// <summary>
        /// Liefert oder setzt die erlaubten Endkodierungen
        /// </summary>
        public string AcceptEncoding { get; private set; }

        /// <summary>
        /// Liefert oder setzt die erlaubten Sprachen
        /// </summary>
        public ICollection<string> AcceptLanguage { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Zugangsdaten Name und Passwort
        /// </summary>
        public RequestAuthorization Authorization { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Cookies
        /// </summary>
        public CookieCollection Cookies { get; } = new CookieCollection();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="request">Der Request, welcher vom HttpListener erzeugt wurde</param>
        internal RequestHeaderFields(HttpListenerRequest request)
        {
            var headers = request.Headers;

            Host = headers["host"];
            Connection = headers["Connection"];
            ContentLanguage = headers["Content-Language"];
            UserEducation = headers["User-Education"];
            AcceptEncoding = headers["Accept-Encoding"];
           
            Authorization = RequestAuthorization.Parse(headers["Authorization"]);
                        
            Accept = request.AcceptTypes;
            ContentType = request.ContentType;
            ContentLength = request.ContentLength64;
            ContentEncoding = request.ContentEncoding;
            AcceptLanguage = request.UserLanguages;
            UserAgent = request.UserAgent;
            Cookies = request.Cookies;
        }
    }
}
