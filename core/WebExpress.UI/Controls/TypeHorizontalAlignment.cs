namespace WebExpress.UI.Controls
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
            switch (alignment)
            {
                case TypeHorizontalAlignment.Left:
                    return "float-left";
                case TypeHorizontalAlignment.Right:
                    return "float-right";
            }

            return string.Empty;
        }
    }
}
