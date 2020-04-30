namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Die Größenmöglichkeiten
    /// </summary>
    public enum TypeSizeProgress
    {
        Default = 0,
        ExtraSmall = 1,
        Small = 2,
        Large = 3,
        ExtraLarge = 4
    }

    public static class TypesSizeProgressExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="size">Die Größe, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeSizeProgress size)
        {
            return string.Empty;
        }

        /// <summary>
        /// Umwandlung in einen CSS-Style
        /// </summary>
        /// <param name="size">Die Größe, welches umgewandelt werden soll</param>
        /// <returns>Der zur Größe gehörende CSS-Style</returns>
        public static string ToStyle(this TypeSizeProgress size)
        {
            switch (size)
            {
                case TypeSizeProgress.ExtraLarge:
                    return "height:40px;";
                case TypeSizeProgress.Large:
                    return "height:27px;";
                case TypeSizeProgress.Small:
                    return "height:10px;";
                case TypeSizeProgress.ExtraSmall:
                    return "height:2px;";
            }

            return string.Empty;
        }
    }
}
