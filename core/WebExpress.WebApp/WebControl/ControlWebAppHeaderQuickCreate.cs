using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebControl
{
    /// <summary>
    /// Header für eine WebApp
    /// </summary>
    public class ControlWebAppHeaderQuickCreate : Control
    {
        /// <summary>
        /// Liefert oder setzt den den Bereich für die App-Navigation
        /// </summary>
        public List<IControlSplitButtonItem> Preferences { get; protected set; } = new List<IControlSplitButtonItem>();

        /// <summary>
        /// Liefert oder setzt den den Bereich für die App-Navigation
        /// </summary>
        public List<IControlSplitButtonItem> Primary { get; protected set; } = new List<IControlSplitButtonItem>();

        /// <summary>
        /// Liefert oder setzt den den Bereich für die App-Navigation
        /// </summary>
        public List<IControlSplitButtonItem> Secondary { get; protected set; } = new List<IControlSplitButtonItem>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlWebAppHeaderQuickCreate(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Padding = new PropertySpacingPadding(PropertySpacing.Space.Null);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var quickcreateList = new List<IControlSplitButtonItem>(Preferences);
            quickcreateList.AddRange(Primary);
            quickcreateList.AddRange(Secondary);

            var firstQuickcreate = (quickcreateList.FirstOrDefault() as ControlLink);
            firstQuickcreate?.Render(context);

            var quickcreate = (quickcreateList.Count > 1) ?
            (IControl)new ControlSplitButtonLink(ID, quickcreateList.Skip(1))
            {
                Text = context.I18N("webexpress.webapp", "header.quickcreate.label"),
                Uri = firstQuickcreate?.Uri,
                BackgroundColor = LayoutSchema.HeaderQuickCreateButtonBackground,
                Size = LayoutSchema.HeaderQuickCreateButtonSize,
                Margin = new PropertySpacingMargin(PropertySpacing.Space.Auto, PropertySpacing.Space.None),
                OnClick = firstQuickcreate?.OnClick,
                Modal = firstQuickcreate?.Modal
            } :
            (Preferences.Count > 0) ?
            new ControlButtonLink(ID)
            {
                Text = context.I18N("webexpress.webapp", "header.quickcreate.label"),
                Uri = firstQuickcreate?.Uri,
                BackgroundColor = LayoutSchema.HeaderQuickCreateButtonBackground,
                Size = LayoutSchema.HeaderQuickCreateButtonSize,
                Margin = new PropertySpacingMargin(PropertySpacing.Space.Auto, PropertySpacing.Space.None),
                OnClick = firstQuickcreate?.OnClick,
                Modal = firstQuickcreate?.Modal
            } :
            null;

            return quickcreate?.Render(context);
        }
    }
}
