namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Die Standardhintergrundfarben
    /// </summary>
    public enum TypeColorBackground
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

    public static class TypeColorBackgroundExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="color">Die Hintergrundfarbe, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeColorBackground color)
        {
            return color switch
            {
                TypeColorBackground.Primary => "bg-primary",
                TypeColorBackground.Secondary => "bg-secondary",
                TypeColorBackground.Success => "bg-success",
                TypeColorBackground.Info => "bg-info",
                TypeColorBackground.Warning => "bg-warning",
                TypeColorBackground.Danger => "bg-danger",
                TypeColorBackground.Light => "bg-light",
                TypeColorBackground.Dark => "bg-dark",
                TypeColorBackground.White => "bg-white",
                TypeColorBackground.Transparent => "bg-transparent",
                _ => string.Empty,
            };
        }
    }
}
