namespace WebExpress.UI.WebControl
{
    public class ValidationResult
    {
        /// <summary>
        /// Returns or sets the type. des Fehlers
        /// </summary>
        public TypesInputValidity Type { get; private set; }

        /// <summary>
        /// Liefert oder setzt den Fehlertext
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">Der Fehlertyp</param>
        /// <param name="text">Der Fehlertext</param>
        public ValidationResult(TypesInputValidity type, string text)
        {
            Type = type;
            Text = text;
        }
    }
}
