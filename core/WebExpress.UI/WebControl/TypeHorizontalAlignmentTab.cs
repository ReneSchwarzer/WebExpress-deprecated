namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Die horizontale Anordnung
    /// </summary>
    public enum TypeHorizontalAlignmentTab
    {
        Default,
        Left,
        Center,
        Right
    }

    public static class TypeHorizontalAlignmentTabExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="alignment">Die Ausrichtung, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeHorizontalAlignmentTab alignment)
        {
            return alignment switch
            {
                TypeHorizontalAlignmentTab.Center => "justify-content-center",
                TypeHorizontalAlignmentTab.Right => "justify-content-end",
                _ => string.Empty,
            };
        }
    }
}
