﻿using System;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebUser;

namespace WebExpress.WebApp.WebControl
{
    internal sealed class ControlModalFormularGoupEdit : ControlModalFormular
    {
        /// <summary>
        /// Die zu löschende Gruppe
        /// </summary>
        public Group Item { get; set; }

        /// <summary>
        /// Liefert die Beschreibung des Formulars
        /// </summary>
        private ControlFormItemStaticText Description { get; } = new ControlFormItemStaticText()
        {
            Text = "webexpress.webapp:setting.usermanager.group.edit.description",
            TextColor = new PropertyColorText(TypeColorText.Secondary)
        };

        /// <summary>
        /// Liefert das Steuerlement zur Eingabe des Gruppennamen
        /// </summary>
        private ControlFormItemInputTextBox GroupName { get; } = new ControlFormItemInputTextBox()
        {
            Label = "webexpress.webapp:setting.usermanager.group.edit.name.label",
            Name = "groupname"
        };

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlModalFormularGoupEdit(string id = null)
            : base(id)
        {
            Add(Description);
            Add(GroupName);

            Header = "webexpress.webapp:setting.usermanager.group.edit.header";

            GroupName.Validation += OnGroupNameValidation;
            Formular.SubmitButton.Text = "webexpress.webapp:setting.usermanager.group.edit.confirm";
            Formular.FillFormular += OnFillFormular;
            Formular.ProcessFormular += OnConfirm;
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular mit Initialwerten gefüllt werden soll
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void OnFillFormular(object sender, FormularEventArgs e)
        {
            GroupName.Value = Item?.Name;
        }

        /// <summary>
        /// Wird aufgerufen, wenn der eingegebene Gruppenname überprüft werden soll
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void OnGroupNameValidation(object sender, ValidationEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(GroupName.Value))
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "webexpress.webapp:setting.usermanager.group.edit.name.error.empty"));
            }

            if (UserManager.Groups.Where(x => x != Item && x.Name.Equals(GroupName.Value, StringComparison.OrdinalIgnoreCase)).Any())
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "webexpress.webapp:setting.usermanager.group.edit.name.error.duplicate"));
            }
        }

        /// <summary>
        /// Called when the delete action has been confirmed.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void OnConfirm(object sender, FormularEventArgs e)
        {
            Item.Name = GroupName.Value;

            UserManager.UpdateGroup(Item);

            e.Context.Page.Redirecting(e.Context.Uri);
        }
    }
}
