using System;
using System.Linq;
using System.Reflection;
using WebExpress.Application;
using WebExpress.Attribute;
using WebExpress.WebResource;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.Message
{
    public static class ResponseManager
    {
        /// <summary>
        /// Liefert oder setzt den Verweis auf Kontext des Hostes
        /// </summary>
        private static IHttpServerContext Context { get; set; }

        /// <summary>
        /// Liefert oder setzt das Verzeichnis, indem die Anwendungen gelistet sind
        /// </summary>
        private static ResponseDictionary Dictionary { get; } = new ResponseDictionary();

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Verweis auf den Kontext des Hostes</param>
        internal static void Initialization(IHttpServerContext context)
        {
            Context = context;

            Context.Log.Info(message: I18N("webexpress:responsemanager.initialization"));
        }

        /// <summary>
        /// Registriert die Statusseiten einer Anwendung
        /// </summary>
        internal static void Register()
        {
            foreach (var application in ApplicationManager.Applications)
            {
                Register(application);
            }
        }

        /// <summary>
        /// Registriert die Statusseiten einer Anwendung
        /// </summary>
        /// <param name="applicationContext">Der Kontext des Moduls, welches die Ressourcen enthält</param>
        public static void Register(IApplicationContext applicationContext)
        {
            var assembly = applicationContext?.Assembly;

            foreach (var resource in assembly.GetTypes().Where(x => x.IsClass == true && x.IsSealed && x.GetInterface(typeof(IPageStatus).Name) != null))
            {
                var statusCode = -1;

                foreach (var customAttribute in resource.CustomAttributes.Where(x => x.AttributeType.GetInterfaces().Contains(typeof(IApplicationAttribute))))
                {
                    if (customAttribute.AttributeType == typeof(StatusCodeAttribute))
                    {
                        statusCode = Convert.ToInt32(customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString());
                    }
                }

                if (statusCode > 0)
                {
                    if (!Dictionary.ContainsKey(applicationContext))
                    {
                        Dictionary.Add(applicationContext, new System.Collections.Generic.Dictionary<int, Type>());
                    }

                    var item = Dictionary[applicationContext];
                    if (!item.ContainsKey(statusCode))
                    {
                        item.Add(statusCode, resource);
                        Context.Log.Info(message: I18N("webexpress:responsemanager.register"), args: new object[] { statusCode, applicationContext.ApplicationID, resource.Name });
                    }
                    else
                    {
                        Context.Log.Info(message: I18N("webexpress:responsemanager.duplicat"), args: new object[] { statusCode, applicationContext.ApplicationID, resource.Name });
                    }
                }
                else
                {
                    Context.Log.Info(message: I18N("webexpress:responsemanager.statuscode"), args: new object[] { resource.Name, applicationContext.ApplicationID});
                }
            }
        }

        /// <summary>
        /// Erstellt eine Statusseite
        /// </summary>
        /// <param name="status">Der Status</param>
        /// <param name="applicationID">Die AnwendungsID</param>
        /// <returns>Die Statusseite oder null</returns>
        public static IPageStatus Create(int status, string applicationID)
        {
            var application = ApplicationManager.GetApplcation(applicationID);

            try
            {
                if (application != null &&  Dictionary.ContainsKey(application))
                {
                    var item = Dictionary[application];
                    if (item.ContainsKey(status))
                    {
                        var type = item[status];

                        return application.Assembly.CreateInstance(type?.FullName) as IPageStatus;
                    }
                }
            }
            catch
            {

            }

            return null;
        }
    }
}
