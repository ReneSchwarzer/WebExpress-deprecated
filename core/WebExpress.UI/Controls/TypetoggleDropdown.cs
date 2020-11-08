namespace WebExpress.UI.Controls
{
    public enum TypeToggleDropdown
    {
        None,
        Toggle
    }

    public static class TypeToggleDropdownExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeToggleDropdown layout)
        {
            switch (layout)
            {
                case TypeToggleDropdown.Toggle:
                    return "dropdown-toggle";
            }

            return string.Empty;
        }
    }
}
