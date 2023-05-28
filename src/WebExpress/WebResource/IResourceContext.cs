using System.Collections.Generic;
using WebExpress.WebApplication;
using WebExpress.WebCondition;
using WebExpress.WebModule;
using WebExpress.WebPlugin;
using WebExpress.WebUri;

namespace WebExpress.WebResource
{
    public interface IResourceContext
    {
        /// <summary>
        /// Returns the associated plugin context.
        /// </summary>
        IPluginContext PluginContext { get; }

        /// <summary>
        /// Returns the associated application context.
        /// </summary>
        IApplicationContext ApplicationContext { get; }

        /// <summary>
        /// Returns the corresponding module context.
        /// </summary>
        IModuleContext ModuleContext { get; }

        /// <summary>
        /// Returns the context name that provides the resource. The context name 
        /// is a string with a name (e.g. global, admin), which can be used by elements to 
        /// determine whether content and how content should be displayed.
        /// </summary>
        IReadOnlyList<string> Context { get; }

        /// <summary>
        /// Provides the conditions that must be met for the resource to be active.
        /// </summary>
        ICollection<ICondition> Conditions { get; }

        /// <summary>
        /// Returns the resource id.
        /// </summary>
        string ResourceId { get; }

        /// <summary>
        /// Returns the resource title.
        /// </summary>
        string ResourceTitle { get; }

        /// <summary>
        /// Returns the parent or null if not used.
        /// </summary>
        IResourceContext ParentContext { get; }

        /// <summary>
        /// Determines whether the resource is created once and reused each time it is called.
        /// </summary>
        bool Cache { get; }

        /// <summary>
        /// Returns the context path.
        /// </summary>
        UriResource ContextPath { get; }

        /// <summary>
        /// Returns the uri.
        /// </summary>
        UriResource Uri { get; }
    }
}
