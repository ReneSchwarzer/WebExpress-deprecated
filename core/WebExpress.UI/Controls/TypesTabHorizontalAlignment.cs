namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Die horizontale Anordnung
    /// </summary>
    public enum TypesTabHorizontalAlignment
    {
        Default,
        Left,
        Center,
        Right
    }

    public static class TypesTabHorizontalAlignmentExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="alignment">Die Ausrichtung, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypesTabHorizontalAlignment alignment)
        {
            switch (alignment)
            {
                case TypesTabHorizontalAlignment.Center:
                    return "justify-content-center";
                case TypesTabHorizontalAlignment.Right:
                    return "justify-content-end";
            }

            return string.Empty;
        }
    }
}
