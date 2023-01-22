using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WebExpress.Internationalization;
using WebExpress.Uri;
using WebExpress.WebApplication;
using WebExpress.WebModule;
using WebExpress.WebPage;
using WebExpress.WebResource;

namespace WebExpress.WebSitemap
{
    /// <summary>
    /// A Sitemap node.
    /// </summary>
    public class SitemapNode
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
        /// Provides the path segment.
        /// </summary>
        public IPathSegment PathSegment { get; internal set; }

        /// <summary>
        /// Returns the class for creating an instance.
        /// </summary>
        public Type Type { get; internal set; }

        /// <summary>
        /// Returns the instance of the resource.
        /// </summary>
        private IResource Instance { get; set; }

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
        /// Returns the child nodes.
        /// </summary>
        public ICollection<SitemapNode> Children { get; } = new List<SitemapNode>();

        /// <summary>
        /// Returns the parent node.
        /// </summary>
        public SitemapNode Parent { get; private set; }

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
        /// Returns whether all subpaths should be taken into sitemap.
        /// </summary>
        public bool IncludeSubPaths { get; internal set; }

        /// <summary>
        /// Marks the node as a dummy with no associated resource.
        /// </summary>
        public bool Dummy { get; internal set; }

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
        /// Returns the path with any regular expressions that may exist.
        /// </summary>
        /// <returns>The path.</returns>
        public string ExpressionPath
        {
            get
            {
                if (Parent == null)
                {
                    return "/";
                }

                var list = new List<string>
                (
                    new string[] { PathSegment?.ToString() }
                );

                var parent = Parent;
                while (parent != null)
                {
                    list.Add(parent.PathSegment?.ToString());

                    parent = parent.Parent;
                }

                list.Reverse();

                return string.Join("/", list);
            }
        }

        /// <summary>
        /// Returns the path with any nodes that may be present.
        /// </summary>
        /// <returns>The path.</returns>
        public string IDPath
        {
            get
            {
                var list = new List<string>
                (
                    new string[] { ID }
                );

                var parent = Parent;
                while (parent != null)
                {
                    list.Add(parent.ID);

                    parent = parent.Parent;
                }

                list.Reverse();

                return string.Join("/", list);
            }
        }

        /// <summary>
        /// Inserts an item.
        /// </summary>
        /// <param name="uri">The path to the resource.</param>
        /// <param name="id">The resources id.</param>
        public SitemapNode Insert(IUri uri, string id)
        {
            // real entry
            if (uri == null || uri.Path.Count < 1)
            {
                return null;
            }

            var first = uri.Take(1).Path.FirstOrDefault();
            var isLast = uri.Path.Count == 1;
            var existingChild = Children.FirstOrDefault(x => x.ID.Equals(first.Value, StringComparison.OrdinalIgnoreCase));

            if (isLast)
            {
                if (existingChild != null)
                {
                    if (existingChild.Dummy)
                    {
                        // node already exists as a dummy
                        existingChild.ID = first.Value;
                        existingChild.PathSegment = new PathSegmentConstant(first.Value?.ToLower(), string.Empty);
                        existingChild.Title = string.Empty;
                        existingChild.Dummy = false;
                    }

                    return existingChild;
                } 
                else if (id == null)
                {
                    // create a dummy
                    var dummy = new SitemapNode()
                    {
                        Parent = this,
                        ID = first.Value,
                        PathSegment = new PathSegmentConstant(first.Value?.ToLower(), string.Empty),
                        Title = first.Value,
                        Dummy = true
                    };

                    Children.Add(dummy);

                    return dummy;
                }

                var child = new SitemapNode()
                {
                    Parent = this,
                    ID = id,
                    Title = first.Value,
                    PathSegment = new PathSegmentConstant(first.Value?.ToLower(), string.Empty),
                    Dummy = false
                };

                Children.Add(child);

                return child;
            }
            else if (existingChild == null)
            {
                // create a dummy
                var dummy = new SitemapNode()
                {
                    Parent = this,
                    ID = first.Value,
                    PathSegment = new PathSegmentConstant(first.Value?.ToLower(), string.Empty),
                    Title = first.Value,
                    Dummy = true
                };

                Children.Add(dummy);

                return dummy.Insert(uri.Skip(1), id);
            }

            return existingChild.Insert(uri.Skip(1), id);
        }

        /// <summary>
        /// Locates a node by ID.
        /// </summary>
        /// <param name="uri">The path to the resource.</param>
        /// <param name="context">The search context.</param>
        /// <returns>The node or null.</returns>
        public SearchResult Find(IUri uri, SearchContext context)
        {
            if (uri == null || uri.Path.Count == 0)
            {
                return null;
            }

            var current = uri.Take(1);
            var next = uri.Skip(1);
            var match = false;
            var uriSegment = current.Path.FirstOrDefault()?.ToString();
            var pathSegment = PathSegment?.ToString();
            var variables = null as IDictionary<string, string>;

            if (string.IsNullOrWhiteSpace(pathSegment) && string.IsNullOrWhiteSpace(uriSegment))
            {
                match = true;
            }
            else if (PathSegment is PathSegmentConstant && pathSegment.Equals(uriSegment, StringComparison.OrdinalIgnoreCase))
            {
                match = true;
            }
            else if (PathSegment is PathSegmentVariable variable && Regex.IsMatch(uriSegment, pathSegment, RegexOptions.IgnoreCase))
            {
                match = true;
                variables = variable.GetVariables(uriSegment);
            }

            if (match && IncludeSubPaths)
            {
                return new SearchResult
                (
                    ID,
                    Title,
                    Type,
                    CreateInstance(context, ApplicationContext),
                    ApplicationContext = ApplicationContext,
                    ModuleContext = ModuleContext,
                    Context,
                    ResourceContextFilter,
                    Path,
                    variables
                );
            }
            else if (match && next == null)
            {
                return new SearchResult
                (
                    ID,
                    Title,
                    Type,
                    CreateInstance(context, ApplicationContext),
                    ApplicationContext = ApplicationContext,
                    ModuleContext = ModuleContext,
                    Context,
                    ResourceContextFilter,
                    Path,
                    variables
                );
            }
            else if (match && Children.Count() > 0)
            {
                foreach (var child in Children)
                {
                    var result = child.Find(next, context);

                    if (result != null)
                    {
                        result.AddVariables(variables);
                        return result;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Creates a new instance or if caching is active, a possibly existing instance is returned.
        /// </summary>
        /// <param name="context">The search context.</param>
        /// <param name="applicationContext">The application context in which the resource is created.</param>
        /// <returns>The instance or null.</returns>
        public IResource CreateInstance(SearchContext context, IApplicationContext applicationContext)
        {
            if (Context == null)
            {
                return null;
            }

            if (Context.Cache && Instance != null)
            {
                return Instance;
            }

            var instance = Type?.Assembly.CreateInstance(Type?.FullName) as IResource;

            if (instance is II18N i18n)
            {
                i18n.Culture = context.Culture;
            }

            if (instance is Resource resorce)
            {
                resorce.ID = ID;
                resorce.Uri = new UriResource(Context.ModuleContext, context.Uri.ToString(), Path, context.Culture);
                resorce.ApplicationContext = applicationContext;
                resorce.ModuleContext = Context.ModuleContext;
            }

            if (instance is IPage page)
            {
                page.Title = Title;
            }

            instance.Initialization(Context);

            if (Context.Cache)
            {
                Instance = instance;
            }

            return instance;
        }

        /// <summary>
        /// Convert to string.
        /// </summary>
        /// <returns>The tree node in its string representation</returns>
        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(IDPath) ? "/" : IDPath;
        }
    }
}
