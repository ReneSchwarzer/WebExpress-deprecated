using System;
using System.Collections.Generic;
using System.Text;

namespace WebExpress.Pages
{
    public class PathExtern : IPath
    {
        /// <summary>
        /// Liefert oder setzt die Uri
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="uri">Die externe Uri</param>
        public PathExtern(string uri)
        {
            Uri = uri;
        }
    }
}
