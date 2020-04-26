namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Die Standardtextfarben
    /// </summary>
    public enum TypesTextColor
    {
        Default = 0,
        Primary = 1,
        Success = 3,
        Info = 4,
        Warning = 5,
        Danger = 6,
        Dark = 7,
        Light = 8,
        White = 9,
        Muted = 11
    }

    public static class TypesTextColorExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="color">Die Farbe, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypesTextColor color)
        {
            switch (color)
            {
                case TypesTextColor.Muted:
                    return "text-muted";
                case TypesTextColor.Primary:
                    return "text-primary";
                case TypesTextColor.Success:
                    return "text-success";
                case TypesTextColor.Info:
                    return "text-info";
                case TypesTextColor.Warning:
                    return "text-warning";
                case TypesTextColor.Danger:
                    return "text-danger";
                case TypesTextColor.Light:
                    return "text-light";
                case TypesTextColor.Dark:
                    return "text-dark";
                case TypesTextColor.White:
                    return "text-white";
            }

            return string.Empty;
        }
    }
}
