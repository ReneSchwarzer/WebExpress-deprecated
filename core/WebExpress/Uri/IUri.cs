using System.Collections.Generic;

namespace WebExpress.Uri
{
    /// <summary>
    /// Uniform resource identifier (RFC 3986).
    /// </summary>
    public interface IUri
    {
        /// <summary>
        /// The path (e.g. /over/there).
        /// </summary>
        ICollection<IUriPathSegment> Path { get; }

        /// <summary>
        /// The query part (e.g. ?title=Uniform_Resource_Identifier&action=submit).
        /// </summary>
        ICollection<UriQuerry> Query { get; }

        /// <summary>
        /// References a position within a resource (e.g. #Anchor).
        /// </summary>
        string Fragment { get; set; }

        /// <summary>
        /// Returns the display string of the Uri
        /// </summary>
        string Display { get; set; }

        /// <summary>
        /// Determines if the uri is empty.
        /// </summary>
        bool Empty { get; }

        /// <summary>
        /// Returns the root.
        /// </summary>
        IUri Root { get; }

        /// <summary>
        /// Determines if the Uri is the root.
        /// </summary>
        bool IsRoot { get; }

        /// <summary>
        /// Adds a path element.
        /// </summary>
        /// <param name="path">The path to append.</param>
        /// <returns>The extended path.</returns>
        IUri Append(string path);

        /// <summary>
        /// Adds a path element.
        /// </summary>
        /// <param name="path">The path to append.</param>
        /// <returns>The extended path.</returns>
        IUri Append(IUriPathSegment path);

        /// <summary>
        /// Return a shortened uri containing n-elements.
        /// count > 0 count elements are included
        /// count < 0 count elements are truncated
        /// count = 0 an empty uri is returned
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>The sub uri.</returns>
        IUri Take(int count);

        /// <summary>
        /// Return a shortened uri by not including the first n elements.
        /// count > 0 count elements are skipped
        /// count <= 0 an empty Uri is returned
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>The sub uri.</returns>
        IUri Skip(int count);

        /// <summary>
        /// Determines whether the given segment is part of the uri.
        /// </summary>
        /// <param name="segment">The segment to be tested.</param>
        /// <returns>true if successful, false otherwise.</returns>
        bool Contains(string segment);

        /// <summary>
        /// Checks whether a given uri is part of that uri.
        /// </summary>
        /// <param name="uri">The Uri to be checked.</param>
        /// <returns>true if part of the uri, false otherwise.</returns>
        bool StartsWith(IUri uri);
    }
}