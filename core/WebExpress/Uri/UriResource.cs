using System.Collections.Generic;
using System.Globalization;
using WebExpress.WebModule;
using WebExpress.WebSitemap;

namespace WebExpress.Uri
{
    /// <summary>
    /// A uri that references a resource.
    /// </summary>
    public class UriResource : IUriResource
    {
        /// <summary>
        /// Returns or sets the context path.
        /// </summary>
        public IUri ContextPath { get; private set; }

        /// <summary>
        /// Returns or sets the uri.
        /// </summary>
        private IUri Uri { get; set; }

        /// <summary>
        /// The path (e.g. /over/there).
        /// </summary>
        public ICollection<IUriPathSegment> Path => Uri.Path;

        /// <summary>
        /// The query part (e.g. ?title=Uniform_Resource_Identifier&action=submit).
        /// </summary>
        public ICollection<UriQuerry> Query => Uri.Query;

        /// <summary>
        /// References a position within a resource (e.g. #Anchor).
        /// </summary>
        public string Fragment
        {
            get { return Uri.Fragment; }
            set { Uri.Fragment = value; }
        }

        /// <summary>
        /// Returns the display string of the Uri
        /// </summary>
        public string Display { get { return Uri.Display; } set { Uri.Display = value; } }

        /// <summary>
        /// Determines if the uri is empty.
        /// </summary>
        public bool Empty => Uri.Empty;

        /// <summary>
        /// Returns the root.
        /// </summary>
        public IUri Root => new UriResource(ContextPath);

        /// <summary>
        /// Determines if the uri is the root.
        /// </summary>
        public bool IsRoot => Uri.IsRoot;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contextPath">The context path.</param>
        /// <param name="uri">The actual uri.</param>
        public UriResource(IUri contextPath, IUri uri = null)
        {
            ContextPath = contextPath;
            Uri = uri ?? new UriRelative();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contextPath">The context path.</param>
        /// <param name="uri">The actual uri.</param>
        public UriResource(IUri contextPath, string uri)
        {
            ContextPath = contextPath;
            Uri = string.IsNullOrWhiteSpace(uri) ? new UriRelative() : new UriRelative(uri);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contextPath1">The left part of the context path.</param>
        /// <param name="contextPath2">The right part of the context path.</param>
        /// <param name="uri">The uri.</param>
        public UriResource(IUri contextPath1, IUri contextPath2, IUri uri)
            :this(UriRelative.Combine(contextPath1, contextPath2), uri)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contextPath1">The left part of the context path.</param>
        /// <param name="contextPath2">The right part of the context path.</param>
        /// <param name="uri">The uri.</param>
        public UriResource(IUri contextPath1, string contextPath2, string uri)
            : this(UriRelative.Combine(contextPath1, contextPath2), uri)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The context of the module.</param>
        /// <param name="url">The actual uri called by the web browser.</param>
        /// <param name="node">The node of the sitemap.</param>
        /// <param name="culture">The culture.</param>
        internal UriResource(IModuleContext context, IUri url, SearchResult node, CultureInfo culture)
            : this(context, url.ToString(), node?.Path, culture)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The context of the module.</param>
        /// <param name="url">The actual uri called by the web browser.</param>
        /// <param name="culture">The culture.</param>
        internal UriResource(IModuleContext context, string url, ICollection<SitemapNode> path, CultureInfo culture)
            : this(context?.ContextPath, new UriRelative())
        {
            if (context == null) return;

            var uri = new UriRelative(url[context.ContextPath.ToString().Length..]);
            var uriPath = uri.Path as List<IUriPathSegment>;
            var nodePath = path as List<SitemapNode>;

            for (var i = 0; i < uriPath.Count; i++)
            {
                var u = uriPath[i];

                if (i < nodePath.Count)
                {
                    var item = new UriPathSegment(u.Value, u.Tag) { Display = nodePath[i]?.PathSegment?.GetDisplay(u.ToString(), context.PluginContext.PluginID, culture) };
                    Uri.Path.Add(item);
                }
                else
                {
                    var item = new UriPathSegment(u.Value, u.Tag);
                    Uri.Path.Add(item);
                }
            }
        }

        /// <summary>
        /// Adds a path element.
        /// </summary>
        /// <param name="path">The path to append.</param>
        /// <returns>The extended path.</returns>
        public IUri Append(string path)
        {
            return new UriResource(ContextPath, UriRelative.Combine(Uri, path));
        }

        /// <summary>
        /// Return a shortened uri containing n-elements.
        /// count > 0 count elements are included
        /// count < 0 count elements are truncated
        /// count = 0 an empty uri is returned
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>The sub uri.</returns>
        public IUri Take(int count)
        {
            return new UriResource(ContextPath, Uri.Take(count));
        }

        /// <summary>
        /// Return a shortened uri by not including the first n elements.
        /// count > 0 count elements are skipped
        /// count <= 0 an empty Uri is returned
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>The sub uri.</returns>
        public IUri Skip(int count)
        {
            return new UriResource(ContextPath, Uri.Skip(count));
        }

        /// <summary>
        /// Determines whether the given segment is part of the uri.
        /// </summary>
        /// <param name="segment">The segment to be tested.</param>
        /// <returns>true if successful, false otherwise.</returns>
        public bool Contains(string segment)
        {
            return Uri.Contains(segment);
        }

        /// <summary>
        /// Checks whether a given uri is part of that uri.
        /// </summary>
        /// <param name="uri">The Uri to be checked.</param>
        /// <returns>true if part of the uri, false otherwise.</returns>
        public bool StartsWith(IUri uri)
        {
            return Uri.ToString().StartsWith(uri.ToString());
        }

        /// <summary>
        /// Converts the uri to a string.
        /// </summary>
        /// <returns>The string representation of the uri.</returns>
        public override string ToString()
        {
            return "/" + string.Join("/", ContextPath.ToString().Trim('/'), Uri.ToString().Trim('/')).Trim('/');
        }
    }
}
