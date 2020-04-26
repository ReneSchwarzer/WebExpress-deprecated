namespace WebExpress.UI.Controls
{
    public enum TypesLayoutList
    {
        Default,
        Simple,
        Inline,
        Group
    }

    public static class TypesLayoutListExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypesLayoutList layout)
        {
            switch (layout)
            {
                case TypesLayoutList.Group:
                    return "list-group";
                case TypesLayoutList.Simple:
                    return "list-unstyled";
                case TypesLayoutList.Inline:
                    return "list-inline";
            }

            return string.Empty;
        }
    }
}
