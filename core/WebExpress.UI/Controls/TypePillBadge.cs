namespace WebExpress.UI.Controls
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
            switch (layout)
            {
                case TypePillBadge.Pill:
                    return "badge-pill";
            }

            return string.Empty;
        }
    }
}
