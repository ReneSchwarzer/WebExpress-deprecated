namespace WebExpress.UI.Controls
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
            switch (layout)
            {
                case TypeColorBorder.Primary:
                    return "border-primary";
                case TypeColorBorder.Secondary:
                    return "border-secondary";
                case TypeColorBorder.Success:
                    return "border-success";
                case TypeColorBorder.Info:
                    return "border-info";
                case TypeColorBorder.Warning:
                    return "border-warning";
                case TypeColorBorder.Danger:
                    return "border-danger";
                case TypeColorBorder.Light:
                    return "border-light";
                case TypeColorBorder.Dark:
                    return "border-dark";
                case TypeColorBorder.White:
                    return "border-white";
                case TypeColorBorder.Transparent:
                    return "border-transparent";
            }

            return string.Empty;
        }
    }
}
