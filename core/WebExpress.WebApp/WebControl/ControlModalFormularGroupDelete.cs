﻿using System;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebUser;
using WebExpress.WebPage;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.WebApp.WebControl
{
    internal sealed class ControlModalFormularGroupDelete : ControlModalFormConfirmDelete
    {
        /// <summary>
        /// Die zu bearbeitende Gruppe
        /// </summary>
        public Group Item { get; set; }

        /// <summary>
        /// Liefert die Beschreibung
        /// </summary>
        private ControlFormularItemStaticText Description { get; } = new ControlFormularItemStaticText();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlModalFormularGroupDelete(string id = null)
            : base("delete_" + id)
        {
            Confirm += OnConfirm;
            
            Header = "webexpress.webapp:setting.usermanager.group.delete.header";
            Content = Description;
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Löschen bestätigt wurde
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        /// <exception cref="NotImplementedException"></exception>
        private void OnConfirm(object sender, FormularEventArgs e)
        {
            UserManager.RemoveGroup(Item); 

            e.Context.Page.Redirecting(e.Context.Uri);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Description.Text = string.Format(I18N(context.Culture, "webexpress.webapp:setting.usermanager.group.delete.description"), Item.Name);

            return base.Render(context);
        }
    }
}