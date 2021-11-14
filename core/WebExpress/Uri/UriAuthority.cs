using System;
using System.Linq;

namespace WebExpress.Uri
{
    /// <summary>
    /// Die Zuständigkeit (z.B. user@example.com:8080)
    /// </summary>
    public class UriAuthority
    {
        /// <summary>
        /// Die Benutzerinformation 
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Das Passwort
        /// wird in RFC 3986 (Abschnitt 3.2.1) als überholt bezeichnet 
        /// </summary>
        [ObsoleteAttribute("Die Eigenschaft ist veraltet (siehe RFC 3986 Abschnitt 3.2.1).", false)]
        public string Password { get; set; }

        /// <summary>
        /// Der HOST (z.B. example.com, 192.0.2.16:80)
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Der Port (z.B. example.com, 192.0.2.16:80)
        /// </summary>
        public int? Port { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public UriAuthority()
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="host">Der Host</param>
        public UriAuthority(string host)
        {
            Host = host;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="host">Der Host</param>
        /// <param name="port">Der Port</param>
        public UriAuthority(string host, int port)
        {
            Host = host;
            Port = port;
        }

        /// <summary>
        /// Wandelt die Uri in einen String um
        /// </summary>
        /// <returns>Die Stringrepräsentation der Uri</returns>
        public override string ToString()
        {
#pragma warning disable 618
            var userinfo = string.Join(":", new string[] { User, Password }.Where(x => !string.IsNullOrWhiteSpace(x)));
#pragma warning restore 618

            var adress = string.Join(":", new string[] { Host, Port?.ToString() }.Where(x => !string.IsNullOrWhiteSpace(x)));

            return "//" + string.Join("@", new string[] { userinfo, adress }.Where(x => !string.IsNullOrWhiteSpace(x)));
        }
    }
}