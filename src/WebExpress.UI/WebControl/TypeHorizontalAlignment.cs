namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Die horizontale Anordnung
    /// </summary>
    public enum TypeHorizontalAlignment
    {
        Default,
        Left,
        Right
    }

    public static class TypesHorizontalAlignmentExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="alignment">Die Ausrichtung, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeHorizontalAlignment alignment)
        {
            return alignment switch
            {
                TypeHorizontalAlignment.Left => "float-left",
                TypeHorizontalAlignment.Right => "float-right",
                _ => string.Empty,
            };
        }
    }
}
