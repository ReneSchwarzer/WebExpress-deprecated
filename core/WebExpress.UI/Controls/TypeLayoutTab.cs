namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Die Layoutmöglichkeiten des Tabulator-Steuerelementes
    /// </summary>
    public enum TypeLayoutTab
    {
        Default,
        Menu,
        Tab,
        Pill
    }

    public static class TypeLayoutTabExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeLayoutTab layout)
        {
            switch (layout)
            {
                case TypeLayoutTab.Tab:
                    return "nav-tabs";
                case TypeLayoutTab.Pill:
                    return "nav-pills";
            }

            return string.Empty;
        }
    }
}
