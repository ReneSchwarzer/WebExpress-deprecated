using System;
using System.Collections.Generic;
using System.Text;

namespace WebExpress.UI.WebControl
{
    public interface IFormularValidation
    {
        /// <summary>
        /// Prüft das Eingabeelement auf Korrektheit der Daten
        /// </summary>
        void Validate();

        /// <summary>
        /// Bestimmt ob die Eingabe gültig sind
        /// </summary>
        ICollection<ValidationResult> ValidationResults { get;}

        /// <summary>
        /// Ermittelt das schwerwiegenste Validierungsergebnis
        /// </summary>
        TypesInputValidity ValidationResult { get; }
    }
}
