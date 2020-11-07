namespace WebExpress.UI.Controls
{
    public enum TypeWrap
    {
        None,
        Nowrap,
        Wrap,
        Reverse
    }

    public static class TypeWrapExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeWrap layout)
        {
            switch (layout)
            {
                case TypeWrap.Nowrap:
                    return "flex-nowrap";
                case TypeWrap.Wrap:
                    return "flex-wrap";
                case TypeWrap.Reverse:
                    return "flex-wrap-reverse";
            }

            return string.Empty;
        }
    }

}
