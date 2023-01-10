using System;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebUser;


namespace WebExpress.WebApp.WebControl
{
    internal sealed class ControlModalFormularUserNew : ControlModalFormular
    {
        /// <summary>
        /// Liefert die Beschreibung des Formulars
        /// </summary>
        private ControlFormularItemStaticText Description { get; } = new ControlFormularItemStaticText()
        {
            Text = "webexpress.webapp:setting.usermanager.user.add.description",
            TextColor = new PropertyColorText(TypeColorText.Secondary)
        };

        /// <summary>
        /// Liefert das Steuerlement zur Eingabe der Loginkennung
        /// </summary>
        private ControlFormularItemInputTextBox Login { get; } = new ControlFormularItemInputTextBox()
        {
            Label = "webexpress.webapp:setting.usermanager.user.add.login.label",
            Name = "login"
        };

        /// <summary>
        /// Liefert das Steuerlement zur Eingabe des Vornamens
        /// </summary>
        private ControlFormularItemInputTextBox Firstname { get; } = new ControlFormularItemInputTextBox()
        {
            Label = "webexpress.webapp:setting.usermanager.user.add.firstname.label",
            Name = "firstname"
        };

        /// <summary>
        /// Liefert das Steuerlement zur Eingabe des Nachnamens
        /// </summary>
        private ControlFormularItemInputTextBox Lastname { get; } = new ControlFormularItemInputTextBox()
        {
            Label = "webexpress.webapp:setting.usermanager.user.add.lastname.label",
            Name = "lastname"
        };

        /// <summary>
        /// Liefert das Steuerlement zur Eingabe der Email-Adresse
        /// </summary>
        private ControlFormularItemInputTextBox Email { get; } = new ControlFormularItemInputTextBox()
        {
            Label = "webexpress.webapp:setting.usermanager.user.add.email.label",
            Name = "email",
            Placeholder = "user@example.com"
        };

        /// <summary>
        /// Liefert die Gruppen
        /// </summary>
        private ControlFormularItemInputMove Groups { get; } = new ControlFormularItemInputMove("group")
        {
            Name = "groups",
            Label = "webexpress.webapp:setting.usermanager.user.edit.groups.label",
            Help = "webexpress.webapp:setting.usermanager.user.edit.groups.description",
            SelectedHeader = "webexpress.webapp:setting.usermanager.user.edit.groups.selected",
            AvailableHeader = "webexpress.webapp:setting.usermanager.user.edit.groups.available",
            Icon = new PropertyIcon(TypeIcon.Users)
        };

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlModalFormularUserNew(string id = null)
            : base(id)
        {
            Login.Validation += OnLoginValidation;
            Firstname.Validation += OnFirstnameValidation;
            Lastname.Validation += OnLastnameValidation;
            Email.Validation += OnEmailValidation;

            Add(Description);
            Add(Login);
            Add(new ControlFormularItemGroupColumnVertical(Firstname, Lastname) { Distribution = new int[] { 50 } });
            Add(Email);
            Add(Groups);
            Formular.SubmitButton.Text = "webexpress.webapp:setting.usermanager.user.add.confirm";
            Formular.FillFormular += OnFillFormular;
            Formular.ProcessFormular += OnConfirm;

            Header = "webexpress.webapp:setting.usermanager.user.add.header";
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular mit Initialwerten gefüllt werden soll
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Das Eventargument</param>
        private void OnFillFormular(object sender, FormularEventArgs e)
        {
            foreach (var v in UserManager.Groups.OrderBy(x => x.Name))
            {
                Groups.Options.Add(new ControlFormularItemInputSelectionItem()
                {
                    ID = v.ID,
                    Label = v.Name
                });
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn die eingegebene Loginkennung überprüft werden soll
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Das Eventargument</param>
        private void OnLoginValidation(object sender, ValidationEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Login.Value))
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "webexpress.webapp:setting.usermanager.user.add.login.error.empty"));
            }

            if (UserManager.Users.Where(x => x.Login.Equals(Login.Value, StringComparison.OrdinalIgnoreCase)).Any())
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "webexpress.webapp:setting.usermanager.user.add.login.error.duplicate"));
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn der eingegebene Vorname überprüft werden soll
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Das Eventargument</param>
        private void OnFirstnameValidation(object sender, ValidationEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Firstname.Value))
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "webexpress.webapp:setting.usermanager.user.add.firstname.error.empty"));
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn der eingegebene Nachname überprüft werden soll
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Das Eventargument</param>
        private void OnLastnameValidation(object sender, ValidationEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Lastname.Value))
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "webexpress.webapp:setting.usermanager.user.add.lastname.error.empty"));
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn die eingegebene E-Mail-Adresse überprüft werden soll
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Das Eventargument</param>
        private void OnEmailValidation(object sender, ValidationEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Email.Value))
            {
                e.Results.Add(new ValidationResult(TypesInputValidity.Error, "webexpress.webapp:setting.usermanager.user.add.email.error.empty"));
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Löschaktion bestätigt wurde
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Das Eventargument</param>
        private void OnConfirm(object sender, FormularEventArgs e)
        {
            var user = new User()
            {
                ID = Guid.NewGuid().ToString(),
                Login = Login.Value,
                Firstname = Firstname.Value,
                Lastname = Lastname.Value,
                Email = Email.Value,
                Created = DateTime.Now,
                Updated = DateTime.Now
            };

            UserManager.AddUser(user);

            e.Context.Page.Redirecting(e.Context.Uri);
        }
    }
}
