using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.Uri;

namespace WebExpress.UI.WebControl
{
    public interface IControlFormular : IControl
    {
        /// <summary>
        /// Event zum Validieren der Eingabewerte
        /// </summary>
        event EventHandler<ValidationEventArgs> Validation;

        /// <summary>
        /// Event wird ausgelöst, wenn das Formular initialisiert wurde
        /// </summary>
        event EventHandler InitializeFormular;
        
        /// <summary>
        /// Event wird ausgelöst, wenn die Daten des Formulars ermittelt werden müssen
        /// </summary>
        event EventHandler FillFormular;

        /// <summary>
        /// Event wird ausgelöst, wenn das Formular verarbeitet werden soll
        /// </summary>
        event EventHandler ProcessFormular;

        /// <summary>
        /// Event wird ausgelöst, wenn das Formular verarbeitet und die nächsten Daten geladen werden sollen
        /// </summary>
        event EventHandler ProcessAndNextFormular;

        /// <summary>
        /// Liefert oder setzt den Formularnamen
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Liefert oder setzt die Ziel-Uri
        /// </summary>
        IUri Uri { get; set; }

        /// <summary>
        /// Liefert oder setzt die Weiterleitungs-Url
        /// </summary>
        IUri RedirectUri { get; set; }

        /// <summary>
        /// Liefert oder setzt die Abbruchs-Url
        /// </summary>
        IUri BackUri { get; set; }

        /// <summary>
        /// Liefert oder setzt die Submit-Schaltfläche
        /// </summary>
        ControlFormularItemButton SubmitButton { get; }

        /// <summary>
        /// Liefert oder setzt den Gültigkeitsbereich der Formulardaten
        /// </summary>
        ParameterScope Scope { get; }

        /// <summary>
        /// Liefert oder setzt die Formulareinträge
        /// </summary>
        ICollection<ControlFormularItem> Items { get; }

        /// <summary>
        /// Bestimmt ob die Eingabe gültig sind
        /// </summary>
        bool Valid { get; }

        /// <summary>
        /// Liefert die Validierungsergebnisse
        /// </summary>
        ICollection<ValidationResult> ValidationResults { get; }

        /// <summary>
        /// Initialisiert das Formular
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        void Initialize(RenderContext context);

        /// <summary>
        /// Vorverarbeitung des Formulars
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        void PreProcess(RenderContext context);

        /// <summary>
        /// Fügt Formularsteuerelement dem Formular hinzu
        /// </summary>
        /// <param name="item">Die Formularelemente</param>
        void Add(params ControlFormularItem[] item);

        /// <summary>
        /// Prüft die Eingabeelemente auf Korrektheit der Daten
        /// </summary>
        void Validate();
    }
}
