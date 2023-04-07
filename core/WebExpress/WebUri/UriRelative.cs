using System;
using System.Collections.Generic;
using System.Linq;

namespace WebExpress.WebUri
{
    /// <summary>
    /// A relative uri (e.g. /image.png).
    /// </summary>
    public class UriRelative : IUri
    {
        /// <summary>
        /// The path (e.g. /over/there).
        /// </summary>
        public ICollection<IUriPathSegment> Path { get; } = new List<IUriPathSegment>();

        /// <summary>
        /// The query part (e.g. ?title=Uniform_Resource_Identifier&action=submit).
        /// </summary>
        public ICollection<UriQuerry> Query { get; } = new List<UriQuerry>();

        /// <summary>
        /// References a position within a resource (e.g. #Anchor).
        /// </summary>
        public string Fragment { get; set; }

        /// <summary>
        /// Returns the display string of the Uri
        /// </summary>
        public virtual string Display
        {
            get
            {
                var last = Path.LastOrDefault();
                return last?.Display;
            }

            set
            {
                var last = Path.LastOrDefault();
                if (last != null)
                {
                    last.Display = value;
                }
            }
        }

        /// <summary>
        /// Determines if the uri is empty.
        /// </summary>
        public bool Empty => Path.Count == 0;

        /// <summary>
        /// Returns the root.
        /// </summary>
        public virtual IUri Root
        {
            get
            {
                var root = new UriRelative(this);
                if (root.Path.Count > 1)
                {
                    return Take(1);
                }

                return root;
            }
        }

        /// <summary>
        /// Determines if the Uri is the root.
        /// </summary>
        public bool IsRoot => string.IsNullOrWhiteSpace(Path.FirstOrDefault()?.Value);

        /// <summary>
        /// Constructor
        /// </summary>
        public UriRelative()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="url">The actual uri called by the web browser.</param>
        public UriRelative(string uri)
        {
            if (uri == null) return;

            var fragment = uri.TrimEnd('/').Split('#');
            if (fragment.Length == 2)
            {
                Fragment = fragment[1];
            }

            var query = fragment[0].Split('?');
            if (query.Length == 2)
            {
                foreach (var q in query[1].Split('&'))
                {
                    var item = q.Split('=');

                    Query.Add(new UriQuerry(item[0], item.Length > 1 ? item[1] : null));
                }
            }

            Path.Add(new UriPathSegment(null, "root"));

            foreach (var p in query[0].Split('/', StringSplitOptions.RemoveEmptyEntries))
            {
                Path.Add(new UriPathSegment(p));
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="url">The uri.</param>
        public UriRelative(IUri uri)
        {
            Path = uri.Path.Select(x => new UriPathSegment(x) as IUriPathSegment).ToList();
            Query = uri.Query.Select(x => new UriQuerry(x.Key, x.Value)).ToList();
            Fragment = uri.Fragment;
        }

        /// <summary>
        /// Adds a path element.
        /// </summary>
        /// <param name="path">The path to append.</param>
        /// <returns>The extended path.</returns>
        public virtual IUri Append(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return this;
            }

            var copy = new UriRelative(this);

            foreach (var p in path.Split('/', StringSplitOptions.RemoveEmptyEntries))
            {
                copy.Path.Add(new UriPathSegment(p));
            }

            return copy;
        }

        /// <summary>
        /// Adds a path element.
        /// </summary>
        /// <param name="path">The path to append.</param>
        /// <returns>The extended path.</returns>
        public virtual IUri Append(IUriPathSegment path)
        {
            if (path == null)
            {
                return this;
            }

            var copy = new UriRelative(this);

            copy.Path.Add(path);

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
        public virtual IUri Take(int count)
        {
            var copy = new UriRelative(this);
            var path = copy.Path.ToList();
            copy.Path.Clear();

            if (count == 0)
            {
                return new UriRelative();
            }
            else if (count > 0)
            {
                (copy.Path as List<IUriPathSegment>).AddRange(path.Take(count));
            }
            else if (count < 0 && Math.Abs(count) < path.Count)
            {
                (copy.Path as List<IUriPathSegment>).AddRange(path.Take(path.Count + count));
            }
            else
            {
                return null;
            }

            return copy;
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
            if (count >= Path.Count)
            {
                return null;
            }
            if (count > 0)
            {
                var copy = new UriRelative(this);
                var path = copy.Path.ToList();
                copy.Path.Clear();
                (copy.Path as List<IUriPathSegment>).AddRange(path.Skip(count));

                return copy;
            }

            return new UriRelative(this);
        }

        /// <summary>
        /// Determines whether the given segment is part of the uri.
        /// </summary>
        /// <param name="segment">The segment to be tested.</param>
        /// <returns>true if successful, false otherwise.</returns>
        public virtual bool Contains(string segment)
        {
            return Path.Where(x => x.Value.Equals(segment, StringComparison.OrdinalIgnoreCase)).Any();
        }

        /// <summary>
        /// Checks whether a given uri is part of that uri.
        /// </summary>
        /// <param name="uri">The Uri to be checked.</param>
        /// <returns>true if part of the uri, false otherwise.</returns>
        public bool StartsWith(IUri uri)
        {
            return ToString().StartsWith(uri.ToString());
        }

        /// <summary>
        /// Converts the uri to a string.
        /// </summary>
        /// <returns>The string representation of the uri.</returns>
        public override string ToString()
        {
            var path = "/" + string.Join("/", Path.Where(x => !string.IsNullOrWhiteSpace(x.Value)).Select(x => x.Value));

            return string.Format("{0}", path);
        }

        /// <summary>
        /// Combines the specified uris into a compound uri.
        /// </summary>
        /// <param name="uris">The uris to be combine.</param>
        /// <returns>A combined uri.</returns>
        public static UriRelative Combine(params string[] uris)
        {
            var uri = new UriRelative();

            uri.Path.Add(new UriPathSegment(null, "root"));
            (uri.Path as List<IUriPathSegment>).AddRange(uris.Where(x => !string.IsNullOrWhiteSpace(x))
                            .SelectMany(x => x.Split('/', StringSplitOptions.RemoveEmptyEntries))
                            .Select(x => new UriPathSegment(x) as IUriPathSegment));

            return uri;
        }

        /// <summary>
        /// Combines the specified uris into a compound uri.
        /// </summary>
        /// <param name="uris">The uris to be combine.</param>
        /// <returns>A combined uri.</returns>
        public static UriRelative Combine(params IUri[] uris)
        {
            var uri = new UriRelative(uris.FirstOrDefault());
            (uri.Path as List<IUriPathSegment>).AddRange(uris.Skip(1).SelectMany(x => x.Path.Skip(1)));

            return uri;
        }

        /// <summary>
        /// Combines the specified uris into a compound uri.
        /// </summary>
        /// <param name="uri">The first uri to be combine.</param>
        /// <param name="uris">The uris to be combine.</param>
        /// <returns>A combined uri.</returns>
        public static UriRelative Combine(IUri uri, params string[] uris)
        {
            var copy = new UriRelative(uri as UriRelative);
            (copy.Path as List<IUriPathSegment>).AddRange(uris.Where(x => !string.IsNullOrWhiteSpace(x))
                    .SelectMany(x => x.Split('/', StringSplitOptions.RemoveEmptyEntries))
                    .Select(x => new UriPathSegment(x) as IUriPathSegment));


            return copy;
        }

    }
}