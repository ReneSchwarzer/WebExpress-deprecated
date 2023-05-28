namespace WebExpress.UI.WebControl
{
    public enum TypeDismissibleAlert
    {
        None,
        Dismissible
    }

    public static class TypeDismissibleAlertExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeDismissibleAlert layout)
        {
            return layout switch
            {
                TypeDismissibleAlert.Dismissible => "alert-dismissible",
                _ => string.Empty,
            };
        }
    }
}
