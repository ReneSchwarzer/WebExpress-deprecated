using System.Collections.Generic;
using WebExpress.Session;

namespace WebExpress.WebApp.WebNotificaation
{
    /// <summary>
    /// Sammlung der Nachrichten
    /// Key = BenachrichtigungsID
    /// Value = Benachrichtigung
    /// </summary>
    public class SessionPropertyNotification : Dictionary<string, Notification>, ISessionProperty
    {
    }
}
