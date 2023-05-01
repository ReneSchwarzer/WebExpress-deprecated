using System;
using System.Collections.Generic;
using System.Linq;

namespace WebExpress.WebUri
{
    /// <summary>
    /// A resource uri (e.g. /image.png).
    /// </summary>
    public class UriResource
    {
        /// <summary>
        /// The scheme (e.g. Http, FTP).
        /// </summary>
        public UriScheme Scheme { get; set; } = UriScheme.Http;

        /// <summary>
        /// The authority (e.g. user@example.com:8080).
        /// </summary>
        public UriAuthority Authority { get; set; }

        /// <summary>
        /// The path (e.g. /over/there).
        /// </summary>
        public ICollection<IUriPathSegment> PathSegments { get; } = new List<IUriPathSegment>();

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
                if (PathSegments.LastOrDefault() is IUriPathSegment last)
                {
                    return last?.Display;
                }

                return null;
            }

            set
            {
                if (PathSegments.LastOrDefault() is IUriPathSegment last)
                {
                    last.Display = value;
                }
            }
        }

        /// <summary>
        /// Determines if the uri is empty.
        /// </summary>
        public bool Empty => !PathSegments.Any();

        /// <summary>
        /// Returns the root.
        /// </summary>
        public virtual UriResource Root
        {
            get
            {
                var root = new UriResource(this);
                if (root.PathSegments.Any())
                {
                    return Take(1);
                }

                return root;
            }
        }

        /// <summary>
        /// Determines if the Uri is the root.
        /// </summary>
        public bool IsRoot => string.IsNullOrWhiteSpace(PathSegments.FirstOrDefault()?.Value);

        /// <summary>
        /// Returns the variables.
        /// </summary>
        public Dictionary<string, string> Variables
        {
            get
            {
                var dic = new Dictionary<string, string>();

                foreach (var path in PathSegments)
                {
                    if (path is IUriPathSegmentVariable variable)
                    {
                        if (!dic.ContainsKey(variable.VariableName?.ToLower()))
                        {
                            dic.Add(variable.VariableName?.ToLower(), variable.Value);
                        }
                    }
                }

                return dic;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public UriResource()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="url">The actual uri called by the web browser.</param>
        public UriResource(string uri)
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

            PathSegments.Add(new UriPathSegmentRoot());

            foreach (var p in query[0].Split('/', StringSplitOptions.RemoveEmptyEntries))
            {
                PathSegments.Add(new UriPathSegmentConstant(p));
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="url">The uri.</param>
        public UriResource(UriResource uri)
        {
            PathSegments = uri.PathSegments.Select(x => x.Copy()).ToList();
            Query = uri.Query.Select(x => new UriQuerry(x.Key, x.Value)).ToList();
            Fragment = uri.Fragment;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="segments">The path segments.</param>
        public UriResource(params IUriPathSegment[] segments)
        {
            PathSegments.Add(new UriPathSegmentRoot());

            foreach (var segment in segments.Where(x => !(x is UriPathSegmentRoot)))
            {
                PathSegments.Add(segment);
            }
        }

        /// <summary>
        /// Adds a path element.
        /// </summary>
        /// <param name="path">The path to append.</param>
        /// <returns>The extended path.</returns>
        public virtual UriResource Append(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return this;
            }

            var copy = new UriResource(this);

            foreach (var p in path.Split('/', StringSplitOptions.RemoveEmptyEntries))
            {
                copy.PathSegments.Add(new UriPathSegmentConstant(p));
            }

            return copy;
        }

        /// <summary>
        /// Adds a path element.
        /// </summary>
        /// <param name="path">The path to append.</param>
        /// <returns>The extended path.</returns>
        public virtual UriResource Append(IUriPathSegment path)
        {
            if (path == null || path.IsEmpty)
            {
                return this;
            }

            var copy = new UriResource(this);

            copy.PathSegments.Add(path);

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
        public virtual UriResource Take(int count)
        {
            var copy = new UriResource(this);
            var path = copy.PathSegments.ToList();
            copy.PathSegments.Clear();

            if (count == 0)
            {
                return new UriResource();
            }
            else if (count > 0)
            {
                (copy.PathSegments as List<IUriPathSegment>).AddRange(path.Take(count));
            }
            else if (count < 0 && Math.Abs(count) < path.Count)
            {
                (copy.PathSegments as List<IUriPathSegment>).AddRange(path.Take(path.Count + count));
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
        public UriResource Skip(int count)
        {
            if (count >= PathSegments.Count)
            {
                return null;
            }
            if (count > 0)
            {
                var copy = new UriResource(this);
                var path = copy.PathSegments.ToList();
                copy.PathSegments.Clear();
                (copy.PathSegments as List<IUriPathSegment>).AddRange(path.Skip(count));

                return copy;
            }

            return new UriResource(this);
        }

        /// <summary>
        /// Determines whether the given segment is part of the uri.
        /// </summary>
        /// <param name="segment">The segment to be tested.</param>
        /// <returns>true if successful, false otherwise.</returns>
        public virtual bool Contains(string segment)
        {
            return PathSegments.Where(x => x.Value.Equals(segment, StringComparison.OrdinalIgnoreCase)).Any();
        }

        /// <summary>
        /// Checks whether a given uri is part of that uri.
        /// </summary>
        /// <param name="uri">The Uri to be checked.</param>
        /// <returns>true if part of the uri, false otherwise.</returns>
        public bool StartsWith(UriResource uri)
        {
            return ToString().StartsWith(uri.ToString());
        }

        /// <summary>
        /// Converts the uri to a string.
        /// </summary>
        /// <returns>A string that represents the current uri.</returns>
        public override string ToString()
        {
            return PathSegments.FirstOrDefault() + string.Join
            (
                "/",
                PathSegments.Where(x => !(x is UriPathSegmentRoot)).Select(x => x.ToString())
            );
        }

        /// <summary>
        /// Combines the specified uris into a compound uri.
        /// </summary>
        /// <param name="uris">The uris to be combine.</param>
        /// <returns>A combined uri.</returns>
        public static UriResource Combine(params string[] uris)
        {
            var uri = new UriResource();

            (uri.PathSegments as List<IUriPathSegment>).AddRange(uris.Where(x => !string.IsNullOrWhiteSpace(x))
                            .SelectMany(x => x.Split('/', StringSplitOptions.RemoveEmptyEntries))
                            .Select(x => new UriPathSegmentConstant(x) as IUriPathSegment));

            return uri;
        }

        /// <summary>
        /// Combines the specified uris into a compound uri.
        /// </summary>
        /// <param name="uris">The uris to be combine.</param>
        /// <returns>A combined uri.</returns>
        public static UriResource Combine(params UriResource[] uris)
        {
            var uri = new UriResource(uris.FirstOrDefault());
            (uri.PathSegments as List<IUriPathSegment>).AddRange(uris.Skip(1).SelectMany(x => x.PathSegments.Skip(1)));

            return uri;
        }

        /// <summary>
        /// Combines the specified uris into a compound uri.
        /// </summary>
        /// <param name="uri">The first uri to be combine.</param>
        /// <param name="uris">The uris to be combine.</param>
        /// <returns>A combined uri.</returns>
        public static UriResource Combine(UriResource uri, params string[] uris)
        {
            var copy = new UriResource(uri);
            (copy.PathSegments as List<IUriPathSegment>).AddRange(uris.Where(x => !string.IsNullOrWhiteSpace(x))
                    .SelectMany(x => x.Split('/', StringSplitOptions.RemoveEmptyEntries))
                    .Select(x => new UriPathSegmentConstant(x) as IUriPathSegment));
            return copy;
        }

        /// <summary>
        /// Converts a resource uri to a normal uri.
        /// </summary>
        /// <param name="uri">The uri to convert.</param>
        public static implicit operator Uri(UriResource uri)
        {
            return new Uri(uri?.ToString(), string.IsNullOrWhiteSpace(uri?.Authority?.Host) ? UriKind.Relative : UriKind.Absolute);
        }

        /// <summary>
        /// Converts a resource uri to a normal uri.
        /// </summary>
        /// <param name="uri">The uri to convert.</param>
        public static implicit operator string(UriResource uri)
        {
            return uri?.ToString();
        }
    }
}