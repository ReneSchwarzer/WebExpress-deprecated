using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WebExpress.Plugins;

namespace WebExpress.Pages
{
    public class Path : IPath
    {
        /// <summary>
        /// Liefert oder setzt die Pfadelemente
        /// </summary>
        public List<IPathItem> Items { get; private set; } = new List<IPathItem>();

        /// <summary>
        /// Liefert oder setzt den Kontext
        /// </summary>
        private IPluginContext Context { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public Path(IPluginContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="name">Der Name</param>
        /// <param name="basePath">Der Basipfad</param>
        /// <param name="item">Die Pfaderweiterung</param>
        public Path(IPluginContext context, Path basePath, PathItem item)
            : this(context)
        {
            if (basePath != null)
            {
                Items.AddRange(basePath.Items);
            }

            Items.Add(new PathItem(item));
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="name">Der Name</param>
        /// <param name="basePath">Der Basipfad</param>
        /// <param name="item">Die Pfaderweiterung</param>
        public Path(IPluginContext context, Path basePath, PathItemVariable item)
            : this(context)
        {
            if (basePath != null)
            {
                Items.AddRange(basePath.Items);
            }

            Items.Add(new PathItemVariable(item));
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="name">Der Name</param>
        /// <param name="extension">Die Url-Erweiterung</param>
        public Path(IPluginContext context, string name, string extension)
            : this(context)
        {
            Items.Add(new PathItem() { Name = name, Fragment = extension });
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="path">Der zu kopierende Pfad</param>
        public Path(Path path)
            : this(path.Context)
        {
            foreach (var item in path.Items)
            {
                if (item is PathItem i)
                {
                    Items.Add(new PathItem(i));
                }
                else if (item is PathItemVariable v)
                {
                    Items.Add(new PathItemVariable(v));
                }
            }
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="path">Der zu kopierende Pfad</param>
        /// <param name="item">Das anzuhängende PfadItem</param>
        public Path(Path path, IPathItem item)
            : this(path)
        {
            if (item is PathItem i)
            {
                Items.Add(new PathItem(i));
            }
            else if (item is PathItemVariable v)
            {
                Items.Add(new PathItemVariable(v));
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="path">Der zu kopierende Pfad</param>
        public Path(IPluginContext context, IPathItem item)
            : this(context)
        {
            if (item is PathItem i)
            {
                Items.Add(new PathItem(i));
            }
            else if (item is PathItemVariable v)
            {
                Items.Add(new PathItemVariable(v));
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="item">Der zu übernehmende Pfad</param>
        public Path(IPluginContext context, string item)
            : this(context)
        {
            if (item.StartsWith("/"))
            {
                item = item.Substring(1);
            }

            Items.Add(new PathItem("None", item));
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="items">Der zu kopierende Pfad</param>
        public Path(IPluginContext context, IEnumerable<IPathItem> items)
            : this(context)
        {
            foreach (var item in items)
            {
                if (item is PathItem i)
                {
                    Items.Add(new PathItem(i));
                }
                else if (item is PathItemVariable v)
                {
                    Items.Add(new PathItemVariable(v));
                }
            }
        }

        /// <summary>
        /// Umwandlung in Stringform
        /// </summary>
        /// <returns>Die URL des Pfades</returns>
        public virtual string ToRawString()
        {
            var list = new List<string>();
            foreach (var item in Items)
            {
                if (item is PathItemVariable dynamic)
                {
                    if (!string.IsNullOrEmpty(dynamic.Pattern))
                    {
                        list.Add(dynamic.Pattern);
                    }
                }
                else if (item is PathItem stat)
                {
                    if (!string.IsNullOrEmpty(stat.Fragment))
                    {
                        list.Add(stat.Fragment);
                    }
                }
            }

            return Context?.UrlBasePath + "/" + string.Join("/", list);
        }

        /// <summary>
        /// Umwandlung in Stringform
        /// </summary>
        /// <returns>Die URL des Pfades</returns>
        public override string ToString()
        {
            //if (Context == null)
            //{
            //    return string.Join("/", Items.Where(x => !string.IsNullOrEmpty(x.Fragment)).Select(x => x.Fragment.Trim()));
            //}

            var list = new List<string>();
            foreach (var item in Items)
            {
                if (item is PathItemVariable dynamic)
                {
                    if (!string.IsNullOrEmpty(dynamic.Fragment))
                    {
                        list.Add(dynamic.Fragment);
                    }
                }
                else if (item is PathItem stat)
                {
                    if (!string.IsNullOrEmpty(stat.Fragment))
                    {
                        list.Add(stat.Fragment);
                    }
                }
            }

            return Context?.UrlBasePath + "/" + string.Join("/", list);
        }
    }
}
