using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Internationalization;
using WebExpress.WebComponent;
using WebExpress.WebMessage;
using WebExpress.WebPlugin;

namespace WebExpress.WebApp.WebNotificaation
{
    public sealed class NotificationManager : IComponent
    {
        /// <summary>
        /// An event that fires when an notification is created.
        /// </summary>
        public event EventHandler<Notification> CreateNotification;

        /// <summary>
        /// An event that fires when an notification is destroyed.
        /// </summary>
        public event EventHandler<Notification> DestroyNotification;

        /// <summary>
        /// Returns the reference to the context of the host.
        /// </summary>
        public IHttpServerContext HttpServerContext { get; private set; }

        /// <summary>
        /// Provides the notification store for global notifications.
        /// </summary>
        private static IDictionary<string, Notification> GlobalNotifications { get; } = new Dictionary<string, Notification>();

        /// <summary>
        /// Constructor
        /// </summary>
        internal NotificationManager()
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The reference to the context of the host.</param>
        public void Initialization(IHttpServerContext context)
        {
            HttpServerContext = context;

            HttpServerContext.Log.Debug
            (
                InternationalizationManager.I18N("webexpress.webapp:notificationmanager.initialization")
            );
        }

        /// <summary>
        /// Creates a new global notification.
        /// </summary>
        /// <param name="message">The notification message.</param>
        /// <param name="durability">The lifetime of the notification. -1 for indefinite validity.</param>
        /// <param name="heading">The headline.</param>
        /// <param name="icon">An icon.</param>
        /// <param name="type">The notification type.</param>
        /// <returns>The created notification.</returns>
        public Notification AddNotification(string message, int durability = -1, string heading = null, string icon = null, TypeNotification type = TypeNotification.Light)
        {
            var notification = new Notification()
            {
                Message = message,
                Durability = durability,
                Heading = heading,
                Icon = icon,
                Type = type
            };

            if (!GlobalNotifications.ContainsKey(notification.Id))
            {
                // global notification
                lock (GlobalNotifications)
                {
                    GlobalNotifications.Add(notification.Id, notification);
                }
            }

            OnCreateNotification(notification);

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
        public Notification AddNotification(Request request, string message, int durability = -1, string heading = null, string icon = null, TypeNotification type = TypeNotification.Light)
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

            if (!notificationProperty.ContainsKey(notification.Id))
            {
                lock (notificationProperty)
                {
                    notificationProperty.Add(notification.Id, notification);
                }
            }

            OnCreateNotification(notification);

            return notification;
        }

        /// <summary>
        /// Returns all notifications from the session.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>An enumeration of the notifications.</returns>
        public IEnumerable<Notification> GetNotifications(Request request)
        {
            var list = new List<Notification>();

            var scrapGlobal = GlobalNotifications.Values.Where(x => x.Durability >= 0 && x.Created.AddMilliseconds(x.Durability) < DateTime.Now).ToList();
            lock (GlobalNotifications)
            {
                // remove expired notifications
                scrapGlobal.ForEach(x => GlobalNotifications.Remove(x.Id));
            }

            list.AddRange(GlobalNotifications.Values);

            if (request.Session.Properties.ContainsKey(typeof(SessionPropertyNotification)) &&
                request.Session.Properties[typeof(SessionPropertyNotification)] is SessionPropertyNotification notificationProperty)
            {
                var scrap = notificationProperty.Values.Where(x => x.Durability >= 0 && x.Created.AddMilliseconds(x.Durability) < DateTime.Now).ToList();

                lock (notificationProperty)
                {
                    // remove expired notifications
                    scrap.ForEach(x => notificationProperty.Remove(x.Id));
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
        public void RemoveNotification(Request request, string id)
        {
            if (GlobalNotifications.ContainsKey(id))
            {
                lock (GlobalNotifications)
                {
                    OnDestroyNotification(GlobalNotifications[id]);

                    // remove notifications
                    GlobalNotifications.Remove(id);
                }
            }

            var scrapGlobal = GlobalNotifications.Values.Where(x => x.Durability >= 0 && x.Created.AddMilliseconds(x.Durability) < DateTime.Now).ToList();

            lock (GlobalNotifications)
            {
                // remove expired notifications
                scrapGlobal.ForEach(x =>
                {
                    OnDestroyNotification(GlobalNotifications[x.Id]);

                    GlobalNotifications.Remove(x.Id);
                });
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
                    scrap.ForEach(x =>
                    {
                        OnDestroyNotification(notificationProperty[x.Id]);

                        notificationProperty.Remove(x.Id);
                    });
                }
            }
        }

        /// <summary>
        /// Raises the CreateNotification event.
        /// </summary>
        /// <param name="notification">The notification.</param>
        private void OnCreateNotification(Notification notification)
        {
            CreateNotification?.Invoke(this, notification);
        }

        /// <summary>
        /// Raises the DestroyNotification event.
        /// </summary>
        /// <param name="notification">The notification.</param>
        private void OnDestroyNotification(Notification notification)
        {
            DestroyNotification?.Invoke(this, notification);
        }

        /// <summary>
        /// Information about the component is collected and prepared for output in the log.
        /// </summary>
        /// <param name="pluginContext">The context of the plugin.</param>
        /// <param name="output">A list of log entries.</param>
        /// <param name="deep">The shaft deep.</param>
        public void PrepareForLog(IPluginContext pluginContext, IList<string> output, int deep)
        {
        }
    }
}
