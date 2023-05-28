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
            return layout switch
            {
                TypeLayoutList.Group => "list-group",
                TypeLayoutList.Simple => "list-unstyled",
                TypeLayoutList.Flush => "list-group list-group-flush",
                TypeLayoutList.Horizontal => "list-group list-group-horizontal",
                _ => string.Empty,
            };
        }
    }
}
