using System.Collections.Generic;
using WebExpress.Application;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebResource;
using WebExpress.Uri;
using WebExpress.WebApp.WebControl;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebResource.PageStatus
{
    /// <summary>
    /// Statusseite
    /// </summary>
    public abstract class PageStatusTemplateWebApp : ResourcePageTemplate, IPageStatus
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
        public override void Initialization()
        {
            base.Initialization();

            var application = ApplicationManager.GetApplcation(Context?.ApplicationID);

            Header.Fixed = TypeFixed.Top;
            Header.Styles = new List<string>(new[] { "position: sticky; top: 0; z-index: 99;" });

            Header.Logo = application?.Icon;
            Header.Title = this.I18N(application?.ApplicationName);
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

        }

        /// <summary>
        /// In HTML konvertieren+
        /// </summary>
        /// <returns>Die Seite als HTML-Baum</returns>
        public override IHtmlNode Render()
        {
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
            
            Content.Add(Header);
            Content.Add(flex);

            return base.Render();
        }
    }
}
