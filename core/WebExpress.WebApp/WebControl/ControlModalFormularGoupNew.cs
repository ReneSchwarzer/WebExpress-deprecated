﻿using System;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebUser;

namespace WebExpress.WebApp.WebControl
{
    internal sealed class ControlModalFormularGoupNew : ControlModalFormular
    {
        /// <summary>
        /// Liefert die Beschreibung des Formulars
        /// </summary>
        private ControlFormularItemStaticText Description { get; } = new ControlFormularItemStaticText()
        {
            Text = "webexpress.webapp:setting.usermanager.group.add.description",
            TextColor = new PropertyColorText(TypeColorText.Secondary)
        };

        /// <summary>
        /// Liefert das Steuerlement zur Eingabe des Gruppennamen
        /// </summary>
        private ControlFormularItemInputTextBox GroupName { get; } = new ControlFormularItemInputTextBox()
        {
            Label = "webexpress.webapp:setting.usermanager.group.add.name.label",
            Name = "groupname"
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlModalFormularGoupNew(string id = null)
            : base(id)
        {
            Add(Description);
            Add(GroupName);

            Header = "webexpress.webapp:setting.usermanager.group.add.header";

            GroupName.Validation += OnGroupNameValidation;
            Formular.SubmitButton.Text = "webexpress.webapp:setting.usermanager.group.add.confirm";
            Formular.ProcessFormular += OnConfirm;
        }

        /// <summary>
        /// Wird aufgerufen, wenn der eingegebene Gruppenname überprüft werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnGroupNameValidation(object sender, ValidationEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(GroupName.Value))
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "webexpress.webapp:setting.usermanager.group.add.name.error.empty"));
            }

            if (UserManager.Groups.Where(x => x.Name.Equals(GroupName.Value, StringComparison.OrdinalIgnoreCase)).Any())
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "webexpress.webapp:setting.usermanager.group.add.name.error.duplicate"));
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Löschaktion bestätigt wurde
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnConfirm(object sender, FormularEventArgs e)
        {
            var group = new Group()
            {
                ID = Guid.NewGuid().ToString(),
                Name = GroupName.Value,
                Created = DateTime.Now,
                Updated = DateTime.Now
            };

            UserManager.AddGroup(group);

            e.Context.Page.Redirecting(e.Context.Uri);
        }
    }
}
