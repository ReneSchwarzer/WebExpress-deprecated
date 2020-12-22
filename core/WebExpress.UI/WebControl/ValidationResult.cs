namespace WebExpress.UI.WebControl
{
    public class ValidationResult
    {
        /// <summary>
        /// Liefert oder setzt den Typ des Fehlers
        /// </summary>
        public TypesInputValidity Type { get; set; }

        /// <summary>
        /// Liefert oder setzt den Fehlertext
        /// </summary>
        public string Text { get; set; }
    }
}
