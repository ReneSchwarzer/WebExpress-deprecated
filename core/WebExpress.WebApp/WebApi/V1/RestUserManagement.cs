﻿using System.Collections.Generic;
using System.Linq;
using WebExpress.Message;
using WebExpress.WebApp.Model;
using WebExpress.WebApp.WebResource;
using WebExpress.WebApp.WebUser;
using WebExpress.WebApp.Wql;
using WebExpress.WebAttribute;
using WebExpress.WebResource;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.WebApp.WebAPI.V1
{
    /// <summary>
    /// REST-API zur Nutzerverwaltiung
    /// </summary>
    [Id("ApiUserManagementV1")]
    [Segment("user", "")]
    [ContextPath("/api/v1")]
    [Module("webexpress.webapp")]
    [Optional]
    public sealed class RestUserManagement : ResourceRestCrud<WebItemUser>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RestUserManagement()
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
        /// Processing of the resource. des GET-Request
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Eine Aufzählung, welche JsonSerializer serialisiert werden kann.</returns>
        public override IEnumerable<ResourceRestCrudColumn> GetColumns(Request request)
        {
            return new ResourceRestCrudColumn[]
            {
                new ResourceRestCrudColumn(I18N(request, "webexpress.webapp:setting.usermanager.user.login.label"))
                {
                    Render = "return item.Login;",
                    Width = 5
                },
                new ResourceRestCrudColumn(I18N(request, "webexpress.webapp:setting.usermanager.user.name.label"))
                {
                    Render = "return item.Lastname + ', ' + item.Firstname;"
                },
                new ResourceRestCrudColumn(I18N(request, "webexpress.webapp:setting.usermanager.user.groups.label"))
                {
                    Render = "var html = '<ul>' + item.Groups.map(function(group) { return '<li>' + group.Name + '</li>'; }).join('') + '</ul>'; return html;"
                },
                new ResourceRestCrudColumn(I18N(request, "webexpress.webapp:setting.usermanager.user.email.label"))
                {
                    Render = "return item.Email;"
                }
            };
        }

        /// <summary>
        /// Processing of the resource. des GET-Request
        /// </summary>
        /// <param name="wql">Der Filter</param>
        /// <param name="request">The request.</param>
        /// <returns>Eine Aufzählung, welche JsonSerializer serialisiert werden kann.</returns>
        public override IEnumerable<WebItemUser> GetData(WqlStatement wql, Request request)
        {
            var users = UserManager.Users.AsQueryable();
            users = wql.Apply(users);

            return users.Select(x => new WebItemUser()
            {
                Id = x.ID,
                Login = x.Login,
                Firstname = x.Firstname,
                Lastname = x.Lastname,
                Email = x.Email,
                Groups = x.Groups.Select(y => new WebItemGroup() { Id = y.ID, Name = y.Name })
            });
        }

        /// <summary>
        /// Processing of the resource. des DELETE-Request
        /// </summary>
        /// <param name="id">Die zu löschende ID</param>
        /// <param name="request">The request.</param>
        /// <returns>Das Ergebnis der Löschung</returns>
        public override bool DeleteData(string id, Request request)
        {
            return true;
        }
    }
}
