using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebPage;
using WebExpress.WebUri;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebResource;
using WebExpress.WebResponse;

namespace WebExpress.WebApp.WebStatusPage
{
    /// <summary>
    /// A status page.
    /// </summary>
    public abstract class PageStatusWebApp<T> : PageControl<RenderContextWebApp>, IStatusPage where T : Response, new()
    {
        /// <summary>
        /// Returns or sets the status code.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Returns or sets the status title.
        /// </summary>
        public string StatusTitle { get; set; }

        /// <summary>
        /// Returns or sets the status message.
        /// </summary>
        public string StatusMessage { get; set; }

        /// <summary>
        /// Returns or sets the status icon.
        /// </summary>
        public IUri StatusIcon { get; set; }

        /// <summary>
        /// Returns or sets the page header.
        /// </summary>
        public ControlWebAppHeader Header { get; protected set; } = new ControlWebAppHeader("header");

        /// <summary>
        /// Constructor
        /// </summary>
        public PageStatusWebApp()
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var visualTree = context.VisualTree;

            var statusCode = new ControlText()
            {
                Text = StatusCode.ToString(),
                Format = TypeFormatText.H2,
                Margin = new PropertySpacingMargin(PropertySpacing.Space.One),
                Padding = new PropertySpacingPadding(PropertySpacing.Space.Four)
            };

            var title = new ControlText()
            {
                Text = StatusTitle,
                Format = TypeFormatText.H3,
                Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.Three)
            };

            var message = new ControlPanelCard(new ControlText()
            {
                Text = StatusMessage,
                Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.Three)
            })
            {
                BackgroundColor = new PropertyColorBackground(TypeColorBackground.Light)
            };

            var panel = new ControlPanel(title, message) { Margin = new PropertySpacingMargin(PropertySpacing.Space.Three) };
            var flex = new ControlPanelFlexbox(statusCode, panel)
            {
                Layout = TypeLayoutFlexbox.Inline,
                Justify = TypeJustifiedFlexbox.Start,
                Align = TypeAlignFlexbox.Stretch
            };

            if (visualTree is VisualTreeControl visualTreeControl)
            {
                visualTreeControl.Content.Add(Header);
                visualTreeControl.Content.Add(flex);
            }
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The response.</returns>
        public override Response Process(Request request)
        {
            var response = base.Process(request);
            var content = response.Content;

            var newResponse = new T();

            newResponse.Header.ContentDisposition = response.Header.ContentDisposition;
            newResponse.Header.CacheControl = "no-cache";
            newResponse.Header.ContentLanguage = response.Header.ContentLanguage;
            newResponse.Header.ContentType = response.Header.ContentType;
            newResponse.Header.ContentLength = response.Header.ContentLength;
            newResponse.Content = content;

            return newResponse;
        }
    }
}
