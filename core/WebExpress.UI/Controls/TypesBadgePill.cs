namespace WebExpress.UI.Controls
{
    public enum TypesBadgePill
    {
        None,
        Pill
    }

    public static class TypesBadgePillExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypesBadgePill layout)
        {
            switch (layout)
            {
                case TypesBadgePill.Pill:
                    return "badge-pill";
            }

            return string.Empty;
        }
    }
}
