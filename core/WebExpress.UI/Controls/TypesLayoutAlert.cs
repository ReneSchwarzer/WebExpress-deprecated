namespace WebExpress.UI.Controls
{
    public enum TypesLayoutAlert
    {
        Default,
        Success,
        Info,
        Warning,
        Danger,
        Light,
        Dark
    }

    public static class TypesLayoutAlertExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypesLayoutAlert layout)
        {
            switch (layout)
            {
                case TypesLayoutAlert.Success:
                    return "alert-success";
                case TypesLayoutAlert.Info:
                    return "alert-info";
                case TypesLayoutAlert.Warning:
                    return "alert-warning";
                case TypesLayoutAlert.Danger:
                    return "alert-danger";
                case TypesLayoutAlert.Light:
                    return "alert-light";
                case TypesLayoutAlert.Dark:
                    return "alert-dark";
            }

            return string.Empty;
        }
    }
}
