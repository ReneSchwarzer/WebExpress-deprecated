namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Die Layoutmöglichkeiten des Tabulator-Steuerelementes
    /// </summary>
    public enum TypesLayoutTab
    {
        Default,
        Menu,
        Tab,
        Pill
    }

    public static class TypesLayoutTabExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypesLayoutTab layout)
        {
            switch (layout)
            {
                case TypesLayoutTab.Tab:
                    return "nav-tabs";
                case TypesLayoutTab.Pill:
                    return "nav-pills";
            }

            return string.Empty;
        }
    }
}
