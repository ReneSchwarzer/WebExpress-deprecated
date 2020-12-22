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
            switch (color)
            {
                case TypeColor.Primary:
                    return "primary";
                case TypeColor.Secondary:
                    return "secondary";
                case TypeColor.Success:
                    return "success";
                case TypeColor.Info:
                    return "info";
                case TypeColor.Warning:
                    return "warning";
                case TypeColor.Danger:
                    return "danger";
                case TypeColor.Light:
                    return "light";
                case TypeColor.Dark:
                    return "dark";
                case TypeColor.White:
                    return "white";
                case TypeColor.Transparent:
                    return "transparent";
                case TypeColor.Mute:
                    return "mute";
            }

            return string.Empty;
        }
    }
}
