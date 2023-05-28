namespace WebExpress.UI.WebControl
{
    public enum TypePillBadge
    {
        None,
        Pill
    }

    public static class TypePillBadgeExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypePillBadge layout)
        {
            return layout switch
            {
                TypePillBadge.Pill => "badge-pill",
                _ => string.Empty,
            };
        }
    }
}
