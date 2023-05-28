namespace WebExpress.UI.WebControl
{
    public enum TypeFixed
    {
        None,
        Top,
        Bottom
    }

    public static class TypeFixedExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeFixed layout)
        {
            return layout switch
            {
                TypeFixed.Top => "fixed-top",
                TypeFixed.Bottom => "fixed-bottom",
                _ => string.Empty,
            };
        }
    }
}
