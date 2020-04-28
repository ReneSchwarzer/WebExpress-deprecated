namespace WebExpress.UI.Controls
{
    public enum TypeColorBackgroundAlert
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

    public static class TypeColorBackgroundAlertExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeColorBackgroundAlert layout)
        {
            switch (layout)
            {
                case TypeColorBackgroundAlert.Primary:
                    return "bg-primary";
                case TypeColorBackgroundAlert.Secondary:
                    return "bg-secondary";
                case TypeColorBackgroundAlert.Success:
                    return "alert-success";
                case TypeColorBackgroundAlert.Info:
                    return "alert-info";
                case TypeColorBackgroundAlert.Warning:
                    return "alert-warning";
                case TypeColorBackgroundAlert.Danger:
                    return "alert-danger";
                case TypeColorBackgroundAlert.Light:
                    return "alert-light";
                case TypeColorBackgroundAlert.Dark:
                    return "alert-dark";
                case TypeColorBackgroundAlert.White:
                    return "bg-white";
                case TypeColorBackgroundAlert.Transparent:
                    return "bg-transparent";
            }

            return string.Empty;
        }
    }
}
