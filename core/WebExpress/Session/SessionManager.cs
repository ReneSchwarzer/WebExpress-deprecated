using System;
using System.Linq;
using WebExpress.Message;

namespace WebExpress.Session
{
    public class SessionManager
    {
        /// <summary>
        /// Liefert oder setzt das Verzeichnis, indem die Sessions serverseitig gespeichert sind
        /// </summary>
        private static SessionDictionary Dictionary { get; } = new SessionDictionary();

        /// <summary>
        /// Erstellt eine Session oder gibt eine bestehende Session zurück
        /// </summary>
        /// <param name="request">Der Request</param>
        /// <returns>Die Session</returns>
        public static Session GetSession(Request request)
        {
            var session = null as Session;

            // Session ermitteln
            var sessionCookie = request?.HeaderFields
                .Cookies?.Where(x => x.Name.Equals("session", StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            Guid guid = Guid.NewGuid();

            try
            {
                guid = Guid.Parse(sessionCookie?.Value);
            }
            catch
            {

            }

            if (sessionCookie != null && Dictionary.ContainsKey(guid))
            {
                session = Dictionary[guid];
                session.Updated = DateTime.Now;
            }
            else
            {
                // keine oder ungültige Session => Neue Session vergeben
                session = new Session(guid);

                lock (Dictionary)
                {
                    Dictionary[guid] = session;
                }
            }

            return session;
        }
    }
}
