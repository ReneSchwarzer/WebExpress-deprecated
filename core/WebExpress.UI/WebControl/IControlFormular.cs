using System;
using System.Collections.Generic;
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
        event EventHandler<FormularEventArgs> InitializeFormular;

        /// <summary>
        /// Event wird ausgelöst, wenn die Daten des Formulars ermittelt werden müssen
        /// </summary>
        event EventHandler<FormularEventArgs> FillFormular;

        /// <summary>
        /// Event wird ausgelöst, wenn das Formular verarbeitet werden soll
        /// </summary>
        event EventHandler<FormularEventArgs> ProcessFormular;

        /// <summary>
        /// Event wird ausgelöst, wenn das Formular verarbeitet und die nächsten Daten geladen werden sollen
        /// </summary>
        event EventHandler<FormularEventArgs> ProcessAndNextFormular;

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
        /// Liefert oder setzt die Submit-Schaltfläche
        /// </summary>
        ControlFormularItemButton SubmitButton { get; }

        /// <summary>
        /// Liefert oder setzt die Formulareinträge
        /// </summary>
        IList<ControlFormularItem> Items { get; }

        /// <summary>
        /// Initialisiert das Formular
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        void Initialize(RenderContextFormular context);

        /// <summary>
        /// Vorverarbeitung des Formulars
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        void PreProcess(RenderContextFormular context);

        /// <summary>
        /// Fügt Formularsteuerelement dem Formular hinzu
        /// </summary>
        /// <param name="item">Die Formularelemente</param>
        void Add(params ControlFormularItem[] item);

        /// <summary>
        /// Prüft die Eingabeelemente auf Korrektheit der Daten
        /// </summary>
        /// <param name="context">Der Kontext, indem die Eingaben validiert werden</param>
        /// <returns>True wenn alle Formulareinträhe gültig sind, false sonst</returns>
        bool Validate(RenderContextFormular context);
    }
}
