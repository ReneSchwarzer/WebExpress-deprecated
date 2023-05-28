namespace WebExpress.UI.WebControl
{
    public enum TypeJustifiedFlexbox
    {
        None,
        Start,
        End,
        Center,
        Between,
        Around
    }

    public static class TypeTypeJustifiedFlexboxExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeJustifiedFlexbox layout)
        {
            return layout switch
            {
                TypeJustifiedFlexbox.Start => "justify-content-start",
                TypeJustifiedFlexbox.End => "justify-content-end",
                TypeJustifiedFlexbox.Center => "justify-content-center",
                TypeJustifiedFlexbox.Between => "justify-content-between",
                TypeJustifiedFlexbox.Around => "justify-content-around",
                _ => string.Empty,
            };
        }
    }

}
