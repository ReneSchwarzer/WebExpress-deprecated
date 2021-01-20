namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Die Standardhintergrundfarben
    /// </summary>
    public enum TypeColor
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
        Transparent = 10,
        Mute = 11,
        User = 12
    }

    public static class TypeColorExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="color">Die Hintergrundfarbe, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeColor color)
        {
            return color switch
            {
                TypeColor.Primary => "primary",
                TypeColor.Secondary => "secondary",
                TypeColor.Success => "success",
                TypeColor.Info => "info",
                TypeColor.Warning => "warning",
                TypeColor.Danger => "danger",
                TypeColor.Light => "light",
                TypeColor.Dark => "dark",
                TypeColor.White => "white",
                TypeColor.Transparent => "transparent",
                TypeColor.Mute => "mute",
                _ => string.Empty,
            };
        }
    }
}
