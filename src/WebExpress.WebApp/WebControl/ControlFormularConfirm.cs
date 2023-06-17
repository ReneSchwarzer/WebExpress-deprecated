using System;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebControl
{
    public class ControlFormularConfirm : ControlForm
    {
        /// <summary>
        /// Event is triggered when deletion is confirmed.
        /// </summary>
        public event EventHandler<FormularEventArgs> Confirm;

        /// <summary>
        /// Returns or sets the icon.
        /// </summary>
        public PropertyIcon ButtonIcon { get => SubmitButton.Icon; set => SubmitButton.Icon = value; }

        /// <summary>
        /// Returns or sets the color. der Schaltfläche
        /// </summary>
        public PropertyColorButton ButtonColor { get => SubmitButton.Color; set => SubmitButton.Color = value; }

        /// <summary>
        /// Returns or sets the label. der Schaltfläche
        /// </summary>
        public string ButtonLabel { get => SubmitButton.Text; set => SubmitButton.Text = value; }

        /// <summary>
        /// Returns or sets the content.
        /// </summary>
        public ControlFormItemStaticText Content { get; } = new ControlFormItemStaticText()
        {
        };

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlFormularConfirm(string id = null)
            : base(id, null)
        {
            ButtonColor = new PropertyColorButton(TypeColorButton.Primary);
        }

        /// <summary>
        /// Triggers the confirm event.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        protected virtual void OnConfirm(RenderContextFormular context)
        {
            Confirm?.Invoke(this, new FormularEventArgs() { Context = context });
        }

        /// <summary>
        /// Initializes the form.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        public override void Initialize(RenderContextFormular context)
        {
            base.Initialize(context);

            ButtonLabel = context.Page.I18N("webexpress.webapp", "confirm.label");
            Content.Text = context.Page.I18N("webexpress.webapp", "confirm.description");
        }

        /// <summary>
        /// Triggers the crocess event.
        /// </summary>
        /// <param name="context"></param>
        protected override void OnProcess(RenderContextFormular context)
        {
            base.OnProcess(context);

            OnConfirm(context);
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Items.Clear();
            Items.Add(Content);

            return base.Render(context);
        }
    }
}
