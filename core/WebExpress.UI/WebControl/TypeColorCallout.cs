namespace WebExpress.UI.WebControl
{
    public enum TypeColorCallout
    {
        Default = 0,
        Primary = 1,
        Secondary = 2,
        Success = 3,
        Info = 4,
        Warning = 5,
        Danger = 6,
        Dark = 7,
        Light = 8
    }

    public static class TypeColorCalloutExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeColorCallout layout)
        {
            switch (layout)
            {
                case TypeColorCallout.Primary:
                    return "callout-primary";
                case TypeColorCallout.Secondary:
                    return "callout-secondary";
                case TypeColorCallout.Success:
                    return "callout-success";
                case TypeColorCallout.Info:
                    return "callout-info";
                case TypeColorCallout.Warning:
                    return "callout-warning";
                case TypeColorCallout.Danger:
                    return "callout-danger";
                case TypeColorCallout.Light:
                    return "callout-light";
                case TypeColorCallout.Dark:
                    return "callout-dark";
            }

            return string.Empty;
        }
    }
}
