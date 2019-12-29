namespace WebExpress.UI.Controls
{
    public enum TypesLayoutCard
    {
        Default,
        Primary,
        Secondary,
        Success,
        Info,
        Warning,
        Danger,
        Light,
        Dark
    }

    public static class TypesLayoutCardExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypesLayoutCard layout)
        {
            switch (layout)
            {
                case TypesLayoutCard.Primary:
                    return "bg-primary";
                case TypesLayoutCard.Secondary:
                    return "bg-secondary";
                case TypesLayoutCard.Success:
                    return "bg-success";
                case TypesLayoutCard.Info:
                    return "bg-info";
                case TypesLayoutCard.Warning:
                    return "bg-warning";
                case TypesLayoutCard.Danger:
                    return "bg-danger";
                case TypesLayoutCard.Light:
                    return "bg-light";
                case TypesLayoutCard.Dark:
                    return "bg-dark";
            }

            return string.Empty;
        }
    }
}
