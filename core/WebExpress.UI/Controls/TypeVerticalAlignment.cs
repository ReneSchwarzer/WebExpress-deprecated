namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Die Ausrichtung von Inline-Elementen
    /// </summary>
    public enum TypeVerticalAlignment
    {
        Default = 0,
        Baseline = 1,
        Top = 2,
        Middle = 3,
        Bottom = 4,
        TextTop = 5,
        TextBottom = 6
    }

    public static class TypeVerticalAlignmentExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="color">Die Hintergrundfarbe, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeVerticalAlignment color)
        {
            switch (color)
            {
                case TypeVerticalAlignment.Baseline:
                    return "align-baseline";
                case TypeVerticalAlignment.Top:
                    return "align-top";
                case TypeVerticalAlignment.Middle:
                    return "align-middle";
                case TypeVerticalAlignment.Bottom:
                    return "align-bottom";
                case TypeVerticalAlignment.TextTop:
                    return "align-text-top";
                case TypeVerticalAlignment.TextBottom:
                    return "align-text-bottom";
            }

            return string.Empty;
        }
    }
}
