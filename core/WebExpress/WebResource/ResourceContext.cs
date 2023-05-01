using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.WebComponent;
using WebExpress.WebCondition;
using WebExpress.WebModule;
using WebExpress.WebPlugin;
using WebExpress.WebUri;

namespace WebExpress.WebResource
{
    public class ResourceContext : IResourceContext
    {
        /// <summary>
        /// Returns the associated plugin context.
        /// </summary>
        public IPluginContext PluginContext { get; private set; }

        /// <summary>
        /// Returns the corresponding module context.
        /// </summary>
        public IModuleContext ModuleContext { get; private set; }

        /// <summary>
        /// Returns the context name that provides the resource. The context name 
        /// is a string with a name (e.g. global, admin), which can be used by elements to 
        /// determine whether content and how content should be displayed.
        /// </summary>
        public IReadOnlyList<string> Context { get; internal set; }

        /// <summary>
        /// Returns the conditions that must be met for the resource to be active.
        /// </summary>
        public ICollection<ICondition> Conditions { get; internal set; } = new List<ICondition>();

        /// <summary>
        /// Returns the resource id.
        /// </summary>
        public string ResourceID { get; internal set; }

        /// <summary>
        /// Returns the resource title.
        /// </summary>
        public string ResourceTitle { get; internal set; }

        /// <summary>
        /// Returns the parent or null if not used.
        /// </summary>
        public IResourceContext ParentContext => ComponentManager.ResourceManager.Resources
            .Where(x => !string.IsNullOrWhiteSpace(ResourceItem.Parent))
            .Where(x => x.ResourceID.Equals(ResourceItem.Parent, StringComparison.OrdinalIgnoreCase))
            .Where(x => x.ModuleContext.ApplicationContext == ModuleContext.ApplicationContext)
            .FirstOrDefault();

        /// <summary>
        /// Returns whether the resource is created once and reused each time it is called.
        /// </summary>
        public bool Cache { get; internal set; }

        /// <summary>
        /// Returns the context path.
        /// </summary>
        public UriResource ContextPath
        {
            get
            {
                var parentContext = ParentContext;
                if (parentContext != null)
                {
                    return UriResource.Combine(ParentContext?.Uri, ResourceItem.ContextPath);
                }

                return UriResource.Combine(ModuleContext.ContextPath, ResourceItem.ContextPath);
            }
        }

        /// <summary>
        /// Returns the uri.
        /// </summary>
        public UriResource Uri => ContextPath.Append(ResourceItem.PathSegment);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="moduleContext">The module context.</param>
        internal ResourceContext(IModuleContext moduleContext)
        {
            PluginContext = moduleContext?.PluginContext;
            ModuleContext = moduleContext;
        }

        /// <summary>
        /// Returns or sets the resource item.
        /// </summary>
        internal ResourceItem ResourceItem { get; set; }
    }
}
