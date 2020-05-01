namespace WebExpress.UI.Controls
{
    public enum TypeJustifiedTab
    {
        Default,
        Justified
    }

    public static class TypeJustifiedTabExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeJustifiedTab layout)
        {
            switch (layout)
            {
                case TypeJustifiedTab.Justified:
                    return "nav-justified";
            }

            return string.Empty;
        }
    }
}
