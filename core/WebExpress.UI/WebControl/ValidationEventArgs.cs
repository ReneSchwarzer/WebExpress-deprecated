using System;
using System.Collections.Generic;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
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
        public List<ValidationResult> Results { get; } = new List<ValidationResult>();

        /// <summary>
        /// Der Kontext, indem die Validierung stattfindet
        /// </summary>
        public RenderContext Context { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ValidationEventArgs()
        {
        }
    }
}
