namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Die Anordnungsmöglichkeiten
    /// </summary>
    public enum TypeDirection
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
        public static string ToClass(this TypeDirection direction)
        {
            switch (direction)
            {
                case TypeDirection.Vertical:
                    return "flex-column";
                case TypeDirection.VerticalReverse:
                    return "flex-column-reverse";
                case TypeDirection.Horizontal:
                    return "flex-row";
                case TypeDirection.HorizontalReverse:
                    return "flex-row-reverse";
            }

            return string.Empty;
        }
    }
}
