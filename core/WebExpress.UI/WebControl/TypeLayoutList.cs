namespace WebExpress.UI.WebControl
{
    public enum TypeLayoutList
    {
        Default,
        Simple,
        Group,
        Flush,
        Horizontal
    }

    public static class TypeLayoutListExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeLayoutList layout)
        {
            switch (layout)
            {
                case TypeLayoutList.Group:
                    return "list-group";
                case TypeLayoutList.Simple:
                    return "list-unstyled";
                case TypeLayoutList.Flush:
                    return "list-group list-group-flush";
                case TypeLayoutList.Horizontal:
                    return "list-group list-group-horizontal";
            }

            return string.Empty;
        }
    }
}
