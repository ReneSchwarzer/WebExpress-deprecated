namespace WebExpress.UI.WebControl
{
    public enum TypeColorBorder
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

    public static class TypeColorBorderExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeColorBorder layout)
        {
            return layout switch
            {
                TypeColorBorder.Primary => "border-primary",
                TypeColorBorder.Secondary => "border-secondary",
                TypeColorBorder.Success => "border-success",
                TypeColorBorder.Info => "border-info",
                TypeColorBorder.Warning => "border-warning",
                TypeColorBorder.Danger => "border-danger",
                TypeColorBorder.Light => "border-light",
                TypeColorBorder.Dark => "border-dark",
                TypeColorBorder.White => "border-white",
                TypeColorBorder.Transparent => "border-transparent",
                _ => string.Empty,
            };
        }
    }
}
