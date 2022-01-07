using System.IO;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebAttribute;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebUser;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebSettingPage
{
    /// <summary>
    /// Einstellungsseiteder der Gruppen
    /// </summary>
    [ID("SettingGroup")]
    [Title("webexpress.webapp:setting.usermanager.group.label")]
    [Segment("group", "webexpress.webapp:setting.usermanager.group.label")]
    [Path("/Setting")]
    [SettingSection(SettingSection.Primary)]
    [SettingIcon(TypeIcon.Users)]
    [SettingGroup("webexpress.webapp:setting.usermanager.group.usermanagement.label")]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Module("webexpress.webapp")]
    [Context("admin")]
    [Context("webexpress.webapp.usermannager.group")]
    [Optional]
    public sealed class PageWebAppSettingUserManagementGroup : PageWebAppSetting
    {
        /// <summary>
        /// Liefert die Bezeichnung.
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
        /// Konstruktor
        /// </summary>
        public PageWebAppSettingUserManagementGroup()
        {
            Icon = new PropertyIcon(TypeIcon.Users);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Vorverarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
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
                            Modal = new ControlModalFormularGoupEdit(group.ID) { Item = group }
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
                            Modal = new ControlModalFormularGroupDelete(group.ID) { Item = group }
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

