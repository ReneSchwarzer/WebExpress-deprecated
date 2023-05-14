using System;
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
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
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
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
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
        /// Called when the delete action has been confirmed.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
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
