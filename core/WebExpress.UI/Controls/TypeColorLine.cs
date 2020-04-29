namespace WebExpress.UI.Controls
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
            switch (color)
            {
                case TypeColorLine.Primary:
                    return "bg-primary";
                case TypeColorLine.Secondary:
                    return "bg-secondary";
                case TypeColorLine.Success:
                    return "bg-success";
                case TypeColorLine.Info:
                    return "bg-info";
                case TypeColorLine.Warning:
                    return "bg-warning";
                case TypeColorLine.Danger:
                    return "bg-danger";
                case TypeColorLine.Light:
                    return "bg-light";
                case TypeColorLine.Dark:
                    return "bg-dark";
            }

            return string.Empty;
        }
    }
}
