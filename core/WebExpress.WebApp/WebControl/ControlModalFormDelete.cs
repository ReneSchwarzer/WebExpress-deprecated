using System;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;

namespace WebExpress.WebApp.WebControl
{
    public class ControlModalFormDelete : ControlModalForm
    {
        /// <summary>
        /// Event wird ausgelöst, wenn das Löschen bestätigt wurde
        /// </summary>
        public event EventHandler Delete;

        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        public new ControlFormularItem Content { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlModalFormDelete(string id = null)
            : this(id, null)
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="content">Die Formularsteuerelemente</param>
        public ControlModalFormDelete(string id, params ControlFormularItem[] content)
            : base(id, string.Empty, content)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Formular.ProcessFormular += (s, e) =>
            {
                OnDelete();
            };
        }

        /// <summary>
        /// Löst das Delete-Event aus
        /// </summary>
        protected virtual void OnDelete()
        {
            Delete?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            if (string.IsNullOrWhiteSpace(Header))
            {
                Header = context.Page.I18N("webexpress.webapp", "delete.header");
            }

            if (Content == null)
            {
                Content = new ControlFormularItemStaticText() { Text = context.Page.I18N("webexpress.webapp", "delete.description") };
            }

            Formular.RedirectUri = context.Uri;
            Formular.SubmitButton.Text = context.Page.I18N("webexpress.webapp", "delete.label");
            Formular.SubmitButton.Icon = new PropertyIcon(TypeIcon.TrashAlt);
            Formular.SubmitButton.Color = new PropertyColorButton(TypeColorButton.Danger);
            Formular.Add(Content);
            
            return base.Render(context);
        }
    }
}
