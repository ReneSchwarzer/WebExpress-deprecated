namespace WebExpress.Pages
{
    public class PathItemVariable : IPathItem
    {
        /// <summary>
        /// Der Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Das Etikett
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Der Name der Variable
        /// </summary>
        public string Variable { get; set; }

        /// <summary>
        /// Das Pattern
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// Das Pfadfragment
        /// </summary>
        public string Fragment { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PathItemVariable()
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="name">Der Name</param>
        /// <param name="variable">Der Variablenname</param>
        public PathItemVariable(string name, string variable)
            : this(name, variable, "([0-9A-Za-z.-]*)")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="name">Der Name</param>
        /// <param name="variable">Der Variablenname</param>
        /// <param name="pattern">Das Pattern</param>
        public PathItemVariable(string name, string variable, string pattern)
            : this(name)
        {
            Name = name;
            Variable = variable;
            Pattern = pattern;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="name">Der Name</param>
        public PathItemVariable(string name)
            : this()
        {
            Name = name;
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="item">Das zu kopierende Element</param>
        public PathItemVariable(PathItemVariable item)
        {
            Name = item.Name;
            Tag = item.Tag;
            Variable = item.Variable;
            Pattern = item.Pattern;
            Fragment = item.Fragment;
        }

        /// <summary>
        /// Vergleicht zwei variable PfadItems
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual bool Equals(PathItemVariable item)
        {
            return Name == item.Name &&
                   Fragment == item.Fragment &&
                   Variable == item.Variable &&
                   Pattern == item.Pattern &&
                   Tag == item.Tag;
        }
    }
}
