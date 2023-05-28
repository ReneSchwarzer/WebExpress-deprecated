namespace WebExpress.UI.WebControl
{
    public enum TypeColorBackgroundList
    {
        Default = 0,
        Primary = 1,
        Secondary = 2,
        Success = 3,
        Info = 4,
        Warning = 5,
        Danger = 6,
        Dark = 7,
        Light = 8,
        White = 9,
        Transparent = 10
    }

    public static class TypeColorBackgroundListExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeColorBackgroundList layout)
        {
            return layout switch
            {
                TypeColorBackgroundList.Primary => "list-group-item-primary",
                TypeColorBackgroundList.Secondary => "list-group-item-secondary",
                TypeColorBackgroundList.Success => "list-group-item-success",
                TypeColorBackgroundList.Info => "list-group-item-info",
                TypeColorBackgroundList.Warning => "list-group-item-warning",
                TypeColorBackgroundList.Danger => "list-group-item-danger",
                TypeColorBackgroundList.Light => "list-group-item-light",
                TypeColorBackgroundList.Dark => "list-group-item-dark",
                TypeColorBackgroundList.White => "bg-white",
                TypeColorBackgroundList.Transparent => "bg-transparent",
                _ => string.Empty,
            };
        }
    }
}
