using System.Collections.Generic;
using WebExpress.WebSession;

namespace WebExpress.WebApp.WebNotificaation
{
    /// <summary>
    /// Sammlung der Nachrichten
    /// Key = BenachrichtigungsId
    /// Value = Benachrichtigung
    /// </summary>
    public class SessionPropertyNotification : Dictionary<string, Notification>, ISessionProperty
    {
    }
}
