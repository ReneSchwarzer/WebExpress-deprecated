using System;
using System.Collections.Generic;

namespace WebExpress.UI.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidationEventArgs : EventArgs
    {
        /// <summary>
        /// Liefert oder setzt sen zu überprüfenden Wert
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Liefert oder setzt die Validierungsnachrichten
        /// </summary>
        public List<ValidationResult> Results { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ValidationEventArgs()
        {
            Results = new List<ValidationResult>();
        }
    }
}
