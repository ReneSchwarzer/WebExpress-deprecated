﻿using System;
using WebExpress.WebHtml;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebUser;
using WebExpress.WebPage;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.WebApp.WebControl
{
    internal sealed class ControlModalFormularUserDelete : ControlModalFormularConfirmDelete
    {
        /// <summary>
        /// Der zu löschende Benutzer
        /// </summary>
        public User Item { get; set; }

        /// <summary>
        /// Liefert die Beschreibung
        /// </summary>
        private ControlFormItemStaticText Description { get; } = new ControlFormItemStaticText();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlModalFormularUserDelete(string id = null)
            : base("delete_" + id)
        {
            Confirm += OnConfirm;

            Header = "webexpress.webapp:setting.usermanager.user.delete.header";
            Content = Description;
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Löschen bestätigt wurde
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        /// <exception cref="NotImplementedException"></exception>
        private void OnConfirm(object sender, FormularEventArgs e)
        {
            UserManager.RemoveUser(Item);

            e.Context.Page.Redirecting(e.Context.Uri);
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Description.Text = string.Format(I18N(context.Culture, "webexpress.webapp:setting.usermanager.user.delete.description"), Item.Login);

            return base.Render(context);
        }
    }
}