namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Die Anordnungsmöglichkeiten
    /// </summary>
    public enum TypeAlighmentDropdownMenu
    {
        Default,
        Right
    }

    public static class TypeAlighmentDropdownMenuExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="direction">Die Anordnung, welches umgewandelt werden soll</param>
        /// <returns>Die zur Anordnung gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeAlighmentDropdownMenu direction)
        {
            switch (direction)
            {
                case TypeAlighmentDropdownMenu.Right:
                    return "dropdown-menu-right";
            }

            return string.Empty;
        }
    }
}
