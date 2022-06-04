using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.Uri;

namespace WebExpress.WebApp.WebNotificaation
{
    public static class NotificationManager
    {
        /// <summary>
        /// Liefert den Notfication-Speicher für globale Benachrichtigungern
        /// </summary>
        private static IDictionary<string, Notification> GlobalNotifications { get; } = new Dictionary<string, Notification>();

        /// <summary>
        /// Erstellt eine neue globale Benachrichtigung
        /// </summary>
        /// <param name="message">Die Benachrichtigungsnachricht</param>
        /// <param name="durability">Die Lebensdauer der Benachrichtigung. -1 für unbegrenzt gültig</param>
        /// <param name="heading">Die Überschrift</param>
        /// <param name="icon">Ein Icon</param>
        /// <param name="type">Der Benachrichtigungstype</param>
        /// <returns>Die erstellte Benachrichtigung</returns>
        public static Notification CreateNotification(string message, int durability = -1, string heading = null, IUri icon = null, TypeColorBackgroundAlert type = TypeColorBackgroundAlert.Light)
        {
            var notification = new Notification()
            {
                Message = message,
                Durability = durability,
                Heading = heading,
                Icon = icon?.ToString(),
                Type = type.ToClass()
            };

            if (!GlobalNotifications.ContainsKey(notification.ID))
            {
                // Globale Benachrichtigung
                lock (GlobalNotifications)
                {
                    GlobalNotifications.Add(notification.ID, notification);
                }
            }

            return notification;
        }

        /// <summary>
        /// Erstellt eine neue Benachrichtigung in der Session
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <param name="message">Die Benachrichtigungsnachricht</param>
        /// <param name="durability">Die Lebensdauer der Benachrichtigung. -1 für unbegrenzt gültig</param>
        /// <param name="heading">Die Überschrift</param>
        /// <param name="icon">Ein Icon</param>
        /// <param name="type">Der Benachrichtigungstype</param>
        /// <returns>Die erstellte Benachrichtigung</returns>
        public static Notification CreateNotification(Request request, string message, int durability = -1, string heading = null, IUri icon = null, TypeColorBackgroundAlert type = TypeColorBackgroundAlert.Light)
        {
            var notification = new Notification()
            {
                Message = InternationalizationManager.I18N(request, message),
                Durability = durability,
                Heading = InternationalizationManager.I18N(request, heading),
                Icon = icon?.ToString(),
                Type = type.ToClass()
            };

            // Benutzerbenachrichtigung
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
            var list = new List<Notification>();

            var scrapGlobal = GlobalNotifications.Values.Where(x => x.Durability >= 0 && x.Created.AddMilliseconds(x.Durability) < DateTime.Now).ToList();
            lock (GlobalNotifications)
            {
                // Abgelaufene Benachrichtigungen entfernen
                scrapGlobal.ForEach(x => GlobalNotifications.Remove(x.ID));
            }

            list.AddRange(GlobalNotifications.Values);

            if (request.Session.Properties.ContainsKey(typeof(SessionPropertyNotification)) &&
                request.Session.Properties[typeof(SessionPropertyNotification)] is SessionPropertyNotification notificationProperty)
            {
                var scrap = notificationProperty.Values.Where(x => x.Durability >= 0 && x.Created.AddMilliseconds(x.Durability) < DateTime.Now).ToList();

                lock (notificationProperty)
                {
                    // Abgelaufene Benachrichtigungen entfernen
                    scrap.ForEach(x => notificationProperty.Remove(x.ID));
                }


                list.AddRange(notificationProperty.Values);
            }

            return list;
        }

        /// <summary>
        /// Liefert alle Benachrichtigungen aus der Session
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <param name="id">Die BenachrichtigungsID</param>
        public static void RemoveNotification(Request request, string id)
        {
            if (GlobalNotifications.ContainsKey(id))
            {
                lock (GlobalNotifications)
                {
                    // Benachrichtigungen entfernen
                    GlobalNotifications.Remove(id);
                }
            }

            var scrapGlobal = GlobalNotifications.Values.Where(x => x.Durability >= 0 && x.Created.AddMilliseconds(x.Durability) < DateTime.Now).ToList();

            lock (GlobalNotifications)
            {
                // Abgelaufene Benachrichtigungen entfernen
                scrapGlobal.ForEach(x => GlobalNotifications.Remove(x.ID));
            }

            if (request.Session.Properties.ContainsKey(typeof(SessionPropertyNotification)) &&
                request.Session.Properties[typeof(SessionPropertyNotification)] is SessionPropertyNotification notificationProperty)
            {
                if (notificationProperty.ContainsKey(id))
                {
                    lock (notificationProperty)
                    {
                        // Benachrichtigungen entfernen
                        notificationProperty.Remove(id);
                    }
                }

                var scrap = notificationProperty.Values.Where(x => x.Created.AddMilliseconds(x.Durability) < DateTime.Now).ToList();

                lock (notificationProperty)
                {
                    // Abgelaufene Benachrichtigungen entfernen
                    scrap.ForEach(x => notificationProperty.Remove(x.ID));
                }
            }
        }
    }
}
