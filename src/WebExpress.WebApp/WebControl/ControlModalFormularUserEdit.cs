using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebUser;

namespace WebExpress.WebApp.WebControl
{
    internal sealed class ControlModalFormularUserEdit : ControlModalFormular
    {
        /// <summary>
        /// Der zu bearbeitende Benutzer
        /// </summary>
        public User Item { get; set; }

        /// <summary>
        /// Liefert die Beschreibung des Formulars
        /// </summary>
        private ControlFormItemStaticText Description { get; } = new ControlFormItemStaticText()
        {
            Text = "webexpress.webapp:setting.usermanager.user.edit.description",
            TextColor = new PropertyColorText(TypeColorText.Secondary)
        };

        /// <summary>
        /// Liefert das Steuerlement zur Eingabe der Loginkennung
        /// </summary>
        private ControlFormItemInputTextBox Login { get; } = new ControlFormItemInputTextBox()
        {
            Label = "webexpress.webapp:setting.usermanager.user.edit.login.label",
            Name = "login"
        };

        /// <summary>
        /// Liefert das Steuerlement zur Eingabe des Vornamens
        /// </summary>
        private ControlFormItemInputTextBox Firstname { get; } = new ControlFormItemInputTextBox()
        {
            Label = "webexpress.webapp:setting.usermanager.user.edit.firstname.label",
            Name = "firstname"
        };

        /// <summary>
        /// Liefert das Steuerlement zur Eingabe des Nachnamens
        /// </summary>
        private ControlFormItemInputTextBox Lastname { get; } = new ControlFormItemInputTextBox()
        {
            Label = "webexpress.webapp:setting.usermanager.user.edit.lastname.label",
            Name = "lastname"
        };

        /// <summary>
        /// Liefert das Steuerlement zur Eingabe der Email-Adresse
        /// </summary>
        private ControlFormItemInputTextBox Email { get; } = new ControlFormItemInputTextBox()
        {
            Label = "webexpress.webapp:setting.usermanager.user.edit.email.label",
            Name = "email",
            Placeholder = "user@example.com"
        };

        /// <summary>
        /// Liefert die Gruppen
        /// </summary>
        private ControlFormItemInputMove Groups { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        public ControlModalFormularUserEdit(string id = null)
            : base(id)
        {
            Groups = new ControlFormItemInputMove(id)
            {
                Name = "groups",
                Label = "webexpress.webapp:setting.usermanager.user.edit.groups.label",
                Help = "webexpress.webapp:setting.usermanager.user.edit.groups.description",
                SelectedHeader = "webexpress.webapp:setting.usermanager.user.edit.groups.selected",
                AvailableHeader = "webexpress.webapp:setting.usermanager.user.edit.groups.available",
                Icon = new PropertyIcon(TypeIcon.Users)
            };

            Login.Validation += OnLoginValidation;
            Firstname.Validation += OnFirstnameValidation;
            Lastname.Validation += OnLastnameValidation;
            Email.Validation += OnEmailValidation;

            Add(Description);
            Add(Login);
            Add(new ControlFormItemGroupColumnVertical(Firstname, Lastname) { Distribution = new int[] { 50 } });
            Add(Email);
            Add(Groups);
            Formular.SubmitButton.Text = "webexpress.webapp:setting.usermanager.user.edit.confirm";
            Formular.FillFormular += OnFillFormular;
            Formular.ProcessFormular += OnConfirm;

            Header = "webexpress.webapp:setting.usermanager.user.edit.header";
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular mit Initialwerten gefüllt werden soll
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void OnFillFormular(object sender, FormularEventArgs e)
        {
            Login.Value = Item?.Login;
            Firstname.Value = Item?.Firstname;
            Lastname.Value = Item?.Lastname;
            Email.Value = Item?.Email;

            foreach (var v in UserManager.Groups.OrderBy(x => x.Name))
            {
                Groups.Options.Add(new ControlFormItemInputSelectionItem()
                {
                    Id = v.Id,
                    Label = v.Name
                });
            }

            Groups.Value = string.Join(";", Item.GroupIds);
        }

        /// <summary>
        /// Wird aufgerufen, wenn die eingegebene Loginkennung überprüft werden soll
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void OnLoginValidation(object sender, ValidationEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Login.Value))
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "webexpress.webapp:setting.usermanager.user.edit.login.error.empty"));
            }

            if (UserManager.Users.Where(x => x != Item && x.Login.Equals(Login.Value, StringComparison.OrdinalIgnoreCase)).Any())
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "webexpress.webapp:setting.usermanager.user.edit.login.error.duplicate"));
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn der eingegebene Vorname überprüft werden soll
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void OnFirstnameValidation(object sender, ValidationEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Firstname.Value))
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "webexpress.webapp:setting.usermanager.user.edit.firstname.error.empty"));
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn der eingegebene Nachname überprüft werden soll
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void OnLastnameValidation(object sender, ValidationEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Lastname.Value))
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "webexpress.webapp:setting.usermanager.user.edit.lastname.error.empty"));
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn die eingegebene E-Mail-Adresse überprüft werden soll
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void OnEmailValidation(object sender, ValidationEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Email.Value))
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "webexpress.webapp:setting.usermanager.user.edit.email.error.empty"));
            }
        }

        /// <summary>
        /// Called when the delete action has been confirmed.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void OnConfirm(object sender, FormularEventArgs e)
        {
            Item.Login = Login.Value;
            Item.Firstname = Firstname.Value;
            Item.Lastname = Lastname.Value;
            Item.Email = Email.Value;

            var groups = Groups.Value?.Split(";", StringSplitOptions.RemoveEmptyEntries);
            Item.GroupIds = new List<string>();
            Item.GroupIds.AddRange(groups);

            UserManager.UpdateUser(Item);

            e.Context.Page.Redirecting(e.Context.Uri);
        }
    }
}
