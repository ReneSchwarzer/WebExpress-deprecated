namespace WebExpress.UI.WebControl
{
    public class ValidationResult
    {
        /// <summary>
        /// Liefert oder setzt den Typ des Fehlers
        /// </summary>
        public TypesInputValidity Type { get; private set; }

        /// <summary>
        /// Liefert oder setzt den Fehlertext
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Konstruktor
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
