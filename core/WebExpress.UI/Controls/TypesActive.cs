namespace WebExpress.UI.Controls
{
    public enum TypesActive
    {
        None,
        Active,
        Disabled
    }

    public static class TypesActiveExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypesActive layout)
        {
            switch (layout)
            {
                case TypesActive.Active:
                    return "active";
                case TypesActive.Disabled:
                    return "disabled";
            }

            return string.Empty;
        }
    }
}
