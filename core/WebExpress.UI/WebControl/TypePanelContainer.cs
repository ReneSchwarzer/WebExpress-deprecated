namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Die Layoutmöglichkeiten des Panels-Steuerelementes
    /// </summary>
    public enum TypePanelContainer
    {
        None,
        Default,
        Fluid
    }

    public static class TypePanelFluidExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypePanelContainer layout)
        {
            switch (layout)
            {
                case TypePanelContainer.Default:
                    return "container";
                case TypePanelContainer.Fluid:
                    return "container-fluid";
            }

            return string.Empty;
        }
    }
}
