namespace WebExpress.UI.Controls
{
    public enum TypesNavOrientation
    {
        Default,
        Horizontal,
        Vertical
    }

    public static class TypesNavVerticalExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypesNavOrientation layout)
        {
            switch (layout)
            {
                case TypesNavOrientation.Vertical:
                    return "flex-column";
            }

            return string.Empty;
        }
    }
}
