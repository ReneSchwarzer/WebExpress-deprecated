using WebExpress.Message;
using WebExpress.WebUri;
using WebExpress.WebApplication;
using WebExpress.WebModule;
using WebExpress.WebResource;

namespace WebExpress.WebResponse
{
    /// <summary>
    /// Statusseite
    /// </summary>
    public interface IStatusPage
    {
        /// <summary>
        /// Returns the resource ID.
        /// </summary>
        string ID { get; }

        /// <summary>
        /// Returns the context of the application.
        /// </summary>
        IApplicationContext ApplicationContext { get; }

        /// <summary>
        /// Returns the context of the module.
        /// </summary>
        IModuleContext ModuleContext { get; }

        /// <summary>
        /// Returns the uri of the resource.
        /// </summary>
        IUri Uri { get; }

        /// <summary>
        /// Liefert oder setzt den Statuscode
        /// </summary>
        int StatusCode { get; set; }

        /// <summary>
        /// Liefert oder setzt die Stausnachricht
        /// </summary>
        string StatusTitle { get; set; }

        /// <summary>
        /// Liefert oder setzt die Stausnachricht
        /// </summary>
        string StatusMessage { get; set; }

        /// <summary>
        /// Liefert oder setzt das Statusicon
        /// </summary>
        IUri StatusIcon { get; set; }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="resourceContext">The context of the resource.</param>
        void Initialization(IResourceContext resourceContext);

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The response.</returns>
        Response Process(Request request);
    }
}
