using System;
using System.Collections.Generic;

namespace WebExpress.WebUri
{
    /// <summary>
    /// Uri which consists only of the fragment (e.g. #).
    /// </summary>
    public class UriFragment : IUri
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UriFragment()
        {
        }

        /// <summary>
        /// The path (e.g. /over/there).
        /// </summary>
        public ICollection<IUriPathSegment> Path => throw new NotImplementedException();

        /// <summary>
        /// The query part (e.g. ? title=Uniform_Resource_Identifier&action=submit).
        /// </summary>
        public ICollection<UriQuerry> Query => throw new NotImplementedException();

        /// <summary>
        /// References a position within a resource (e.g. #Anchor).
        /// </summary>
        public string Fragment { get; set; }

        /// <summary>
        /// Returns the display string of the Uri
        /// </summary>
        public string Display { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Determines if the uri is empty.
        /// </summary>
        public bool Empty => throw new NotImplementedException();

        /// <summary>
        /// Returns the root.
        /// </summary>
        public IUri Root => throw new NotImplementedException();

        /// <summary>
        /// Determines if the uri is the root.
        /// </summary>
        public bool IsRoot => throw new NotImplementedException();

        /// <summary>
        /// Adds a path element.
        /// </summary>
        /// <param name="path">The path to append.</param>
        /// <returns>The extended path.</returns>
        public IUri Append(string path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a path element.
        /// </summary>
        /// <param name="path">The path to append.</param>
        /// <returns>The extended path.</returns>
        public IUri Append(IUriPathSegment path)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines whether the given segment is part of the uri.
        /// </summary>
        /// <param name="segment">The segment to be tested.</param>
        /// <returns>true if successful, false otherwise.</returns>
        public bool Contains(string segment)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks whether a given uri is part of that uri.
        /// </summary>
        /// <param name="uri">The Uri to be checked.</param>
        /// <returns>true if part of the uri, false otherwise.</returns>
        public bool StartsWith(IUri uri)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the uri to a string.
        /// </summary>
        /// <returns>The string representation of the uri.</returns>
        public override string ToString()
        {
            return "#" + Fragment;
        }
    }
}