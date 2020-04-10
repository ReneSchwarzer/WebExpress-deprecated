using System.Collections.Generic;
using System.Linq;
using WebExpress.Plugins;

namespace WebExpress.Pages
{
    public class Path
    {
        /// <summary>
        /// Liefert oder setzt die Pfadelemente
        /// </summary>
        public List<PathItem> Items { get; private set; } = new List<PathItem>();

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
        /// <param name="extension">Die Url-Erweiterung</param>
        /// <param name="tag">Das Etikett</param>
        public Path(IPluginContext context, string name, Path basePath, string extension, string tag = null)
            : this(context)
        {
            if (basePath != null)
            {
                Items.AddRange(basePath.Items);
            }

            Items.Add(new PathItem() { Name = name, Fragment = extension, Tag = tag });
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
            Items.AddRange(path.Items.Select(x => new PathItem(x)));
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="path">Der zu kopierende Pfad</param>
        public Path(Path path, PathItem item)
            : this(path.Context)
        {
            Items.AddRange(path.Items.Select(x => new PathItem(x)));

            Items.Add(new PathItem(item));
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="path">Der zu kopierende Pfad</param>
        public Path(IPluginContext context, PathItem item)
            : this(context)
        {
            Items.Add(new PathItem(item));
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="item">Der zu übernehmende Pfad</param>
        public Path(IPluginContext context, string item)
            : this(context)
        {
            if(item.StartsWith("/"))
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
        public Path(IPluginContext context, IEnumerable<PathItem> items)
            : this(context)
        {
            Items.AddRange(items.Select(x => new PathItem(x)));
        }

        /// <summary>
        /// Umwandlung in Stringform
        /// </summary>
        /// <returns>Die URL des Pfades</returns>
        public override string ToString()
        {
            if (Context == null)
            {
                return string.Join("/", Items.Where(x => !string.IsNullOrEmpty(x.Fragment)).Select(x => x.Fragment.Trim()));
            }

            return Context?.UrlBasePath + "/" + string.Join("/", Items.Where(x => !string.IsNullOrEmpty(x.Fragment)).Select(x => x.Fragment.Trim()));
        }
    }
}
