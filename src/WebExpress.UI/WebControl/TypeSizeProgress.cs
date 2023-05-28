namespace WebExpress.UI.WebControl
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
            return size switch
            {
                TypeSizeProgress.ExtraLarge => "height:40px;",
                TypeSizeProgress.Large => "height:27px;",
                TypeSizeProgress.Small => "height:10px;",
                TypeSizeProgress.ExtraSmall => "height:2px;",
                _ => string.Empty,
            };
        }
    }
}
