namespace WebExpress.UI.WebControl
{
    public enum TypeLayoutTreeItem
    {
        Default,
        Flat,
        Simple,
        Group,
        Flush,
        Horizontal,
        TreeView
    }

    public static class TypeLayoutTreeItemExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeLayoutTreeItem layout)
        {
            return layout switch
            {
                TypeLayoutTreeItem.TreeView => "list-treeview",
                TypeLayoutTreeItem.Group => "list-group",
                TypeLayoutTreeItem.Simple => "list-simple",
                TypeLayoutTreeItem.Flat => "list-unstyled",
                TypeLayoutTreeItem.Flush => "list-group list-group-flush",
                TypeLayoutTreeItem.Horizontal => "list-group list-group-horizontal",
                _ => string.Empty,
            };
        }
    }
}
