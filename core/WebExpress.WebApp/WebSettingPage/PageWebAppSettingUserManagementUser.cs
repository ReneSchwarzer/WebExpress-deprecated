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
    /// Einstellungsseiteder der Nutzer
    /// </summary>
    [ID("SettingUser")]
    [Title("webexpress.webapp:setting.usermanager.user.label")]
    [Segment("user", "webexpress.webapp:setting.usermanager.user.label")]
    [Path("/Setting")]
    [SettingSection(SettingSection.Primary)]
    [SettingIcon(TypeIcon.User)]
    [SettingGroup("webexpress.webapp:setting.usermanager.group.usermanagement.label")]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Module("webexpress.webapp")]
    [Context("admin")]
    [Context("webexpress.webapp.usermannager.user")]
    [Optional]
    public sealed class PageWebAppSettingUserManagementUser : PageWebAppSetting
    {
        /// <summary>
        /// Liefert die Bezeichnung.
        /// </summary>
        private ControlText Label { get; } = new ControlText()
        {
            Text = "webexpress.webapp:setting.usermanager.user.help",
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two),
            TextColor = new PropertyColorText(TypeColorText.Info)
        };

        /// <summary>
        /// Liefert den Hilfetext.
        /// </summary>
        private ControlText Description { get; } = new ControlText()
        {
            Text = "webexpress.webapp:setting.usermanager.user.description",
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two)
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageWebAppSettingUserManagementUser()
        {
            Icon = new PropertyIcon(TypeIcon.User);
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

            var plugins = new ControlTable() { Striped = false };
            plugins.AddColumn("webexpress.webapp:setting.usermanager.user.login.label");
            plugins.AddColumn("webexpress.webapp:setting.usermanager.user.name.label");
            plugins.AddColumn("webexpress.webapp:setting.usermanager.user.groups.label");

            foreach (var user in UserManager.Users.OrderBy(x => x.Login))
            {
                plugins.AddRow
                (
                    new ControlText()
                    {
                        Text = user.Login
                    },
                    new ControlText()
                    {
                        Text = $"{user.Lastname}, {user.Firstname}"
                    },
                    new ControlList(user.Groups.Select(x => new ControlListItem(new ControlText() { Text = x.Name })).ToArray())
                    {
                        
                    },
                    new ControlPanelFlexbox
                    (
                        new ControlLink()
                        {
                            Text = "webexpress.webapp:setting.usermanager.group.edit.label",
                            Uri = new UriFragment(),
                            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.Null),
                            Modal = new ControlModalFormularUserEdit(user.ID) { Item = user }
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
                            Modal = new ControlModalFormularUserDelete(user.ID) { Item = user }
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
            context.VisualTree.Content.Primary.Add(plugins);
        }
    }
}

