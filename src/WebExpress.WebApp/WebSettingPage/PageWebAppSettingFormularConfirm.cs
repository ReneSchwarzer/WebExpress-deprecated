using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebControl;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebPage
{
    /// <summary>
    /// A form based page.
    /// </summary>
    public abstract class PageWebAppSettingFormularConfirm<T> : PageWebAppSettingFormular<T> where T : ControlFormularConfirm, new()
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public PageWebAppSettingFormularConfirm()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public PageWebAppSettingFormularConfirm(string id)
            : base(id)
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Form.Confirm += OnConfirmFormular;
        }

        /// <summary>
        /// Triggered when the form is confirmed.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
        protected virtual void OnConfirmFormular(object sender, FormularEventArgs e)
        {
        }

        /// <summary>
        /// Returns the description.
        /// </summary>
        /// <returns>The description.</returns>
        protected string SetDescription()
        {
            return Form.Content.Text;
        }

        /// <summary>
        /// Sets the description.
        /// </summary>
        /// <param name="description">The description.</param>
        protected void SetDescription(string description)
        {
            Form.Content.Text = description;
        }


        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);
        }
    }
}
