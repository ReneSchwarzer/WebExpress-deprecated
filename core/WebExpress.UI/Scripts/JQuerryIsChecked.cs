namespace WebExpress.UI.Scripts
{
    public class JQuerryIsChecked : JQuerryIs
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="selector">Der Selektor</param>
        public JQuerryIsChecked(string selector)
            : base(selector, "checked")
        {
        }
    }
}
