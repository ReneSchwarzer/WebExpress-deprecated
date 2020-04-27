namespace WebExpress.UI.Controls
{
    public enum TypesLayoutCallout
    {
        Default = 0,
        Primary,
        Secondary,
        Success = 3,
        Info = 4,
        Warning = 5,
        Danger = 6,
        Dark = 7,
        Light = 8
    }

    public static class TypesLayoutCalloutExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypesLayoutCallout layout)
        {
            switch (layout)
            {
                case TypesLayoutCallout.Primary:
                    return "callout-primary";
                case TypesLayoutCallout.Secondary:
                    return "callout-secondary";
                case TypesLayoutCallout.Success:
                    return "callout-success";
                case TypesLayoutCallout.Info:
                    return "callout-info";
                case TypesLayoutCallout.Warning:
                    return "callout-warning";
                case TypesLayoutCallout.Danger:
                    return "callout-danger";
                case TypesLayoutCallout.Light:
                    return "callout-light";
                case TypesLayoutCallout.Dark:
                    return "callout-dark";
            }

            return string.Empty;
        }
    }
}
