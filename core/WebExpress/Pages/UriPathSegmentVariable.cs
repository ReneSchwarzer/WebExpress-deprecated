namespace WebExpress.Pages
{
    public class UriPathSegmentVariable
    {
        /// <summary>
        /// Liefert oder setzt den Namen
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Liefert oder setzt das Ersetzungsmuster
        /// </summary>
        public string Pattern { get; protected set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="name">Der Name</param>
        /// <param name="pattern">Das Muster</param>
        public UriPathSegmentVariable(string name, string pattern)
        {
            Name = name;
            Pattern = pattern;
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="variable">Das zu kopierende Objekt</param>
        public UriPathSegmentVariable(UriPathSegmentVariable variable)
        {
            Name = variable.Name;
            Pattern = variable.Pattern;
        }

        /// <summary>
        /// Wandelt die ID in einen String um
        /// </summary>
        /// <returns>Die Stringrepräsentation der Uri</returns>
        public override string ToString()
        {
            return "$Name" + ": " + Pattern;
        }
    }
}