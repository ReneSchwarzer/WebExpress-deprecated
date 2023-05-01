using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Internationalization;
using WebExpress.WebMessage;

namespace WebExpress.WebApp.WebNotificaation
{
    public static class NotificationManager
    {
        /// <summary>
        /// Provides the notification store for global notifications.
        /// </summary>
        private static IDictionary<string, Notification> GlobalNotifications { get; } = new Dictionary<string, Notification>();

        /// <summary>
        /// Creates a new global notification.
        /// </summary>
        /// <param name="message">The notification message.</param>
        /// <param name="durability">The lifetime of the notification. -1 for indefinite validity.</param>
        /// <param name="heading">The headline.</param>
        /// <param name="icon">An icon.</param>
        /// <param name="type">The notification type.</param>
        /// <returns>The created notification.</returns>
        public static Notification CreateNotification(string message, int durability = -1, string heading = null, string icon = null, TypeNotification type = TypeNotification.Light)
        {
            var notification = new Notification()
            {
                Message = message,
                Durability = durability,
                Heading = heading,
                Icon = icon,
                Type = type
            };

            if (!GlobalNotifications.ContainsKey(notification.ID))
            {
                // global notification
                lock (GlobalNotifications)
                {
                    GlobalNotifications.Add(notification.ID, notification);
                }
            }

            return notification;
        }

        /// <summary>
        /// Creates a new notification in the session.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="message">The notification message.</param>
        /// <param name="durability">The lifetime of the notification. -1 for indefinite validity.</param>
        /// <param name="heading">The headline.</param>
        /// <param name="icon">An icon.</param>
        /// <param name="type">The notification type.</param>
        /// <returns>The created notification.</returns>
        public static Notification CreateNotification(Request request, string message, int durability = -1, string heading = null, string icon = null, TypeNotification type = TypeNotification.Light)
        {
            var notification = new Notification()
            {
                Message = InternationalizationManager.I18N(request, message),
                Durability = durability,
                Heading = InternationalizationManager.I18N(request, heading),
                Icon = icon?.ToString(),
                Type = type
            };

            // user Notification
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
        /// Returns all notifications from the session.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>An enumeration of the notifications.</returns>
        public static IEnumerable<Notification> GetNotifications(Request request)
        {
            var list = new List<Notification>();

            var scrapGlobal = GlobalNotifications.Values.Where(x => x.Durability >= 0 && x.Created.AddMilliseconds(x.Durability) < DateTime.Now).ToList();
            lock (GlobalNotifications)
            {
                // remove expired notifications
                scrapGlobal.ForEach(x => GlobalNotifications.Remove(x.ID));
            }

            list.AddRange(GlobalNotifications.Values);

            if (request.Session.Properties.ContainsKey(typeof(SessionPropertyNotification)) &&
                request.Session.Properties[typeof(SessionPropertyNotification)] is SessionPropertyNotification notificationProperty)
            {
                var scrap = notificationProperty.Values.Where(x => x.Durability >= 0 && x.Created.AddMilliseconds(x.Durability) < DateTime.Now).ToList();

                lock (notificationProperty)
                {
                    // remove expired notifications
                    scrap.ForEach(x => notificationProperty.Remove(x.ID));
                }


                list.AddRange(notificationProperty.Values);
            }

            return list;
        }

        /// <summary>
        /// Returns all notifications from the session.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="id">The notification id.</param>
        public static void RemoveNotification(Request request, string id)
        {
            if (GlobalNotifications.ContainsKey(id))
            {
                lock (GlobalNotifications)
                {
                    // remove notifications
                    GlobalNotifications.Remove(id);
                }
            }

            var scrapGlobal = GlobalNotifications.Values.Where(x => x.Durability >= 0 && x.Created.AddMilliseconds(x.Durability) < DateTime.Now).ToList();

            lock (GlobalNotifications)
            {
                // remove expired notifications
                scrapGlobal.ForEach(x => GlobalNotifications.Remove(x.ID));
            }

            if (request.Session.Properties.ContainsKey(typeof(SessionPropertyNotification)) &&
                request.Session.Properties[typeof(SessionPropertyNotification)] is SessionPropertyNotification notificationProperty)
            {
                if (notificationProperty.ContainsKey(id))
                {
                    lock (notificationProperty)
                    {
                        // remove notifications
                        notificationProperty.Remove(id);
                    }
                }

                var scrap = notificationProperty.Values.Where(x => x.Created.AddMilliseconds(x.Durability) < DateTime.Now).ToList();

                lock (notificationProperty)
                {
                    // remove expired notifications
                    scrap.ForEach(x => notificationProperty.Remove(x.ID));
                }
            }
        }
    }
}
