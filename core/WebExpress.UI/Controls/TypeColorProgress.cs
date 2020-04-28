namespace WebExpress.UI.Controls
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
            switch (color)
            {
                case TypeColorProgress.Primary:
                    return "bg-primary";
                case TypeColorProgress.Secondary:
                    return "bg-secondary";
                case TypeColorProgress.Success:
                    return "bg-success";
                case TypeColorProgress.Info:
                    return "bg-info";
                case TypeColorProgress.Warning:
                    return "bg-warning";
                case TypeColorProgress.Danger:
                    return "bg-danger";
                case TypeColorProgress.Light:
                    return "bg-light";
                case TypeColorProgress.Dark:
                    return "bg-dark";
                case TypeColorProgress.White:
                    return "bg-white";
            }

            return string.Empty;
        }
    }
}
