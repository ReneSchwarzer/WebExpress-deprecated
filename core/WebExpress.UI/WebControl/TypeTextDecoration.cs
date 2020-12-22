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
            switch (layout)
            {
                case TypeTextDecoration.None:
                    return "text-decoration-none";
            }

            return string.Empty;
        }
    }
}
