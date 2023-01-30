using System;
using System.Collections.Generic;
using WebExpress.WebCondition;

namespace WebExpress.WebResource
{
    /// <summary>
    /// A resource element that contains meta information about a resource.
    /// </summary>
    public class ResourceItem
    {
        /// <summary>
        /// Returns or sets the resource id.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Returns or sets the resource title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Returns or sets the type of resource.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Returns or sets the instance of the resource, if the resource is cached, otherwise null.
        /// </summary>
        public IResource Instance { get; set; }

        /// <summary>
        /// Provides or sets the context of the resource.
        /// </summary>
        public IResourceContext Context { get; set; }

        /// <summary>
        /// Returns or sets the context name that provides the resource. The context name 
        /// is a string with a name (e.g. global, admin), which can be used by elements to 
        /// determine whether content and how content should be displayed.
        /// </summary>
        public IReadOnlyList<string> ResourceContext { get; set; }

        /// <summary>
        /// Returns or sets the paths of the resource.
        /// </summary>
        public IReadOnlyList<string> Paths { get; set; }

        /// <summary>
        /// Returns or sets the path segment.
        /// </summary>
        public IPathSegment PathSegment { get; internal set; }

        /// <summary>
        /// Returns or sets whether all subpaths should be taken into sitemap.
        /// </summary>
        public bool IncludeSubPaths { get; set; }

        /// <summary>
        /// Returns whether it is an optional resource that must be enabled in a linked application.
        /// </summary>
        public bool Optional { get; set; }

        /// <summary>
        /// Returns the conditions that must be met for the resource to be active.
        /// </summary>
        public ICollection<ICondition> Conditions { get; set; }

        /// <summary>
        /// Returns whether the resource is created once and reused each time it is called.
        /// </summary>
        public bool Cache { get; set; }

        /// <summary>
        /// Convert the resource element to a string.
        /// </summary>
        /// <returns>The resource element in its string representation.</returns>
        public override string ToString()
        {
            return ID;
        }
    }
}
