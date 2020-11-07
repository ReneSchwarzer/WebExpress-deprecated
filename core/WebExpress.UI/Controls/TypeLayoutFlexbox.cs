namespace WebExpress.UI.Controls
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
            switch (layout)
            {
                case TypeLayoutFlexbox.Default:
                    return "d-flex";
                case TypeLayoutFlexbox.Inline:
                    return "d-inline-flex";
            }

            return string.Empty;
        }
    }
}
