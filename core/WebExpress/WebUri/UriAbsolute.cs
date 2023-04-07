using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebExpress.WebUri
{
    /// <summary>
    /// Absolute uri (e.g. https://de.wikipedia.org/wiki/Uniform_Resource_Identifier).
    /// </summary>
    public class UriAbsolute : UriRelative
    {
        /// <summary>
        /// Pattern of the uri.
        /// </summary>
        private const string Pattern = "^([a-z0-9+.-]+):(?://(?:((?:[a-z0-9-._~!$&'()*+,;=:]|%[0-9A-F]{2})*)@)?((?:[a-z0-9-._~!$&'()*+,;=]|%[0-9A-F]{2})*)(?::(\\d*))?(.*)?)$";

        /// <summary>
        /// The scheme (e.g. Http, FTP).
        /// </summary>
        public UriScheme Scheme { get; set; }

        /// <summary>
        /// The authority (e.g. user@example.com:8080).
        /// </summary>
        public UriAuthority Authority { get; set; }

        /// <summary>
        /// Returns the root.
        /// </summary>
        public override IUri Root
        {
            get
            {
                var root = new UriAbsolute(this);
                if (root.Path.Count > 1)
                {
                    (root.Path as List<IUriPathSegment>).AddRange(root.Path.Skip(1));
                }

                return root;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public UriAbsolute()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uri">The uri.</param>
        public UriAbsolute(string url)
        {
            if (url == null) return;

            var match = Regex.Match(url, Pattern);

            try
            {
                Scheme = (UriScheme)Enum.Parse(typeof(UriScheme), match.Groups[1].Value, true);
            }
            catch
            {
                Scheme = UriScheme.Http;
            }

            Authority = new UriAuthority()
            {
                User = match.Groups[2].Value,
                Host = match.Groups[3].Value,
                Port = !string.IsNullOrWhiteSpace(match.Groups[4].Value) ? Convert.ToInt32(match.Groups[4].Value) : null
            };

            var uri = new UriRelative(match.Groups[5].Value);
            (Path as List<IUriPathSegment>).AddRange(uri.Path.Select(x => new UriPathSegment(x.Value, x.Tag) as IUriPathSegment));
            (Query as List<UriQuerry>).AddRange(uri.Query.Select(x => new UriQuerry(x.Key, x.Value)));
            Fragment = uri.Fragment;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="scheme">The scheme (e.g. Http, FTP).</param>
        /// <param name="authority">The authority (e.g. user@example.com:8080).</param>
        /// <param name="uri">The uri.</param>
        public UriAbsolute(UriScheme scheme, UriAuthority authority, IUri uri)
        {
            Scheme = scheme;
            Authority = authority;

            if (uri != null)
            {
                (Path as List<IUriPathSegment>).AddRange(uri.Path.Select(x => new UriPathSegment(x.Value, x.Tag) as IUriPathSegment));
                (Query as List<UriQuerry>).AddRange(uri.Query.Select(x => new UriQuerry(x.Key, x.Value)));
                Fragment = uri.Fragment;
            }
        }

        /// <summary>
        /// Copy-Constructor
        /// </summary>
        /// <param name="uri">The uri to copy.</param>
        public UriAbsolute(UriAbsolute uri)
        {
            Scheme = uri.Scheme;
            Authority = uri.Authority;
            (Path as List<IUriPathSegment>).AddRange(uri.Path.Select(x => new UriPathSegment(x.Value, x.Tag) as IUriPathSegment));
            (Query as List<UriQuerry>).AddRange(uri.Query.Select(x => new UriQuerry(x.Key, x.Value)));
            Fragment = uri.Fragment;
        }

        /// <summary>
        /// Adds a path element.
        /// </summary>
        /// <param name="path">The path to append.</param>
        /// <returns>The extended path.</returns>
        public override IUri Append(string path)
        {
            var copy = new UriAbsolute(this);

            foreach (var p in path.Split('/'))
            {
                copy.Path.Add(new UriPathSegment(p));
            }

            return copy;
        }

        /// <summary>
        /// Return a shortened uri containing n-elements.
        /// count > 0 count elements are included
        /// count < 0 count elements are truncated
        /// count = 0 an empty uri is returned
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>The sub uri.</returns>
        public override IUri Take(int count)
        {
            var copy = new UriAbsolute(this);

            if (count > 0)
            {
                (copy.Path as List<IUriPathSegment>).AddRange(copy.Path.Take(count));
            }
            else if (count > 0 && count < copy.Path.Count)
            {
                (copy.Path as List<IUriPathSegment>).AddRange(copy.Path.Take(copy.Path.Count - count));
            }
            else
            {
                return new UriAbsolute();
            }

            return copy;
        }

        /// <summary>
        /// Converts the uri to a string.
        /// </summary>
        /// <returns>The string representation of the uri.</returns>
        public override string ToString()
        {
            var scheme = Scheme.ToString("g").ToLower() + ":";
            var authority = Authority?.ToString();

            return Scheme switch
            {
                UriScheme.Mailto => string.Format("{0}{1}", scheme, authority),
                _ => string.Format("{0}{1}{2}", scheme, authority, base.ToString()),
            };
        }
    }
}