namespace WebExpress.UI.WebControl
{
    public enum TypeExpandTree
    {
        None,
        Visible,
        Collapse
    }

    public static class TypeExpandTreeExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="expand">Der Expansionszustand</param>
        /// <returns>Die zum Icon gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeExpandTree expand)
        {
            return expand switch
            {
                TypeExpandTree.Collapse => "tree-node-hide",
                _ => string.Empty,
            };
        }
    }
}
