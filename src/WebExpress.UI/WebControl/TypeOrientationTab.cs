namespace WebExpress.UI.WebControl
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
            return layout switch
            {
                TypeOrientationTab.Vertical => "flex-column",
                _ => string.Empty,
            };
        }
    }
}
