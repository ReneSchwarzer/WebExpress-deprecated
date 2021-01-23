namespace WebExpress.UI.WebControl
{
    public enum TypeColorBackgroundBadge
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

    public static class TypeColorBackgroundBadgeExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeColorBackgroundBadge layout)
        {
            switch (layout)
            {
                case TypeColorBackgroundBadge.Primary:
                    return "badge-primary";
                case TypeColorBackgroundBadge.Secondary:
                    return "bg-secondary";
                case TypeColorBackgroundBadge.Success:
                    return "badge-success";
                case TypeColorBackgroundBadge.Info:
                    return "badge-info";
                case TypeColorBackgroundBadge.Warning:
                    return "badge-warning";
                case TypeColorBackgroundBadge.Danger:
                    return "badge-danger";
                case TypeColorBackgroundBadge.Light:
                    return "badge-light";
                case TypeColorBackgroundBadge.Dark:
                    return "badge-dark";
                case TypeColorBackgroundBadge.White:
                    return "bg-white";
                case TypeColorBackgroundBadge.Transparent:
                    return "bg-transparent";
            }

            return string.Empty;
        }
    }
}
