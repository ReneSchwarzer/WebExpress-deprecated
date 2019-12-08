using System.Collections.Generic;
using System.Linq;

namespace WebExpress.Pages
{
    public class Path
    {
        /// <summary>
        /// Liefert oder setzt die Pfadelemente
        /// </summary>
        public List<PathItem> Items { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="basePath">Der Basipfad</param>
        public Path()
        {
            Items = new List<PathItem>();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="name">Der Name</param>
        /// <param name="basePath">Der Basipfad</param>
        /// <param name="extension">Die Url-Erweiterung</param>
        /// <param name="tag">Das Etikett</param>
        public Path(string name, Path basePath, string extension, string tag = null)
            : this()
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
        /// <param name="name">Der Name</param>
        /// <param name="extension">Die Url-Erweiterung</param>
        public Path(string name, string extension)
            : this()
        {
            Items.Add(new PathItem() { Name = name, Fragment = extension });
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="path">Der zu kopierende Pfad</param>
        public Path(Path path)
            : this()
        {
            Items.AddRange(path.Items.Select(x => new PathItem(x)));
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="path">Der zu kopierende Pfad</param>
        public Path(Path path, PathItem item)
            : this()
        {
            Items.AddRange(path.Items.Select(x => new PathItem(x)));

            Items.Add(new PathItem(item));
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="path">Der zu kopierende Pfad</param>
        public Path(PathItem item)
            : this()
        {
            Items.Add(new PathItem(item));
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="items">Der zu kopierende Pfad</param>
        public Path(IEnumerable<PathItem> items)
            : this()
        {
            Items.AddRange(items.Select(x => new PathItem(x)));
        }

        /// <summary>
        /// Umwandlung in Stringform
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "/" + string.Join("/", Items.Where(x => !string.IsNullOrEmpty(x.Fragment)).Select(x => x.Fragment.Trim()));
        }
    }
}
