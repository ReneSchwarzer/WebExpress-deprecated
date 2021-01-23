namespace WebExpress.UI.WebControl
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

    public static class TypeSizeButtonExtensions
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
                case TypeSizeButton.Small:
                    return "btn-sm";
            }

            return string.Empty;
        }
    }
}
