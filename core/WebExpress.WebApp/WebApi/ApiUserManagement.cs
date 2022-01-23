using System.Collections.Generic;
using System.Linq;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;
using WebExpress.WebApp.WebUser;
using WebExpress.WebAttribute;
using WebExpress.WebResource;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.WebApp.WebAPI
{
    /// <summary>
    /// REST-API zur Nutzerverwaltiung
    /// </summary>
    [ID("APIUserManagementV1")]
    [Segment("user", "")]
    [Path("/api/v1")]
    [Module("webexpress.webapp")]
    [Optional]
    public sealed class ApiUSerManagement : ResourceApiCrud
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ApiUSerManagement()
        {
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
        /// Verarbeitung des GET-Request
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Eine Aufzählung, welche JsonSerializer serialisiert werden kann.</returns>
        public override IEnumerable<ResourceApiCrudColumn> GetColumns(Request request)
        {
            return new ResourceApiCrudColumn[]
            {
                new ResourceApiCrudColumn(I18N(request, "webexpress.webapp:setting.usermanager.user.login.label"))
                {
                    Render = "return item.Login;",
                    Width = 5
                },
                new ResourceApiCrudColumn(I18N(request, "webexpress.webapp:setting.usermanager.user.name.label"))
                {
                    Render = "return item.Lastname + ', ' + item.Firstname;"
                },
                new ResourceApiCrudColumn(I18N(request, "webexpress.webapp:setting.usermanager.user.groups.label"))
                {
                    Render = "var html = '<ul>' + item.Groups.map(function(group) { return '<li>' + group.Name + '</li>'; }).join('') + '</ul>'; return html;"
                },
                new ResourceApiCrudColumn(I18N(request, "webexpress.webapp:setting.usermanager.user.email.label"))
                {
                    Render = "return item.Email;"
                }
            };
        }

        /// <summary>
        /// Verarbeitung des GET-Request
        /// </summary>
        /// <param name="id">Die ID oder null wenn nicht gefiltert werden soll</param>
        /// <param name="search">Ein Suchstring oder null wenn nicht gefiltert werden soll</param>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Eine Aufzählung, welche JsonSerializer serialisiert werden kann.</returns>
        public override IEnumerable<object> GetData(string id, string search, Request request)
        {
            var users = UserManager.Users as IEnumerable<User>;

            if (id != null)
            {
                users = users.Where(x => x.ID.Equals(id));
            }

            if (search != null)
            {
                users = users.Where
                (
                    x =>
                    x.Login.Contains(search, System.StringComparison.OrdinalIgnoreCase) ||
                    $"{ x.Lastname }, { x.Firstname }".Contains(search, System.StringComparison.OrdinalIgnoreCase) ||
                    x.Email.Contains(search, System.StringComparison.OrdinalIgnoreCase)
                );
            }

            return users.Select(x => (object)new
            {
                ID = x.ID,
                Login = x.Login,
                Firstname = x.Firstname,
                Lastname = x.Lastname,
                Email = x.Email,
                Groups = x.Groups.Select(y => new { ID = y.ID, Name = y.Name })
            });
        }

        /// <summary>
        /// Verarbeitung des DELETE-Request
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Das Ergebnis der Löschung</returns>
        public override bool DeleteData(Request request)
        {
            return true;
        }
    }
}
