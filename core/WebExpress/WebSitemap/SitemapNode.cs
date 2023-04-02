using System.Collections.Generic;
using System.Linq;
using WebExpress.WebApplication;
using WebExpress.WebModule;
using WebExpress.WebResource;

namespace WebExpress.WebSitemap
{
    /// <summary>
    /// A Sitemap node.
    /// </summary>
    public class SitemapNode
    {
        /// <summary>
        /// Returns the node path segment.
        /// </summary>
        public IPathSegment PathSegment { get; internal set; }

        /// <summary>
        /// Returns the resource item.
        /// </summary>
        public ResourceItem ResourceItem { get; internal set; }

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
        /// Returns the instance
        /// </summary>
        public IResource Instance { get; internal set; }

        /// <summary>
        /// Returns the child nodes.
        /// </summary>
        public ICollection<SitemapNode> Children { get; } = new List<SitemapNode>();

        /// <summary>
        /// Returns the parent node.
        /// </summary>
        public SitemapNode Parent { get; internal set; }

        /// <summary>
        /// returns the root.
        /// </summary>
        public SitemapNode Root
        {
            get
            {
                if (IsRoot)
                {
                    return this;
                }

                var parent = Parent;
                while (!parent.IsRoot)
                {
                    parent = parent.Parent;
                }

                return parent;
            }
        }

        /// <summary>
        /// Checks whether the node is the root.
        /// </summary>
        /// <returns>true if root, otherwise false.</returns>
        public bool IsRoot => Parent == null;

        /// <summary>
        /// Checks whether the node is a leaf.
        /// </summary>
        /// <returns>true if a leaf, otherwise false.</returns>
        public bool IsLeaf => Children.Count() == 0;

        /// <summary>
        /// Constructor
        /// </summary>
        public SitemapNode()
        {

        }

        /// <summary>
        /// Passes through the tree in pre order.
        /// </summary>
        /// <returns>The tree as a list.</returns>
        public IEnumerable<SitemapNode> GetPreOrder()
        {
            var list = new List<SitemapNode>
            {
                this
            };

            foreach (var child in Children)
            {
                list.AddRange(child.GetPreOrder());
            }

            return list;
        }

        /// <summary>
        /// Returns the path.
        /// </summary>
        /// <returns>The path.</returns>
        public ICollection<SitemapNode> Path
        {
            get
            {
                var list = new List<SitemapNode>
                {
                    this
                };

                var parent = Parent;
                while (parent != null)
                {
                    list.Add(parent);

                    parent = parent.Parent;
                }

                list.Reverse();

                return list;
            }
        }

        /// <summary>
        /// Convert to string.
        /// </summary>
        /// <returns>The tree node in its string representation</returns>
        public override string ToString()
        {
            return string.Join("/", Path.Select(x => x.PathSegment?.ToString().ToLower()));
        }
    }
}
