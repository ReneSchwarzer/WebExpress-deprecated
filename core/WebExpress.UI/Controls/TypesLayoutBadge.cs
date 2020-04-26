namespace WebExpress.UI.Controls
{
    public enum TypesLayoutBadge
    {
        Default,
        Primary,
        Success,
        Info,
        Warning,
        Danger,
        Light,
        Dark,
        Color
    }

    public static class TypesLayoutBadgeExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypesLayoutBadge layout)
        {
            switch (layout)
            {
                case TypesLayoutBadge.Primary:
                    return "badge-primary";
                case TypesLayoutBadge.Success:
                    return "badge-success";
                case TypesLayoutBadge.Info:
                    return "badge-info";
                case TypesLayoutBadge.Warning:
                    return "badge-warning";
                case TypesLayoutBadge.Danger:
                    return "badge-danger";
                case TypesLayoutBadge.Light:
                    return "badge-light";
                case TypesLayoutBadge.Dark:
                    return "badge-dark";
                case TypesLayoutBadge.Color:
                    return "badge-dark";
            }

            return string.Empty;
        }
    }
}
