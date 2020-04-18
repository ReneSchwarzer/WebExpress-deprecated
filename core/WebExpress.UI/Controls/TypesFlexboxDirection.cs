namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Die Anordnungsmöglichkeiten
    /// </summary>
    public enum TypesFlexboxDirection
    {
        Default,
        Vertical,
        Horizontal,
        VerticalReverse,
        HorizontalReverse
    }

    public static class TypesFlexboxDirectionExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="direction">Die Anordnung, welches umgewandelt werden soll</param>
        /// <returns>Die zur Anordnung gehörende CSS-KLasse</returns>
        public static string ToClass(this TypesFlexboxDirection direction)
        {
            switch (direction)
            {
                case TypesFlexboxDirection.Vertical:
                    return "flex-column";
                case TypesFlexboxDirection.VerticalReverse:
                    return "flex-column-reverse";
                case TypesFlexboxDirection.Horizontal:
                    return "flex-row";
                case TypesFlexboxDirection.HorizontalReverse:
                    return "flex-row-reverse";
            }

            return string.Empty;
        }
    }
}
