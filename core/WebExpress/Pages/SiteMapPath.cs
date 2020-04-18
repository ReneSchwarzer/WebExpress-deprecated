using System;
using WebExpress.Workers;

namespace WebExpress.Pages
{
    public class SiteMapPath
    {
        /// <summary>
        /// Liefert oder setzt den Pfad
        /// </summary>
        public string Path { get; protected set; }

        /// <summary>
        /// Liefert oder setzt, ob alle Subpfade micht berücksichtigt werden sollen
        /// </summary>
        public bool IncludeSubPaths { get; protected set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="path">Der Pfad</param>
        /// <param name="includeSubPaths">Subpfade mit einbeziehen</param>
        public SiteMapPath(string path, bool includeSubPaths)
        {
            Path = path;
            IncludeSubPaths = includeSubPaths;
        }
    }
}