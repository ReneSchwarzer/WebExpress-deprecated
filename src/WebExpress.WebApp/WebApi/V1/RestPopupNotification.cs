﻿using System.Collections;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebMessage;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebAPI.V1
{
    /// <summary>
    /// Returns the status and progress of a task (WebTask).
    /// </summary>
    [Segment("popupnotifications", "")]
    [ContextPath("/api/v1")]
    [Module<Module>]
    [IncludeSubPaths(true)]
    [Optional]
    public sealed class RestPopupNotification : ResourceRest
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RestPopupNotification()
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Processing of the resource. des GET-Request
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>An enumeration that can be serialized using the JsonSerializer.</returns>
        public override ICollection GetData(Request request)
        {
            return (ICollection)ComponentManager.GetComponent<NotificationManager>()?.GetNotifications(request);
        }

        /// <summary>
        /// Processing of the resource. des DELETE-Request
        /// </summary>
        /// <param name="id">The id to delete.</param>
        /// <param name="request">The request.</param>
        /// <returns>The result of the deletion.</returns>
        public override bool DeleteData(string id, Request request)
        {
            ComponentManager.GetComponent<NotificationManager>()?.RemoveNotification(request, id);

            return true;
        }
    }
}
