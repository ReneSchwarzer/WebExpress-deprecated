namespace WebExpress.UI.Controls
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
            switch (layout)
            {
                case TypePanelGridRow.Auto:
                    return "container";
                case TypePanelGridRow.Fluid:
                    return "container-fluid";
            }

            return string.Empty;
        }
    }
}
