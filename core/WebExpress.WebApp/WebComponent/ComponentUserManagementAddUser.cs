﻿using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebControl;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebComponent
{
    [Section(Section.HeadlineSecondary)]
    [Application("webexpress.webapp")]
    [Context("webexpress.webapp.usermanagement.user")]
    public sealed class ComponentUserManagementAddUser : ComponentControlButtonLink
    {
        /// <summary>
        /// Liefert den modalen Dialog zur Bestätigung der Löschaktion
        /// </summary>
        private ControlModalFormularUserNew ModalDlg = new ControlModalFormularUserNew("add_user");

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentUserManagementAddUser()
            : base("add_user")
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IComponentContext context, IPage page)
        {
            base.Initialization(context, page);

            Text = "webexpress.webapp:setting.usermanager.user.add.label";
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
            BackgroundColor = new PropertyColorButton(TypeColorButton.Primary);
            Icon = new PropertyIcon(TypeIcon.Plus);
            Modal = new PropertyModal(TypeModal.Modal, ModalDlg);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            return base.Render(context);
        }
    }
}
