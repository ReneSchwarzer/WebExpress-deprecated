using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace WebExpress.WebMessage
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
        public IEnumerable<string> AcceptLanguage { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Zugangsdaten Name und Passwort
        /// </summary>
        public RequestAuthorization Authorization { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Cookies
        /// </summary>
        public ICollection<Cookie> Cookies { get; } = new List<Cookie>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contextFeatures">Anfänglicher Satz von Features.</param>
        internal RequestHeaderFields(IFeatureCollection contextFeatures)
        {
            var requestFeature = contextFeatures.Get<IHttpRequestFeature>();

            Host = requestFeature.Headers.Host;
            Connection = requestFeature.Headers.Connection;
            ContentType = requestFeature.Headers.ContentType;
            ContentLength = requestFeature.Headers.ContentLength ?? 0;
            ContentLanguage = requestFeature.Headers.ContentLanguage;
            ContentEncoding = requestFeature.Headers.ContentEncoding.Any() ? Encoding.GetEncoding(requestFeature.Headers.ContentEncoding) : Encoding.Default;
            Accept = requestFeature.Headers.Accept;
            AcceptEncoding = requestFeature.Headers.AcceptEncoding;
            AcceptLanguage = requestFeature.Headers.AcceptLanguage.SelectMany(x => x.Split(';', StringSplitOptions.RemoveEmptyEntries));
            UserAgent = requestFeature.Headers.UserAgent;

            foreach (var cookie in requestFeature.Headers.Cookie)
            {
                var split = cookie.Split('=');
                var key = split[0];
                var value = split[1];

                Cookies.Add(new Cookie(key, value));
            }

            Authorization = RequestAuthorization.Parse(requestFeature.Headers.Authorization);
        }
    }
}
