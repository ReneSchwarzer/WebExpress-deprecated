namespace WebExpress.UI.WebControl
{
    public enum TypeColorNavbar
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

    public static class TypeColorNavbarExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeColorNavbar layout)
        {
            switch (layout)
            {
                case TypeColorNavbar.Primary:
                    return "navbar-primary";
                case TypeColorNavbar.Secondary:
                    return "navbar-secondary";
                case TypeColorNavbar.Success:
                    return "navbar-success";
                case TypeColorNavbar.Info:
                    return "navbar-info";
                case TypeColorNavbar.Warning:
                    return "navbar-warning";
                case TypeColorNavbar.Danger:
                    return "navbar-danger";
                case TypeColorNavbar.Light:
                    return "navbar-light";
                case TypeColorNavbar.Dark:
                    return "navbar-dark";
                case TypeColorNavbar.White:
                    return "navbar-white";
                case TypeColorNavbar.Transparent:
                    return "navbar-transparent";
            }

            return string.Empty;
        }
    }
}
