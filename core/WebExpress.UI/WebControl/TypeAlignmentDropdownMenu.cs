namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Die Anordnungsmöglichkeiten
    /// </summary>
    public enum TypeAlignmentDropdownMenu
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
        public static string ToClass(this TypeAlignmentDropdownMenu direction)
        {
            return direction switch
            {
                TypeAlignmentDropdownMenu.Right => "dropdown-menu-lg-end",
                _ => string.Empty,
            };
        }
    }
}
