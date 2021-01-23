using System.Collections.Generic;

namespace WebExpress.UI.WebControl
{
    public interface IFormularValidation
    {
        /// <summary>
        /// Liefert oder setzt, ob das Formaulrelement validiert wurde
        /// </summary>
        //bool IsValidated { get; }

        /// <summary>
        /// Bestimmt ob die Eingabe gültig sind
        /// </summary>
        ICollection<ValidationResult> ValidationResults { get; }

        /// <summary>
        /// Ermittelt das schwerwiegenste Validierungsergebnis
        /// </summary>
        TypesInputValidity ValidationResult { get; }

        /// <summary>
        /// Prüft das Eingabeelement auf Korrektheit der Daten
        /// </summary>
        void Validate();
    }
}
