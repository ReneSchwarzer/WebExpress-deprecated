using System;
using System.Collections.Generic;
using WebExpress.Internationalization;
using WebExpress.WebApplication;
using WebExpress.WebModule;
using WebExpress.WebResource;

namespace WebExpress.WebSitemap
{
    /// <summary>
    /// Represents the search result within the site map.
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// Returns the resource id.
        /// </summary>
        public string ID { get; internal set; }

        /// <summary>
        /// Returns the resource title.
        /// </summary>
        public string Title { get; internal set; }

        /// <summary>
        /// Returns the resource type (calss).
        /// </summary>
        public Type Type { get; internal set; }

        /// <summary>
        /// Returns the instance
        /// </summary>
        public IResource Instance { get; internal set; }

        /// <summary>
        /// Returns the context of the application.
        /// </summary>
        public IApplicationContext ApplicationContext { get; internal set; }

        /// <summary>
        /// Returns the context of the module.
        /// </summary>
        public IModuleContext ModuleContext { get; internal set; }

        /// <summary>
        /// Returns the context of the resource.
        /// </summary>
        public IResourceContext Context { get; internal set; }

        /// <summary>
        /// Returns the context where the resource exists.
        /// </summary>
        public IReadOnlyList<string> ResourceContextFilter { get; internal set; }

        /// <summary>
        /// Returns the path.
        /// </summary>
        /// <returns>The path.</returns>
        public ICollection<SitemapNode> Path { get; internal set; }

        /// <summary>
        /// Returns the variables.
        /// </summary>
        public IDictionary<string, string> Variables { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The resource id.</param>
        /// <param name="title">The resource title.</param>
        /// <param name="type">The resource type.</param>
        /// <param name="instance">The instance.</param>
        /// <param name="applicationContext">The context of the application.</param>
        /// <param name="moduleContext">The context of the module.</param>
        /// <param name="context">The context of the resource.</param>
        /// <param name="resourceContext">The context where the resource exists.</param>
        /// <param name="path">The path.</param>
        /// <param name="variables">Variable-value pairs</param>
        public SearchResult
        (
            string id,
            string title,
            Type type,
            IResource instance, 
            IApplicationContext applicationContext,
            IModuleContext moduleContext,
            IResourceContext context,
            IReadOnlyList<string> resourceContext,
            ICollection<SitemapNode> path,
            IDictionary<string, string> variables
        )
        {
            ID = id;
            Title = title;
            Type = type;
            Instance = instance;
            ApplicationContext = applicationContext;
            ModuleContext = moduleContext;
            Context = context;
            ResourceContextFilter = resourceContext;
            Path = path;

            AddVariables(variables);
        }

        /// <summary>
        /// Adds the variable-value pairs to the result.
        /// </summary>
        /// <param name="variables">The variable-value pairs.</param>
        public void AddVariables(IDictionary<string, string> variables)
        {
            if (variables != null)
            {
                foreach (var v in variables)
                {
                    if (!Variables.ContainsKey(v.Key))
                    {
                        Variables.Add(v.Key, v.Value);
                    }
                    else
                    {
                        Context.Log.Warning(message: InternationalizationManager.I18N(InternationalizationManager.DefaultCulture, "webexpress", "resource.variable.duplicate"), args: v.Key);
                    }
                }
            }
        }
    }
}
