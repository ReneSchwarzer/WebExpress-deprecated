namespace WebExpress.UI.Controls
{
    public enum TypeOrientationTab
    {
        Default,
        Horizontal,
        Vertical
    }

    public static class TypeOrientationTabExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeOrientationTab layout)
        {
            switch (layout)
            {
                case TypeOrientationTab.Vertical:
                    return "flex-column";
            }

            return string.Empty;
        }
    }
}
