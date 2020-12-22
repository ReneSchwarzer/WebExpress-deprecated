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
            switch (layout)
            {
                case TypeFixed.Top:
                    return "fixed-top";
                case TypeFixed.Bottom:
                    return "fixed-bottom";
            }

            return string.Empty;
        }
    }
}
