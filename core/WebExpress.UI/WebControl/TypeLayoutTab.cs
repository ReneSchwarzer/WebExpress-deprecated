namespace WebExpress.UI.WebControl
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
            return layout switch
            {
                TypeLayoutTab.Tab => "nav-tabs",
                TypeLayoutTab.Pill => "nav-pills",
                _ => string.Empty,
            };
        }
    }
}
