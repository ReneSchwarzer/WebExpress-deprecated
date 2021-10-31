using WebExpress.Attribute;
using WebExpress.Message;
using WebExpress.WebResource;
using WebExpress.WebTask;

namespace WebExpress.WebApp.WebAPI
{
    /// <summary>
    /// Ermittelt den Status und Forschritt einer Aufgabe (WebTask)
    /// </summary>
    [ID("TaskStatusV1")]
    [Segment("taskstatus", "")]
    [Path("/api/v1")]
    [Module("webexpress.webapp")]
    [Optional]
    public sealed class APITaskStatusV1 : ResourceApi
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public APITaskStatusV1()
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
        /// <returns>Ein Objekt welches mittels JsonSerializer serialisiert werden kann.</returns>
        public override object GetData(Request request)
        {
            var id = $"inventoryexpress_export_{request.Session.ID}";

            if (TaskManager.ContainsTask(id))
            {
                var task = TaskManager.GetTask(id);

                return new
                {
                    ID = id,
                    State = task.State,
                    Progress = task.Progress
                };
            }

            return new
            {
                ID = id,
                State = null as string,
                Progress = 0
            };
        }
    }
}
