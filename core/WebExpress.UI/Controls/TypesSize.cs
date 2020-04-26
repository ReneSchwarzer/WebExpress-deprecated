namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Die Größenmöglichkeiten
    /// </summary>
    public enum TypesSize
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
        public static string ToClass(this TypesSize size)
        {
            switch (size)
            {
                case TypesSize.Large:
                    return "btn-lg";
                    break;
                case TypesSize.Small:
                    return "btn-sm";
            }

            return string.Empty;
        }
    }
}
