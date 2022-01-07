using System;
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
        private ControlFormularItemStaticText Description { get; } = new ControlFormularItemStaticText()
        {
            Text = "webexpress.webapp:setting.usermanager.group.edit.description",
            TextColor = new PropertyColorText(TypeColorText.Secondary)
        };

        /// <summary>
        /// Liefert das Steuerlement zur Eingabe des Gruppennamen
        /// </summary>
        private ControlFormularItemInputTextBox GroupName { get; } = new ControlFormularItemInputTextBox()
        {
            Label = "webexpress.webapp:setting.usermanager.group.edit.name.label",
            Name = "groupname"
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
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
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnFillFormular(object sender, FormularEventArgs e)
        {
            GroupName.Value = Item?.Name;
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
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "webexpress.webapp:setting.usermanager.group.edit.name.error.empty"));
            }

            if (UserManager.Groups.Where(x => x != Item && x.Name.Equals(GroupName.Value, StringComparison.OrdinalIgnoreCase)).Any())
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "webexpress.webapp:setting.usermanager.group.edit.name.error.duplicate"));
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Löschaktion bestätigt wurde
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnConfirm(object sender, FormularEventArgs e)
        {
            Item.Name = GroupName.Value;
           
            UserManager.UpdateGroup(Item);

            e.Context.Page.Redirecting(e.Context.Uri);
        }
    }
}
