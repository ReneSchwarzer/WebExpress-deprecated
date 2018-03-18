using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Messages;
using WebExpress.Pages;

namespace WebExpress.Workers
{
    /// <summary>
    /// Arbeitet eine Anfrage ab. Dies erfolgt nebenläufig
    /// </summary>
    public abstract class WorkerSession : Worker
    {
        /// <summary>
        /// Liefert oder setzt den SessionManager
        /// </summary>
        private static Dictionary<Guid, Session> SessionManager { get; set; }

        /// <summary>
        /// Liefert oder setzt die aktuelle Session
        /// </summary>
        public Session CurrentSession { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="url">Die URL</param>
        public WorkerSession(Path url)
            : base(url)
        {
            if (SessionManager == null)
            {
                SessionManager = new Dictionary<Guid, Session>();
            }
        }

        /// <summary>
        /// Vorverarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Antwort aus der Vorverarbeitung oder null</returns>
        public override Response PreProcess(Request request)
        {
            Session session = null;

            // Session ermitteln
            var sessionCookie = request.HeaderFields.Cookies == null ? null :
                            (from c in request.HeaderFields.Cookies
                             where c.Name.Equals("session", StringComparison.OrdinalIgnoreCase)
                             select c).FirstOrDefault();

            Guid guid = Guid.NewGuid();
            try
            {
                guid = Guid.Parse(sessionCookie.Value);
            }
            catch
            {

            }

            if (sessionCookie != null && SessionManager.ContainsKey(guid))
            {
                session = SessionManager[guid];
            }
            else
            {
                // keine oder ungültige Session => Neue Session vergeben
                session = new Session(guid);

                SetSession(session);
            }

            CurrentSession = session;

            return null;
        }

        /// <summary>
        /// Vorverarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <param name="response">Die Antwort</param>
        /// <returns>Die Antwort</returns>
        public override Response PostProcess(Request request, Response response)
        {
            Session session = CurrentSession;

            response.HeaderFields.AddCustomHeader("Set-Cookie", "session=" + session.ID);

            return response;
        }

        /// <summary>
        /// Liefert die Session
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        private Session GetSession(Guid guid)
        {
            if (SessionManager == null)
            {
                SessionManager = new Dictionary<Guid, Session>();
            }

            if (!SessionManager.ContainsKey(guid))
            {
                return null;
            }

            return SessionManager[guid];
        }

        /// <summary>
        /// Liefert die Session
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        private void SetSession(Session session)
        {
            if (SessionManager == null)
            {
                SessionManager = new Dictionary<Guid, Session>();
            }

            if (!SessionManager.ContainsKey(session.ID))
            {
                SessionManager.Add(session.ID, session);

                return;
            }

            SessionManager[session.ID] = session;
        }
    }
}
