namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Die Standardhintergrundfarben
    /// </summary>
    public enum TypesBackgroundColor
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

    public static class TypesBackgroundColorExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="color">Die Hintergrundfarbe, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypesBackgroundColor color)
        {
            switch (color)
            {
                case TypesBackgroundColor.Primary:
                    return "bg-primary";
                case TypesBackgroundColor.Secondary:
                    return "bg-secondary";
                case TypesBackgroundColor.Success:
                    return "bg-success";
                case TypesBackgroundColor.Info:
                    return "bg-info";
                case TypesBackgroundColor.Warning:
                    return "bg-warning";
                case TypesBackgroundColor.Danger:
                    return "bg-danger";
                case TypesBackgroundColor.Light:
                    return "bg-light";
                case TypesBackgroundColor.Dark:
                    return "bg-dark";
                case TypesBackgroundColor.White:
                    return "bg-white";
                case TypesBackgroundColor.Transparent:
                    return "bg-transparent";
            }

            return string.Empty;
        }
    }
}
