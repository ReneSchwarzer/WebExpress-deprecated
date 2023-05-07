using System;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebControl
{
    public class ControlModalFormularConfirm : ControlModalFormular
    {
        /// <summary>
        /// Event wird ausgelöst, wenn das Löschen bestätigt wurde
        /// </summary>
        public event EventHandler<FormularEventArgs> Confirm;

        /// <summary>
        /// Liefert oder setzt das Icon
        /// </summary>
        public PropertyIcon ButtonIcon { get; set; }

        /// <summary>
        /// Liefert oder setzt die Farbe der Schaltfläche
        /// </summary>
        public PropertyColorButton ButtonColor { get; set; }

        /// <summary>
        /// Liefert oder setzt die Beschriftung der Schaltfläche
        /// </summary>
        public string ButtonLabel { get; set; }

        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        public new ControlFormularItem Content { get; set; }

        /// <summary>
        /// Liefert oder setzt die Weiterleitungs-Uri
        /// </summary>
        public string RedirectUri { get { return Formular?.RedirectUri; } set { Formular.RedirectUri = value; } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlModalFormularConfirm(string id = null)
            : this(id, null)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="content">Die Formularsteuerelemente</param>
        public ControlModalFormularConfirm(string id, params ControlFormularItem[] content)
            : base(id, string.Empty, content)
        {
            Init();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void Init()
        {
            Formular.ProcessFormular += (s, e) =>
            {
                OnConfirm(e.Context);
            };
        }

        /// <summary>
        /// Löst das Confirm-Event aus
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        protected virtual void OnConfirm(RenderContextFormular context)
        {
            Confirm?.Invoke(this, new FormularEventArgs() { Context = context });
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            if (string.IsNullOrWhiteSpace(Header))
            {
                Header = context.Page.I18N("webexpress.webapp", "confirm.header");
            }

            if (string.IsNullOrWhiteSpace(ButtonLabel))
            {
                ButtonLabel = context.Page.I18N("webexpress.webapp", "confirm.label");
            }

            if (Content == null)
            {
                Content = new ControlFormularItemStaticText() { Text = context.Page.I18N("webexpress.webapp", "confirm.description") };
            }

            if (ButtonColor == null)
            {
                ButtonColor = new PropertyColorButton(TypeColorButton.Primary);
            }

            Formular.RedirectUri = RedirectUri ?? context.Uri;
            Formular.SubmitButton.Text = ButtonLabel;
            Formular.SubmitButton.Icon = ButtonIcon;
            Formular.SubmitButton.Color = ButtonColor;
            Formular.Items.Clear();
            Formular.Add(Content);

            return base.Render(context);
        }
    }
}
