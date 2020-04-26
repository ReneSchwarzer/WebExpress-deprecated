namespace WebExpress.UI.Controls
{
    public enum TypesNavJustified
    {
        Default,
        Justified
    }

    public static class TypesNavJustifiedExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypesNavJustified layout)
        {
            switch (layout)
            {
                case TypesNavJustified.Justified:
                    return "nav-justified";
            }

            return string.Empty;
        }
    }
}
