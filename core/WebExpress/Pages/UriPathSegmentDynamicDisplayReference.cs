namespace WebExpress.Pages
{
    /// <summary>
    /// Referentiert eine Pfadvariable
    /// </summary>
    public class UriPathSegmentDynamicDisplayReference : IUriPathSegmentDynamicDisplayItem
    {
        /// <summary>
        /// Liefert oder setzt den Namen
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="name">Der Name</param>
        public UriPathSegmentDynamicDisplayReference(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Wandelt die Referenz in einen String um
        /// </summary>
        /// <returns>Die Stringrepräsentation der Referenz</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}