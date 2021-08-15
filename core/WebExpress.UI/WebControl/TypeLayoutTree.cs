namespace WebExpress.UI.WebControl
{
    public enum TypeLayoutTree
    {
        Default,
        Flat,
        Simple,
        Group,
        Flush,
        Horizontal,
        TreeView
    }

    public static class TypeLayoutTreeExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeLayoutTree layout)
        {
            return layout switch
            {
                TypeLayoutTree.TreeView => "tree-treeview",
                TypeLayoutTree.Group => "list-group",
                TypeLayoutTree.Simple => "tree-simple",
                TypeLayoutTree.Flat => "list-unstyled",
                TypeLayoutTree.Flush => "list-group list-group-flush",
                TypeLayoutTree.Horizontal => "list-group list-group-horizontal",
                _ => string.Empty,
            };
        }
    }
}
