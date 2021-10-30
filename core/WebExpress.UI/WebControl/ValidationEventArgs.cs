using System.Collections.Generic;

namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Eventargument zum Validieren von Formulareingaben
    /// </summary>
    public class ValidationEventArgs : FormularEventArgs
    {
        /// <summary>
        /// Liefert oder setzt sen zu überprüfenden Wert
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Liefert oder setzt die Validierungsnachrichten
        /// </summary>
        public List<ValidationResult> Results { get; } = new List<ValidationResult>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ValidationEventArgs()
        {
        }
    }
}
