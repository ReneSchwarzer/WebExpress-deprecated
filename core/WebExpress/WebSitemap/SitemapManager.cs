using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Uri;
using WebExpress.WebApplication;
using WebExpress.WebComponent;
using WebExpress.WebModule;
using WebExpress.WebResource;
using static WebExpress.Internationalization.InternationalizationManager;

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
        public IHttpServerContext Context { get; private set; }

        /// <summary>
        /// Returns the side map.
        /// </summary>
        private static SitemapNode SiteMap { get; set; } = new SitemapNode();

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
            Context = context;

            Context.Log.Info(message: I18N("webexpress:sitemapmanager.initialization"));
        }

        /// <summary>
        /// Rebuilds the sitemap.
        /// </summary>
        public void Refresh()
        {
            var newSiteMap = new SitemapNode() { Dummy = true };

            Context.Log.Info(message: I18N("webexpress:sitemapmanager.refresh"));

            foreach (var moduleUri in ApplicationManager.Applications
                .SelectMany(a => ModuleManager.Modules.Select(x => x.GetContextPath(a))))
            {
                var skip = moduleUri.Skip(1);

                // create an node and set properties
                var node = newSiteMap.Insert(skip, null);
            }

            foreach (var resource in ResourceManager.Resources)
            {
                var moduleID = resource.Context.ModuleContext.ModuleID;

                foreach (var applicationContext in resource.Context.GetApplicationContexts())
                {
                    var pathUri = ToUri(resource.Paths);
                    var resourceUri = !string.IsNullOrWhiteSpace(resource.PathSegment.ToString()) ?
                            UriRelative.Combine(resource.Context.GetContextPath(applicationContext), pathUri.ToString(), resource.PathSegment.ToString()) :
                            resource.Context.GetContextPath(applicationContext);

                    // check if an optional resource
                    if (resource.Optional &&
                        !(applicationContext.Options.Contains($"{moduleID}.*".ToLower()) ||
                          applicationContext.Options.Contains($"{moduleID}.{resource.ID}".ToLower()))
                    )
                    {
                        continue;
                    }

                    if (resourceUri.Path.Count == 0)
                    {
                        // set Root
                        if (newSiteMap.Dummy)
                        {
                            newSiteMap.ID = resource.ID;
                            newSiteMap.Title = resource.Title;
                            newSiteMap.Type = resource.Type;
                            newSiteMap.ApplicationContext = applicationContext;
                            newSiteMap.ModuleContext = resource.Context.ModuleContext;
                            newSiteMap.Context = resource.Context;
                            newSiteMap.ResourceContextFilter = resource.ResourceContext;
                            newSiteMap.IncludeSubPaths = resource.IncludeSubPaths;
                            newSiteMap.PathSegment = resource.PathSegment;
                            newSiteMap.Dummy = false;

                            Context.Log.Info(message: I18N("webexpress:sitemapmanager.addresource", moduleID));
                        }
                        else
                        {
                            Context.Log.Warning(message: I18N("webexpress:sitemapmanager.alreadyassigned", "ROOT", resource.ID));
                        }
                    }
                    else
                    {
                        var skip = resourceUri.Skip(1);

                        // create an node and set properties
                        var node = newSiteMap.Insert(skip, resource.ID);
                        var pathSegment = resource.PathSegment;

                        if (pathSegment is PathSegmentConstant constant)
                        {
                            var nodeSegment = node?.PathSegment as PathSegmentConstant;
                            pathSegment = new PathSegmentConstant(nodeSegment?.Segment, constant.Display);
                        }

                        if (node != null)
                        {
                            node.PathSegment = pathSegment;
                            node.Title = resource.Title;
                            node.Type = resource.Type;
                            node.ApplicationContext = applicationContext;
                            node.ModuleContext = resource.Context.ModuleContext;
                            node.Context = resource.Context;
                            node.ResourceContextFilter = resource.ResourceContext;
                            node.IncludeSubPaths = resource.IncludeSubPaths;

                            Context.Log.Info(message: I18N("webexpress:sitemapmanager.addresource", resource.ID));
                        }
                        else
                        {
                            Context.Log.Warning(message: I18N("webexpress:sitemapmanager.addresource.error", resource.ID));
                        }
                    }
                }
            }

            SiteMap = newSiteMap;

            foreach (var node in SiteMap.GetPreOrder())
            {
                Context.Log.Info(message: I18N("webexpress:sitemapmanager.preorder", node.ToString().PadRight(60), !node.Dummy ? node.ID : "dummy"));
            }
        }

        /// <summary>
        /// Locates the resource associated with the Uri.
        /// </summary>
        /// <param name="uri">The Uri.</param>
        /// <param name="context">The search context.</param>
        /// <returns>The search result with the found resource or null</returns>
        public static SearchResult Find(string uri, SearchContext context)
        {
            var root = SiteMap;
            var requestUri = new UriRelative(uri);

            var result = root.Find(requestUri, context);

            if (result != null && result.Context != null)
            {
                if (!result.Context.Conditions.Any() || result.Context.Conditions.All(x => x.Fulfillment(context.Request)))
                {
                    return result;
                }
            }

            // 404
            return null;
        }

        /// <summary>
        /// Converts the given path to a uri.
        /// </summary>
        /// <param name="path">The path to be converted.</param>
        /// <returns>The uri that represents the path.</returns>
        private IUri ToUri(IReadOnlyList<string> path)
        {
            var uri = new UriRelative() as IUri;

            foreach (var item in path)
            {
                var resource = ResourceManager.Resources
                    .Where(x => x.ID.Equals(item, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();

                uri = uri.Append(resource?.PathSegment?.ToString() ?? item);
            }

            return uri;
        }
    }
}
