namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Die Layoutmöglichkeiten des Grid-Systems
    /// </summary>
    public enum TypePanelGridRow
    {
        None,
        Auto,
        Fluid
    }

    public static class TypePanelGridRowExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypePanelGridRow layout)
        {
            return layout switch
            {
                TypePanelGridRow.Auto => "container",
                TypePanelGridRow.Fluid => "container-fluid",
                _ => string.Empty,
            };
        }
    }
}
