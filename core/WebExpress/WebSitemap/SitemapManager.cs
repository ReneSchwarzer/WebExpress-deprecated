using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WebExpress.Internationalization;
using WebExpress.WebUri;
using WebExpress.WebApplication;
using WebExpress.WebComponent;
using WebExpress.WebModule;
using WebExpress.WebPage;
using WebExpress.WebResource;

namespace WebExpress.WebSitemap
{
    /// <summary>
    /// The resource manager manages WebExpress elements, which can be called with a URI (Uniform Resource Identifier).
    /// </summary>
    public sealed class SitemapManager : IComponent, ISystemComponent
    {
        /// <summary>
        /// Returns the reference to the context of the host.
        /// </summary>
        public IHttpServerContext HttpServerContext { get; private set; }

        /// <summary>
        /// Returns the side map.
        /// </summary>
        private SitemapNode SiteMap { get; set; } = new SitemapNode();

        /// <summary>
        /// Constructor
        /// </summary>
        internal SitemapManager()
        {

        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The reference to the context of the host.</param>
        public void Initialization(IHttpServerContext context)
        {
            HttpServerContext = context;

            HttpServerContext.Log.Debug
            (
                InternationalizationManager.I18N("webexpress:sitemapmanager.initialization")
            );
        }

        /// <summary>
        /// Rebuilds the sitemap.
        /// </summary>
        public void Refresh()
        {
            var newSiteMapNode = new SitemapNode() { };

            HttpServerContext.Log.Debug
            (
                InternationalizationManager.I18N("webexpress:sitemapmanager.refresh")
            );

            // applications
            var applications = ComponentManager.ApplicationManager.Applications
                .Select(x => new
                {
                    ApplicationContext = x,
                    PathSegments = x.ContextPath.Path.Skip(1)
                })
                .OrderBy(x => x.PathSegments.Count());

            foreach (var application in applications)
            {
                CreateSiteMap(newSiteMapNode, application.PathSegments, application.ApplicationContext);
            }

            // modules
            var modules = ComponentManager.ApplicationManager.Applications
                .Select(x => new
                {
                    ModuleContext = x,
                    PathSegments = x.ContextPath.Path.Skip(1)
                })
                .OrderBy(x => x.PathSegments.Count());

            foreach (var module in modules)
            {
                CreateSiteMap(newSiteMapNode, module.PathSegments, module.ModuleContext);
            }

            // resourcen
            var resources = ComponentManager.ResourceManager.ResourceItems
                .SelectMany(x => x.ResourceContexts
                .Select(y => new
                {
                    Item = x,
                    ResourceContext = y,
                    PathSegments = y.ContextPath.Path.Skip(1)
                }))
                .OrderBy(x => x.PathSegments.Count());

            foreach (var item in resources)
            {
                CreateSiteMap(newSiteMapNode, item.PathSegments, item.Item, item.ResourceContext);
            }

            using (var frame = new LogFrameSimple(HttpServerContext.Log))
            {

                HttpServerContext.Log.Info
                (
                    InternationalizationManager.I18N
                    (
                        "webexpress:sitemapmanager.sitemap"
                    )
                );

                var preorder = newSiteMapNode
                    .GetPreOrder()
                    .Select(x => InternationalizationManager.I18N
                        (
                            "webexpress:sitemapmanager.preorder",
                            "  " + x.ToString().PadRight(60),
                            x.ResourceItem != null ? x.ResourceItem?.ID : ""
                        ));

                foreach (var node in preorder)
                {
                    HttpServerContext.Log.Info(string.Join(Environment.NewLine, node));
                }
            }

            SiteMap = newSiteMapNode;
        }

        /// <summary>
        /// Locates the resource associated with the Uri.
        /// </summary>
        /// <param name="requestUri">The Uri.</param>
        /// <param name="searchContext">The search context.</param>
        /// <returns>The search result with the found resource or null</returns>
        public SearchResult SearchResource(IUri requestUri, SearchContext searchContext)
        {
            var variables = new Dictionary<string, string>();
            var result = SearchNode(SiteMap, requestUri.Path.Skip(1), searchContext, variables);

            if (result != null && result.ResourceContext != null)
            {
                if (!result.ResourceContext.Conditions.Any() || result.ResourceContext.Conditions.All(x => x.Fulfillment(searchContext.Request)))
                {
                    return result;
                }
            }

            // 404
            return result;
        }

        /// <summary>
        /// Creates the sitemap. Works recursively.
        /// It is important for the algorithm that the addition of application is sorted 
        /// by the number of path segments in ascending order.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="contextPathSegments">The path segments of the context path.</param>
        /// <param name="applicationContext">The application context.</param>
        private void CreateSiteMap(SitemapNode node, IEnumerable<IUriPathSegment> contextPathSegments, IApplicationContext applicationContext)
        {
            var pathSegment = contextPathSegments.FirstOrDefault();

            if (pathSegment == null)
            {
                return;
            }

            foreach (var child in node.Children.Where(x => IsMatched(x, pathSegment)))
            {
                CreateSiteMap(child, contextPathSegments.Skip(1), applicationContext);

                return;
            }

            var dummyNode = new SitemapNode()
            {
                Parent = node,
                PathSegment = new PathSegmentConstant(pathSegment.Value, applicationContext.ApplicationName),
                ApplicationContext = applicationContext
            };

            CreateSiteMap(dummyNode, contextPathSegments.Skip(1), applicationContext);

            node.Children.Add(dummyNode);
        }

        /// <summary>
        /// Creates the sitemap. Works recursively.
        /// It is important for the algorithm that the addition of module is sorted 
        /// by the number of path segments in ascending order.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="contextPathSegments">The path segments of the context path.</param>
        /// <param name="moduleContext">The application context.</param>
        private void CreateSiteMap(SitemapNode node, IEnumerable<IUriPathSegment> contextPathSegments, IModuleContext moduleContext)
        {
            var pathSegment = contextPathSegments.FirstOrDefault();

            if (pathSegment == null)
            {
                return;
            }

            foreach (var child in node.Children.Where(x => IsMatched(x, pathSegment)))
            {
                CreateSiteMap(child, contextPathSegments.Skip(1), moduleContext);

                return;
            }

            var dummyNode = new SitemapNode()
            {
                Parent = node,
                PathSegment = new PathSegmentConstant(pathSegment.Value, moduleContext.ModuleName),
                ApplicationContext = moduleContext?.ApplicationContext,
                ModuleContext = moduleContext
            };

            CreateSiteMap(dummyNode, contextPathSegments.Skip(1), moduleContext);

            node.Children.Add(dummyNode);
        }

        /// <summary>
        /// Creates the sitemap. Works recursively.
        /// It is important for the algorithm that the addition of resources is sorted 
        /// by the number of path segments in ascending order.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="contextPathSegments">The path segments of the context path.</param>
        /// <param name="resourceItem">The resource item.</param>
        /// <param name="resourceContext">The resource context.</param>
        private void CreateSiteMap(SitemapNode node, IEnumerable<IUriPathSegment> contextPathSegments, ResourceItem resourceItem, IResourceContext resourceContext)
        {
            var pathSegment = contextPathSegments.FirstOrDefault();

            if (pathSegment == null)
            {
                var resourceNode = new SitemapNode()
                {
                    Parent = node,
                    PathSegment = resourceItem.PathSegment,
                    ResourceItem = resourceItem,
                    ApplicationContext = resourceContext?.ModuleContext?.ApplicationContext,
                    ModuleContext = resourceContext?.ModuleContext,
                    ResourceContext = resourceContext
                };

                node.Children.Add(resourceNode);

                return;
            }
            else if (contextPathSegments.Count() <= 1 &&
                resourceItem.PathSegment is PathSegmentConstant constant &&
                string.IsNullOrWhiteSpace(constant.Segment))
            {
                var child = node.Children.Where(x => IsMatched(x, pathSegment)).FirstOrDefault();

                if (child?.ResourceItem == null)
                {
                    child.PathSegment = new PathSegmentConstant(pathSegment.Value, resourceItem.Title);
                    child.ResourceItem = resourceItem;
                    child.ApplicationContext = resourceContext?.ModuleContext?.ApplicationContext;
                    child.ModuleContext = resourceContext?.ModuleContext;
                    child.ResourceContext = resourceContext;
                }
            }

            foreach (var child in node.Children.Where(x => IsMatched(x, pathSegment)))
            {
                CreateSiteMap(child, contextPathSegments.Skip(1), resourceItem, resourceContext);

                return;
            }

            var dummyNode = new SitemapNode()
            {
                Parent = node,
                PathSegment = new PathSegmentConstant(pathSegment.Value, pathSegment.Display),
                ApplicationContext = resourceContext?.ModuleContext?.ApplicationContext,
                ModuleContext = resourceContext?.ModuleContext
            };

            CreateSiteMap(dummyNode, contextPathSegments.Skip(1), resourceItem, resourceContext);

            node.Children.Add(dummyNode);
        }

        /// <summary>
        /// Locates the resource associated with the Uri. Works recursively.
        /// </summary>
        /// <param name="node">The sitemap node.</param>
        /// <param name="pathSegments">The path segments.</param>
        /// <param name="searchContext">The search context.</param>
        /// <param name="variables">Pick up the variables along the path or null.</param>
        /// <returns>The search result with the found resource</returns>
        private SearchResult SearchNode(SitemapNode node, IEnumerable<IUriPathSegment> pathSegments, SearchContext searchContext, IDictionary<string, string> variables = null)
        {
            var pathSegment = pathSegments.FirstOrDefault();

            if (pathSegment == null)
            {
                // 404
                return new SearchResult()
                {
                    ApplicationContext = node.ApplicationContext,
                    ModuleContext = node.ModuleContext,
                    ResourceContext = node.ResourceContext,
                    Variables = variables ?? new Dictionary<string, string>()
                };
            }

            foreach (var child in node.Children.Where(x => IsMatched(x, pathSegment, variables)))
            {
                if (child.ResourceItem != null && (child.IsLeaf || pathSegments.Count() <= 1))
                {
                    return new SearchResult()
                    {
                        ID = child.ResourceItem.ID,
                        Title = child.ResourceItem.Title,
                        ApplicationContext = child.ApplicationContext,
                        ModuleContext = child.ModuleContext,
                        ResourceContext = child.ResourceContext,
                        Instance = CreateInstance(child, searchContext),
                        Variables = variables ?? new Dictionary<string, string>(),
                        Uri = UriRelative.Combine
                        (
                            child.ResourceContext?.ContextPath,
                            child.ResourceItem?.ID
                        )
                    };
                }
                else if (child.IsLeaf || pathSegments.Count() <= 1)
                {
                    return new SearchResult()
                    {
                        ApplicationContext = node.ApplicationContext,
                        ModuleContext = node.ModuleContext,
                        ResourceContext = node.ResourceContext,
                        Variables = variables ?? new Dictionary<string, string>()
                    };
                }

                return SearchNode(child, pathSegments.Skip(1), searchContext, variables);
            }

            // 404
            return new SearchResult()
            {
                ApplicationContext = node.ApplicationContext,
                ModuleContext = node.ModuleContext,
                ResourceContext = node.ResourceContext,
                Variables = variables ?? new Dictionary<string, string>()
            };
        }

        /// <summary>
        /// Creates a new instance or if caching is active, a possibly existing instance is returned.
        /// </summary>
        /// <param name="node">The sitemap node.</param>
        /// <param name="context">The search context.</param>
        /// <returns>The instance or null.</returns>
        private IResource CreateInstance(SitemapNode node, SearchContext context)
        {
            if (node == null || node.ResourceItem == null || node.ResourceContext == null)
            {
                return null;
            }

            if (node.ResourceContext.Cache && node.Instance != null)
            {
                return node.Instance;
            }

            var instance = node.ResourceItem.ResourceClass?.Assembly.CreateInstance(node.ResourceItem.ResourceClass?.FullName) as IResource;

            if (instance is II18N i18n)
            {
                i18n.Culture = context.Culture;
            }

            if (instance is Resource resorce)
            {
                resorce.ID = node.ResourceItem?.ID;
                resorce.Uri = context.Uri;
                resorce.ApplicationContext = node.ResourceContext?.ModuleContext?.ApplicationContext;
                resorce.ModuleContext = node.ResourceContext?.ModuleContext;
            }

            if (instance is IPage page)
            {
                page.Title = node.ResourceItem?.Title;
            }

            instance.Initialization(node.ResourceContext);

            if (node.ResourceContext.Cache)
            {
                node.Instance = instance;
            }

            return instance;
        }

        /// <summary>
        /// Checks whether the node matches the path element.
        /// </summary>
        /// <param name="node">The sitemap node.</param>
        /// <param name="pathSegement">The path segments.</param>
        /// <param name="variables">The variable, if set.</param>
        /// <returns>True if the path element matched, false otherwise.</returns>
        private bool IsMatched(SitemapNode node, IUriPathSegment pathSegement, IDictionary<string, string> variables = null)
        {
            if (node == null || string.IsNullOrWhiteSpace(pathSegement?.Value))
            {
                return false;
            }
            else if (node.PathSegment is PathSegmentConstant constant && constant.Segment.Equals(pathSegement?.Value, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else if (node.PathSegment is PathSegmentVariable variable && Regex.IsMatch(pathSegement?.Value, variable.Expression, RegexOptions.IgnoreCase))
            {
                if (variables != null)
                {
                    foreach (var variableValue in variable.GetVariables(pathSegement?.Value))
                    {
                        if (!variables.ContainsKey(variableValue.Key))
                        {
                            variables.Add(variableValue);
                        }
                    }
                }

                return true;
            }

            return false;
        }
    }
}
