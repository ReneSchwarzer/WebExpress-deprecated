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
            switch (layout)
            {
                case TypeJustifiedFlexbox.Start:
                    return "justify-content-start";
                case TypeJustifiedFlexbox.End:
                    return "justify-content-end";
                case TypeJustifiedFlexbox.Center:
                    return "justify-content-center";
                case TypeJustifiedFlexbox.Between:
                    return "justify-content-between";
                case TypeJustifiedFlexbox.Around:
                    return "justify-content-around";
            }

            return string.Empty;
        }
    }

}
