using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Message;

namespace WebExpress.WebApp.WebNotificaation
{
    public static class NotificationManager
    {
        /// <summary>
        /// Erstellt eine neue Benachrichtigung in der Session
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <param name="message">Die Benachrichtigungsnachricht</param>
        /// <param name="durability">Die Lebensdauer der Benachrichtigung. -1 für unbegrenzt gültig</param>
        /// <returns>Die erstellte Benachrichtigung</returns>
        public static Notification CreateNotification(Request request, string message, int durability = -1)
        {
            var notification = new Notification() { Message = message, Durability = durability };

            if (!request.Session.Properties.ContainsKey(typeof(SessionPropertyNotification)))
            {
                request.Session.Properties.Add(typeof(SessionPropertyNotification), new SessionPropertyNotification());
            }

            var notificationProperty = request.Session.Properties[typeof(SessionPropertyNotification)] as SessionPropertyNotification;

            if (!notificationProperty.ContainsKey(notification.ID))
            {
                lock (notificationProperty)
                {
                    notificationProperty.Add(notification.ID, notification);
                }
            }

            return notification;
        }

        /// <summary>
        /// Liefert alle Benachrichtigungen aus der Session
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Benachrichtigungen</returns>
        public static ICollection<Notification> GetNotifications(Request request)
        {
            if (request.Session.Properties.ContainsKey(typeof(SessionPropertyNotification)) &&
                request.Session.Properties[typeof(SessionPropertyNotification)] is SessionPropertyNotification notificationProperty)
            {
                var scrap = notificationProperty.Values.Where(x => x.Created.AddMilliseconds(x.Durability) < DateTime.Now).ToList();

                lock (notificationProperty)
                {
                    // Abgelaufene Benachrichtigungen entfernen
                    scrap.ForEach(x => notificationProperty.Remove(x.ID));
                }

                return notificationProperty.Values;
            }

            return new List<Notification>();
        }

        /// <summary>
        /// Liefert alle Benachrichtigungen aus der Session
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <param name="id">Die BenachrichtigungsID</param>
        public static void RemoveNotification(Request request, string id)
        {
            if (request.Session.Properties.ContainsKey(typeof(SessionPropertyNotification)) &&
                request.Session.Properties[typeof(SessionPropertyNotification)] is SessionPropertyNotification notificationProperty)
            {
                var scrap = notificationProperty.Values.Where(x => x.Created.AddMilliseconds(x.Durability) < DateTime.Now).ToList();

                lock (notificationProperty)
                {
                    // Abgelaufene Benachrichtigungen entfernen
                    scrap.ForEach(x => notificationProperty.Remove(x.ID));
                }

                lock (notificationProperty)
                {
                    // Abgelaufene Benachrichtigungen entfernen
                    notificationProperty.Remove(id);
                }
            }
        }
    }
}
