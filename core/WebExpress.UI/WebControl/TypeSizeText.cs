namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Die Größenmöglichkeiten
    /// </summary>
    public enum TypeSizeText
    {
        Default = 0,
        ExtraSmall = 1,
        Small = 2,
        Large = 3,
        ExtraLarge = 4
    }

    public static class TypesSizeTextExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="size">Die Größe, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeSizeText size)
        {
            return string.Empty;
        }

        /// <summary>
        /// Umwandlung in einen CSS-Style
        /// </summary>
        /// <param name="size">Die Größe, welches umgewandelt werden soll</param>
        /// <returns>Der zur Größe gehörende CSS-Style</returns>
        public static string ToStyle(this TypeSizeText size)
        {
            switch (size)
            {
                case TypeSizeText.ExtraLarge:
                    return "font-size:2rem;";
                case TypeSizeText.Large:
                    return "font-size:1.5rem;";
                case TypeSizeText.Small:
                    return "font-size:0.75rem;";
                case TypeSizeText.ExtraSmall:
                    return "font-size:0.55rem;";
            }

            return string.Empty;
        }
    }
}
