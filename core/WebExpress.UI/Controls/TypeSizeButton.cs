namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Die Größenmöglichkeiten
    /// </summary>
    public enum TypeSizeButton
    {
        Default,
        Small,
        Large
    }

    public static class TypesSizeExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="size">Die Größe, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeSizeButton size)
        {
            switch (size)
            {
                case TypeSizeButton.Large:
                    return "btn-lg";
                    break;
                case TypeSizeButton.Small:
                    return "btn-sm";
            }

            return string.Empty;
        }
    }
}
