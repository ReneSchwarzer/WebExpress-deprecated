namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Die Standardhintergrundfarben
    /// </summary>
    public enum TypeColorProgress
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
        White = 9
    }

    public static class TypeColorProgressExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="color">Die Hintergrundfarbe, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeColorProgress color)
        {
            return color switch
            {
                TypeColorProgress.Primary => "bg-primary",
                TypeColorProgress.Secondary => "bg-secondary",
                TypeColorProgress.Success => "bg-success",
                TypeColorProgress.Info => "bg-info",
                TypeColorProgress.Warning => "bg-warning",
                TypeColorProgress.Danger => "bg-danger",
                TypeColorProgress.Light => "bg-light",
                TypeColorProgress.Dark => "bg-dark",
                TypeColorProgress.White => "bg-white",
                _ => string.Empty,
            };
        }
    }
}
