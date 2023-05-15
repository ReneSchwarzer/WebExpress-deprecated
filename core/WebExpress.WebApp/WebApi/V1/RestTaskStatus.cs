﻿using System.Collections;
using System.Linq;
using WebExpress.WebAttribute;
using WebExpress.WebMessage;
using WebExpress.WebResource;
using WebExpress.WebTask;

namespace WebExpress.WebApp.WebAPI.V1
{
    /// <summary>
    /// Determines the status and progress of a task (WebTask).
    /// </summary>
    [WebExSegment("taskstatus", "")]
    [WebExContextPath("/api/v1")]
    [WebExIncludeSubPaths(true)]
    [WebExModule(typeof(Module))]
    [WebExOptional]
    public sealed class RestTaskStatus : ResourceRest
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RestTaskStatus()
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
        /// Processing of the resource.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>An enumeration that can be serialized using the JsonSerializer.</returns>
        public override ICollection GetData(Request request)
        {
            var id = request.Uri.PathSegments.Last().Value;

            if (TaskManager.ContainsTask(id))
            {
                var task = TaskManager.GetTask(id);

                return new object[]
                {
                    new
                    {
                        ID = id,
                        task.State,
                        task.Progress,
                        task.Message
                    }
                };
            }

            return new object[]
            {
                new
                {
                    ID = id,
                    State = null as string,
                    Progress = 0
                }
            };
        }
    }
}
