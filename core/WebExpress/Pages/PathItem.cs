namespace WebExpress.Pages
{
    public class PathItem
    {
        /// <summary>
        /// Der Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Das Pfadfragment
        /// </summary>
        public string Fragment { get; set; }

        /// <summary>
        /// Das Etikett
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PathItem()
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="name">Der Name</param>
        /// <param name="extension">Die Url-Erweiterung</param>
        public PathItem(string name, string extension)
            : this()
        {
            Name = name;
            Fragment = extension;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="name">Der Name</param>
        public PathItem(string name)
            : this()
        {
            Name = name;
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="item">Das zu kopierende Element</param>
        public PathItem(PathItem item)
        {
            Name = item.Name;
            Fragment = item.Fragment;
            Tag = item.Tag;
        }
    }
}
