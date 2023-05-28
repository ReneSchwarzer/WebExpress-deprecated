using System;
using System.Collections.Generic;

namespace WebExpress.UI.WebControl
{
    public interface IControlForm : IControl
    {
        /// <summary>
        /// Event to validate the input values.
        /// </summary>
        event EventHandler<ValidationEventArgs> Validation;

        /// <summary>
        /// Event is raised when the form has been initialized.
        /// </summary>
        event EventHandler<FormularEventArgs> InitializeFormular;

        /// <summary>
        /// Event is raised when the form's data needs to be determined.
        /// </summary>
        event EventHandler<FormularEventArgs> FillFormular;

        /// <summary>
        /// Event is raised when the form is about to be processed.
        /// </summary>
        event EventHandler<FormularEventArgs> ProcessFormular;

        /// <summary>
        /// Event is raised when the form is to be processed and the next data is to be loaded.
        /// </summary>
        event EventHandler<FormularEventArgs> ProcessAndNextFormular;

        /// <summary>
        /// Returns or sets the name of the form.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Returns or sets the target uri.
        /// </summary>
        string Uri { get; set; }

        /// <summary>
        /// Returns or sets the redirect uri.
        /// </summary>
        string RedirectUri { get; set; }

        /// <summary>
        /// Returns or sets the submit button.
        /// </summary>
        ControlFormItemButton SubmitButton { get; }

        /// <summary>
        /// Returns or sets the form items.
        /// </summary>
        IList<ControlFormItem> Items { get; }

        /// <summary>
        /// Initializes the form.
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
        void Add(params ControlFormItem[] item);

        /// <summary>
        /// Prüft die Eingabeelemente auf Korrektheit der Daten
        /// </summary>
        /// <param name="context">The context in which the inputs are validated.</param>
        /// <returns>True if all form items are valid, false otherwise.</returns>
        bool Validate(RenderContextFormular context);
    }
}
