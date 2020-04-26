namespace WebExpress.UI.Controls
{
    public enum TypesDismissibleAlert
    {
        None,
        Dismissible
    }

    public static class TypesDismissibleAlertExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypesDismissibleAlert layout)
        {
            switch (layout)
            {
                case TypesDismissibleAlert.Dismissible:
                    return "alert-dismissible";
            }

            return string.Empty;
        }
    }
}
