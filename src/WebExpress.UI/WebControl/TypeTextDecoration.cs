namespace WebExpress.UI.WebControl
{
    public enum TypeTextDecoration
    {
        Default,
        None
    }

    public static class TypeTextDecorationExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeTextDecoration layout)
        {
            return layout switch
            {
                TypeTextDecoration.None => "text-decoration-none",
                _ => string.Empty,
            };
        }
    }
}
