using System.Collections.Generic;
using WebExpress.Application;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebPage;
using WebExpress.Uri;
using WebExpress.WebApp.WebControl;
using WebExpress.WebPage;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebPage.PageStatus
{
    /// <summary>
    /// Statusseite
    /// </summary>
    public abstract class PageStatusTemplateWebApp<T> : PageControl<VisualTreeWebApp>, IPageStatus where T : Response, new()
    {
        /// <summary>
        /// Liefert oder setzt den Statuscode
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Liefert oder setzt die Stausnachricht
        /// </summary>
        public string StatusTitle { get; set; }

        /// <summary>
        /// Liefert oder setzt die Stausnachricht
        /// </summary>
        public string StatusMessage { get; set; }

        /// <summary>
        /// Liefert oder setzt das Statusicon
        /// </summary>
        public IUri StatusIcon { get; set; }

        /// <summary>
        /// Liefert oder setzt den Kopf
        /// </summary>
        public ControlWebAppHeader Header { get; protected set; } = new ControlWebAppHeader("header");

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageStatusTemplateWebApp()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            var application = ApplicationManager.GetApplcation(Context?.ApplicationID);

            Header.Fixed = TypeFixed.Top;
            Header.Styles = new List<string>(new[] { "position: sticky; top: 0; z-index: 99;" });

            Header.Logo = application?.Icon;
            Header.Title = this.I18N(application.PluginID, application?.ApplicationName);
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContext context)
        {
            base.Process(context);

            var visualTree = context.GetVisualTree<VisualTreeWebApp>();

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
        /// Verarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Antwort</returns>
        public override Response Process(Request request)
        {
            var response = base.Process(request);
            var content = response.Content;

            var newResponse = new T();

            newResponse.HeaderFields.ContentDisposition = response.HeaderFields.ContentDisposition;
            newResponse.HeaderFields.CacheControl = "no-cache";
            newResponse.HeaderFields.ContentLanguage = response.HeaderFields.ContentLanguage;
            newResponse.HeaderFields.ContentType = response.HeaderFields.ContentType;
            newResponse.HeaderFields.ContentLength = response.HeaderFields.ContentLength;
            newResponse.Content = content;

            return newResponse;
        }
    }
}
