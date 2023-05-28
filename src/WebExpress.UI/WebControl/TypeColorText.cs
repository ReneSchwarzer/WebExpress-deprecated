namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Die Standardtextfarben
    /// </summary>
    public enum TypeColorText
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
        Muted = 11
    }

    public static class TypeColorTextExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="color">Die Farbe, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeColorText color)
        {
            return color switch
            {
                TypeColorText.Muted => "text-muted",
                TypeColorText.Primary => "text-primary",
                TypeColorText.Secondary => "text-secondary",
                TypeColorText.Success => "text-success",
                TypeColorText.Info => "text-info",
                TypeColorText.Warning => "text-warning",
                TypeColorText.Danger => "text-danger",
                TypeColorText.Light => "text-light",
                TypeColorText.Dark => "text-dark",
                TypeColorText.White => "text-white",
                _ => string.Empty,
            };
        }
    }
}
