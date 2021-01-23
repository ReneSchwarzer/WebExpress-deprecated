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
            switch (color)
            {
                case TypeColorBackground.Primary:
                    return "bg-primary";
                case TypeColorBackground.Secondary:
                    return "bg-secondary";
                case TypeColorBackground.Success:
                    return "bg-success";
                case TypeColorBackground.Info:
                    return "bg-info";
                case TypeColorBackground.Warning:
                    return "bg-warning";
                case TypeColorBackground.Danger:
                    return "bg-danger";
                case TypeColorBackground.Light:
                    return "bg-light";
                case TypeColorBackground.Dark:
                    return "bg-dark";
                case TypeColorBackground.White:
                    return "bg-white";
                case TypeColorBackground.Transparent:
                    return "bg-transparent";
            }

            return string.Empty;
        }
    }
}
