namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Die Standardhintergrundfarben
    /// </summary>
    public enum TypeColorLine
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

    public static class TypeColorLineExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="color">Die Hintergrundfarbe, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeColorLine color)
        {
            return color switch
            {
                TypeColorLine.Primary => "bg-primary",
                TypeColorLine.Secondary => "bg-secondary",
                TypeColorLine.Success => "bg-success",
                TypeColorLine.Info => "bg-info",
                TypeColorLine.Warning => "bg-warning",
                TypeColorLine.Danger => "bg-danger",
                TypeColorLine.Light => "bg-light",
                TypeColorLine.Dark => "bg-dark",
                _ => string.Empty,
            };
        }
    }
}
