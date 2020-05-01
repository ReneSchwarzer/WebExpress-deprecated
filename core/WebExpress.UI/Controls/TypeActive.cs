namespace WebExpress.UI.Controls
{
    public enum TypeActive
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
        public static string ToClass(this TypeActive layout)
        {
            switch (layout)
            {
                case TypeActive.Active:
                    return "active";
                case TypeActive.Disabled:
                    return "disabled";
            }

            return string.Empty;
        }
    }
}
