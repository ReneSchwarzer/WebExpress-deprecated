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
            return layout switch
            {
                TypeColorCallout.Primary => "callout-primary",
                TypeColorCallout.Secondary => "callout-secondary",
                TypeColorCallout.Success => "callout-success",
                TypeColorCallout.Info => "callout-info",
                TypeColorCallout.Warning => "callout-warning",
                TypeColorCallout.Danger => "callout-danger",
                TypeColorCallout.Light => "callout-light",
                TypeColorCallout.Dark => "callout-dark",
                _ => string.Empty,
            };
        }
    }
}
