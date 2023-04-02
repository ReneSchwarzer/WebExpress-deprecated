using System;
using System.Collections.Generic;
using WebExpress.Internationalization;
using WebExpress.Uri;
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
        public IResourceContext ResourceContext { get; internal set; }

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
        /// Returns the uri.
        /// </summary>
        /// <returns>The uri.</returns>
        public IUri Uri { get; internal set; }

        /// <summary>
        /// Returns the variables.
        /// </summary>
        public IDictionary<string, string> Variables { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Constructor
        /// </summary>
        internal SearchResult()
        {

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
                        ResourceContext.Log.Warning(message: InternationalizationManager.I18N(InternationalizationManager.DefaultCulture, "webexpress", "resource.variable.duplicate"), args: v.Key);
                    }
                }
            }
        }
    }
}
