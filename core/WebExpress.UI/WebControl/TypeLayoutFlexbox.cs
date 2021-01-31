namespace WebExpress.UI.WebControl
{
    public enum TypeLayoutFlexbox
    {
        None,
        Default,
        Inline
    }

    public static class TypeInlineFlexboxExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeLayoutFlexbox layout)
        {
            return layout switch
            {
                TypeLayoutFlexbox.Default => "d-flex",
                TypeLayoutFlexbox.Inline => "d-inline-flex",
                _ => string.Empty,
            };
        }
    }
}
