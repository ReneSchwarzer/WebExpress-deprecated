namespace WebExpress.UI.WebControl
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
            return layout switch
            {
                TypeWrap.Nowrap => "flex-nowrap",
                TypeWrap.Wrap => "flex-wrap",
                TypeWrap.Reverse => "flex-wrap-reverse",
                _ => string.Empty,
            };
        }
    }

}
