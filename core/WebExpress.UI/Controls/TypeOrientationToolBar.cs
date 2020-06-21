namespace WebExpress.UI.Controls
{
    public enum TypeOrientationToolBar
    {
        Default,
        Horizontal,
        Vertical
    }

    public static class TypeOrientationToolBarExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeOrientationToolBar layout)
        {
            switch (layout)
            {
                case TypeOrientationToolBar.Horizontal:
                    return string.Empty;
            }

            return "navbar-expand-sm";
        }
    }
}
