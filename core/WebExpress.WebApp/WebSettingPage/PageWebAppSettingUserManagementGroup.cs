using System.Linq;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebUser;
using WebExpress.WebAttribute;
using WebExpress.WebResource;
using WebExpress.WebUri;

namespace WebExpress.WebApp.WebSettingPage
{
    /// <summary>
    /// Einstellungsseiteder der Gruppen
    /// </summary>
    [WebExID("SettingGroup")]
    [WebExTitle("webexpress.webapp:setting.usermanager.group.label")]
    [WebExSegment("group", "webexpress.webapp:setting.usermanager.group.label")]
    [WebExContextPath("/Setting")]
    [WebExSettingSection(WebExSettingSection.Primary)]
    [WebExSettingIcon(TypeIcon.Users)]
    [WebExSettingGroup("webexpress.webapp:setting.usermanager.group.usermanagement.label")]
    [WebExSettingContext("webexpress.webapp:setting.tab.general.label")]
    [WebExModule("webexpress.webapp")]
    [WebExContext("admin")]
    [WebExContext("webexpress.webapp.usermanagement.group")]
    [WebExOptional]
    public sealed class PageWebAppSettingUserManagementGroup : PageWebAppSetting
    {
        /// <summary>
        /// Returns the label.
        /// </summary>
        private ControlText Label { get; } = new ControlText()
        {
            Text = "webexpress.webapp:setting.usermanager.group.help",
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two),
            TextColor = new PropertyColorText(TypeColorText.Info)
        };

        /// <summary>
        /// Liefert den Hilfetext.
        /// </summary>
        private ControlText Description { get; } = new ControlText()
        {
            Text = "webexpress.webapp:setting.usermanager.group.description",
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two)
        };

        /// <summary>
        /// Constructor
        /// </summary>
        public PageWebAppSettingUserManagementGroup()
        {
            Icon = new PropertyIcon(TypeIcon.Users);
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
        /// Vorverarbeitung
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var table = new ControlTable() { Striped = false };
            table.AddColumn("webexpress.webapp:setting.usermanager.group.name.label");
            table.AddColumn("");

            foreach (var group in UserManager.Groups.OrderBy(x => x.Name))
            {
                table.AddRow
                (
                    new ControlText()
                    {
                        Text = group.Name
                    },
                    new ControlPanelFlexbox
                    (
                        new ControlLink()
                        {
                            Text = "webexpress.webapp:setting.usermanager.group.edit.label",
                            Uri = new UriFragment(),
                            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.Null),
                            Modal = new PropertyModal(TypeModal.Modal, new ControlModalFormularGoupEdit(group.ID) { Item = group })
                        },
                        new ControlText()
                        {
                            Text = "|",
                            TextColor = new PropertyColorText(TypeColorText.Muted)
                        },
                        new ControlLink()
                        {
                            Text = "webexpress.webapp:setting.usermanager.group.delete.label",
                            TextColor = new PropertyColorText(TypeColorText.Danger),
                            Uri = new UriFragment(),
                            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.Null),
                            Modal = new PropertyModal(TypeModal.Modal, new ControlModalFormularGroupDelete(group.ID) { Item = group })
                        }
                    )
                    {
                        Align = TypeAlignFlexbox.Center,
                        Layout = TypeLayoutFlexbox.Default,
                        Justify = TypeJustifiedFlexbox.End
                    }
                );
            }

            context.VisualTree.Content.Primary.Add(Description);
            context.VisualTree.Content.Primary.Add(Label);
            context.VisualTree.Content.Primary.Add(table);
        }
    }
}

