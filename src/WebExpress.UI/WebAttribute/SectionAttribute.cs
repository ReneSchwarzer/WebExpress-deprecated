namespace WebExpress.UI.WebAttribute
{
    /// <summary>
    /// Attribute to identify a section.
    /// </summary>
    public class SectionAttribute : System.Attribute, IFragmentAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="section">The section where the component is embedded.</param>
        public SectionAttribute(string section)
        {
        }

    }
}
