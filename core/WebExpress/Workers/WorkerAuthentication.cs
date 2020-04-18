using WebExpress.Messages;
using WebExpress.Pages;

namespace WebExpress.Workers
{
    /// <summary>
    /// Arbeitet eine Anfrage ab. Dies erfolgt nebenläufig
    /// </summary>
    public abstract class WorkerAuthentication : WorkerSession
    {
        /// <summary>
        /// Liefert oder setzt den Atorisationsservice
        /// </summary>
        public AuthorizationService AuthorizationService { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="url">Die URL</param>
        public WorkerAuthentication(UriPage url)
            : base(url)
        {
        }

        /// <summary>
        /// Vorverarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Antwort aus der Vorverarbeitung oder null</returns>
        public override Response PreProcess(Request request)
        {
            var response = base.PreProcess(request);
            if (response != null || AuthorizationService == null)
            {
                return response;
            }

            var session = CurrentSession;

            if (session == null)
            {
                return null;
            }

            var authentification = session.GetProperty<SessionPropertyAuthentification>();

            if (authentification == null && request.HeaderFields.Authorization == null)
            {
                return new ResponseUnauthorized();
            }
            else if (authentification == null && request.HeaderFields.Authorization != null)
            {
                session.SetProperty(new SessionPropertyAuthentification()
                {
                    Identification = request.HeaderFields.Authorization.Identification,
                    Password = request.HeaderFields.Authorization.Password
                });
            }

            var authorization = session.GetProperty<SessionPropertyAuthorization>();

            if (authorization == null)
            {
                if (AuthorizationService.Authorization(session))
                {
                    session.SetProperty(new SessionPropertyAuthorization());
                }
                else
                {
                    Logout();

                    return new ResponseForbidden();
                }
            }

            return null;
        }

        /// <summary>
        /// Meldet den Nutzer ab
        /// </summary>
        protected virtual void Logout()
        {
            var session = CurrentSession;

            if (session == null)
            {
                return;
            }

            session.RemoveProperty<SessionPropertyAuthentification>();
            session.RemoveProperty<SessionPropertyAuthorization>();
        }
    }
}
