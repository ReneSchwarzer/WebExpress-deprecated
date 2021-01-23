namespace WebExpress.UI.Attribute
{
    /// <summary>
    /// Attribut zur Kennzeichnun einer Klasse als Plugin-Komponente
    /// </summary>
    public class SectionAttribute : System.Attribute, IComponentAttribute
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="section">Die Sektion, indem die Komponente eingebettet wird</param>
        public SectionAttribute(string section)
        {
        }

    }
}
