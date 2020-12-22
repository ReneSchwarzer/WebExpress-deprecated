namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Die Layoutmöglichkeiten der Formulare
    /// </summary>
    public enum TypeLayoutFormular
    {
        Default,
        Inline,
        Horizontal
    }

    public static class TypeLayoutFormularExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeLayoutFormular layout)
        {
            switch (layout)
            {
                case TypeLayoutFormular.Inline:
                    return "form-inline";
            }

            return string.Empty;
        }
    }
}
