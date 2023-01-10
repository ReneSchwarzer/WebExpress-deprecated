﻿using System.Globalization;
using WebExpress.Message;
using WebExpress.Uri;
using WebExpress.WebApplication;
using WebExpress.WebModule;

namespace WebExpress.WebResource
{
    public abstract class Resource : IResource
    {
        /// <summary>
        /// Returns the resource id.
        /// </summary>
        public string ID { get; internal set; }

        /// <summary>
        /// Returns the context of the application.
        /// </summary>
        public IApplicationContext ApplicationContext { get; internal set; }

        /// <summary>
        /// Returns the context of the module.
        /// </summary>
        public IModuleContext ModuleContext { get; internal set; }

        /// <summary>
        /// Returns the module context where the resource exists.
        /// </summary>
        public IResourceContext ResourceContext { get; private set; }

        /// <summary>
        /// Returns the uri of the resource.
        /// </summary>
        public IUriResource Uri { get; internal set; }

        /// <summary>
        /// Returns the context path.
        /// </summary>
        public IUri ContextPath => ModuleContext?.GetContextPath(ApplicationContext);

        /// <summary>
        /// Provides the culture.
        /// </summary>
        public CultureInfo Culture { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Resource()
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="resourceContext">The context of the resource.</param>
        public virtual void Initialization(IResourceContext resourceContext)
        {
            ResourceContext = resourceContext;
        }

        /// <summary>
        /// Preprocessing of the resource.
        /// </summary>
        /// <param name="request">The request.</param>
        public virtual void PreProcess(Request request)
        {
            return;
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The response.</returns>
        public abstract Response Process(Request request);

        /// <summary>
        /// Post-processing of the resource.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="response">The response.</param>
        /// <returns>The response.</returns>
        public virtual Response PostProcess(Request request, Response response)
        {
            return response;
        }
    }
}
