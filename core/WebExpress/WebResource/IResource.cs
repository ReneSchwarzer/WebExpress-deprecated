using WebExpress.Internationalization;
using WebExpress.WebApplication;
using WebExpress.WebMessage;
using WebExpress.WebModule;
using WebExpress.WebUri;

namespace WebExpress.WebResource
{
    public interface IResource : II18N
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
        /// Returns the module context where the resource exists.
        /// </summary>
        IResourceContext ResourceContext { get; }

        /// <summary>
        /// Returns the uri of the resource.
        /// </summary>
        UriResource Uri { get; }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="resourceContext">The context of the resource.</param>
        void Initialization(IResourceContext resourceContext);

        /// <summary>
        /// Preprocessing of the resource.
        /// </summary>
        /// <param name="request">The request.</param>
        void PreProcess(Request request);

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The response.</returns>
        Response Process(Request request);

        /// <summary>
        /// Post-processing of the resource.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="response">The response.</param>
        /// <returns>The response.</returns>
        Response PostProcess(Request request, Response response);
    }
}
