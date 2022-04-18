using System.Collections;
using System.Linq;
using WebExpress.Message;
using WebExpress.WebAttribute;
using WebExpress.WebResource;
using WebExpress.WebTask;

namespace WebExpress.WebApp.WebAPI.V1
{
    /// <summary>
    /// Ermittelt den Status und Forschritt einer Aufgabe (WebTask)
    /// </summary>
    [ID("ApiTaskStatusV1")]
    [Segment("taskstatus", "")]
    [Path("/api/v1")]
    [IncludeSubPaths(true)]
    [Module("webexpress.webapp")]
    [Optional]
    public sealed class RestTaskStatus : ResourceRest
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public RestTaskStatus()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Eine Aufzählung, welche mittels JsonSerializer serialisiert werden kann.</returns>
        public override ICollection GetData(Request request)
        {
            var id = request.Uri.Path.Last().Value;

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
