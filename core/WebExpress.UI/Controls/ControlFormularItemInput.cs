using System;
using System.Collections.Generic;
using System.Linq;

namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Kennzeichnet ein Steuerelement, welches von Benutzer auszufüllen ist
    /// </summary>
    public abstract class ControlFormularItemInput : ControlFormularItem, IControlFormularLabel
    {
        /// <summary>
        /// Event zum Validieren der Eingabewerte
        /// </summary>
        public event EventHandler<ValidationEventArgs> Validation;

        /// <summary>
        /// Liefert oder setzt das Icon
        /// </summary>
        public PropertyIcon Icon { get; set; }

        /// <summary>
        /// Liefert oder setzt die Beschriftung
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Liefert oder setzt ein optinalen Hilfetext
        /// </summary>
        public string Help { get; set; }

        /// <summary>
        /// Liefert oder setzt ob die Eingabeelemet deaktiviert ist
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// Liefert oder setzt die Elemente, welche vor dem Steuerelement angezeigt werden
        /// </summary>
        public List<Control> Prepend { get; private set; }

        /// <summary>
        /// Liefert oder setzt, ob das Formaulrelement validiert wurde
        /// </summary>
        private bool IsValidated { get; set; }

        /// <summary>
        /// Liefert oder setzt die Elemente, welche nach dem Steuerelement angezeigt werden
        /// </summary>
        public List<Control> Append { get; private set; }

        /// <summary>
        /// Bestimmt ob die Eingabe gültig sind
        /// </summary>
        public List<ValidationResult> ValidationResults { get; private set; }

        /// <summary>
        /// Ermittelt das schwerwiegenste Validierungsergebnis
        /// </summary>
        /// <returns>Das schwerwiegenste Validierungsergebnis</returns>
        public virtual TypesInputValidity ValidationResult
        {
            get
            {
                var buf = ValidationResults;

                if (buf.Where(x => x.Type == TypesInputValidity.Error).Count() > 0)
                {
                    return TypesInputValidity.Error;
                }
                else if (buf.Where(x => x.Type == TypesInputValidity.Warning).Count() > 0)
                {
                    return TypesInputValidity.Warning;
                }
                else if (buf.Where(x => x.Type == TypesInputValidity.Success).Count() > 0)
                {
                    return TypesInputValidity.Success;
                }

                return IsValidated ? TypesInputValidity.Success : TypesInputValidity.Default;
            }
        }

        /// <summary>
        /// Liefert oder setzt den Wert
        /// </summary>
        public virtual string Value { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularItemInput(string id)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Prepend = new List<Control>();
            Append = new List<Control>();
            ValidationResults = new List<Controls.ValidationResult>();
            IsValidated = false;
        }

        ///// <summary>
        ///// In HTML konvertieren
        ///// </summary>
        ///// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        ///// <returns>Das Control als HTML</returns>
        //public override IHtmlNode Render(RenderContext context)
        //{
        //    //get => context.Page.GetParam(Name);
        //    //set
        //    //{
        //    //    var v = GetParam(Name);

        //    //    if (string.IsNullOrWhiteSpace(v))
        //    //    {
        //    //        AddParam(Name, value, Formular.Scope);
        //    //    }
        //    //}

        //    return base.Render(context);
        //}

        /// <summary>
        /// Löst das Validation-Event aus
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected virtual void OnValidation(ValidationEventArgs e)
        {
            Validation?.Invoke(this, e);
        }

        /// <summary>
        /// Prüft das Eingabeelement auf Korrektheit der Daten
        /// </summary>
        public virtual void Validate()
        {
            IsValidated = true;

            if (!Disabled)
            {
                var args = new ValidationEventArgs() { Value = Value };
                OnValidation(args);

                ValidationResults.AddRange(args.Results);
            }
        }
    }
}
