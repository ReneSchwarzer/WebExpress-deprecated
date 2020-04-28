namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Die Größenmöglichkeiten
    /// </summary>
    public enum TypesSizeProgress
    {
        Default,
        ExtraSmall,
        Small,
        Large,
        ExtraLarge
    }

    public static class TypesSizeProgressExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="size">Die Größe, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypesSizeProgress size)
        {
            return string.Empty;
        }

        /// <summary>
        /// Umwandlung in einen CSS-Style
        /// </summary>
        /// <param name="size">Die Größe, welches umgewandelt werden soll</param>
        /// <returns>Der zur Größe gehörende CSS-Style</returns>
        public static string ToStyle(this TypesSizeProgress size)
        {
            switch (size)
            {
                case TypesSizeProgress.ExtraLarge:
                    return "height:40px;";
                case TypesSizeProgress.Large:
                    return "height:27px;";
                case TypesSizeProgress.Small:
                    return "height:10px;";
                case TypesSizeProgress.ExtraSmall:
                    return "height:2px;";
            }

            return string.Empty;
        }
    }
}
