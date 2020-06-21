namespace WebExpress.UI.Controls
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
            switch (color)
            {
                case TypeColorText.Muted:
                    return "text-muted";
                case TypeColorText.Primary:
                    return "text-primary";
                case TypeColorText.Secondary:
                    return "text-secondary";
                case TypeColorText.Success:
                    return "text-success";
                case TypeColorText.Info:
                    return "text-info";
                case TypeColorText.Warning:
                    return "text-warning";
                case TypeColorText.Danger:
                    return "text-danger";
                case TypeColorText.Light:
                    return "text-light";
                case TypeColorText.Dark:
                    return "text-dark";
                case TypeColorText.White:
                    return "text-white";
            }

            return string.Empty;
        }
    }
}
