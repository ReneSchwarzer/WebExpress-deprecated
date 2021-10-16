﻿using System;
using System.Linq;
using WebExpress.Application;
using WebExpress.Attribute;
using WebExpress.Module;
using WebExpress.Uri;
using WebExpress.WebPage;
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
        /// Registriert die Statusseiten eines Moduls
        /// </summary>
        internal static void Register()
        {
            foreach (var mudule in ModuleManager.Modules)
            {
                Register(mudule);
            }
        }

        /// <summary>
        /// Registriert die Statusseiten einer Anwendung
        /// </summary>
        /// <param name="moduleContext">Der Kontext des Moduls, welches die Ressourcen enthält</param>
        public static void Register(IModuleContext moduleContext)
        {
            var assembly = moduleContext?.Assembly;

            foreach (var resource in assembly.GetTypes().Where(x => x.IsClass == true && x.IsSealed && x.GetInterface(typeof(IPageStatus).Name) != null))
            {
                var statusCode = -1;
                var moduleID = string.Empty;

                foreach (var customAttribute in resource.CustomAttributes.Where(x => x.AttributeType.GetInterfaces().Contains(typeof(IApplicationAttribute))))
                {
                    if (customAttribute.AttributeType == typeof(StatusCodeAttribute))
                    {
                        statusCode = Convert.ToInt32(customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString());
                    }

                    if (customAttribute.AttributeType == typeof(ModuleAttribute))
                    {
                        moduleID = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString().ToLower();
                    }
                }

                if (statusCode > 0)
                {
                    if (!Dictionary.ContainsKey(moduleContext))
                    {
                        Dictionary.Add(moduleContext, new System.Collections.Generic.Dictionary<int, Type>());
                    }

                    var item = Dictionary[moduleContext];
                    if (!item.ContainsKey(statusCode))
                    {
                        item.Add(statusCode, resource);
                        Context.Log.Info(message: I18N("webexpress:responsemanager.register"), args: new object[] { statusCode, moduleContext.ApplicationID, moduleContext.ModuleID, resource.Name });
                    }
                    else
                    {
                        Context.Log.Info(message: I18N("webexpress:responsemanager.duplicat"), args: new object[] { statusCode, moduleContext.ApplicationID, moduleContext.ModuleID, resource.Name });
                    }
                }
                else
                {
                    Context.Log.Info(message: I18N("webexpress:responsemanager.statuscode"), args: new object[] { moduleContext.ApplicationID, moduleContext.ModuleID, resource.Name });
                }
            }
        }

        /// <summary>
        /// Erstellt eine Statusseite
        /// </summary>
        /// <param name="massage">Die Statusnachricht</param>
        /// <param name="status">Der Status</param>
        /// <param name="module">Das Module, indem sich die Statusseiten befinden oder null für eine undefinierte Seite (kann aus einem anderen Modul stammen), welche dem Statuscode entspricht</param>
        /// <param name="uri">Die URI</param>
        /// <returns>Die Statusseite oder null</returns>
        public static IPageStatus Create(string massage, int status, IModuleContext module, IUri uri)
        {
            try
            {
                if (module == null)
                {
                    module = GetDefaultModule(status, uri?.ToString(), module?.ModuleID);
                }

                if (module != null && Dictionary.ContainsKey(module))
                {
                    var item = Dictionary[module];
                    if (item.ContainsKey(status))
                    {
                        var type = item[status];
                        var statusPage = module.Assembly.CreateInstance(type?.FullName) as IPageStatus;
                        statusPage.StatusMessage = massage;
                        statusPage.StatusCode = status;

                        return statusPage;
                    }
                }
            }
            catch
            {

            }

            return null;
        }

        /// <summary>
        /// Liefert das erste verfügbare Modul, welches über eine Statusseite verfügt
        /// </summary>
        /// <param name="status">Der Status</param>
        /// <param name="uri">Die URI</param>
        /// <param name="moduleID">Die ModulID oder null</param>
        /// <returns>Die erste gefunde Statusseite zu den gegebenen Status oder null</returns>
        public static IModuleContext GetDefaultModule(int status, string uri, string moduleID)
        {
            var applications = ApplicationManager.GetApplcations().Where(x => uri != null && uri.StartsWith(x.ContextPath.ToString()));

            // 1. Bevorzugte Statusseite 
            var mudules = Dictionary.Where(x => applications.Select(x => x.ApplicationID).Contains(x.Key.ApplicationID))
                .Where(x => x.Key.ModuleID.Equals(moduleID, StringComparison.OrdinalIgnoreCase))
                .Where(x => x.Value.ContainsKey(status));

            var module = mudules.FirstOrDefault();

            if (module.Key != null)
            {
                return module.Key;
            }

            // 2. Wenn nicht vorhanden alternativ verfügbarte Statusseite
            mudules = Dictionary.Where(x => applications.Select(x => x.ApplicationID).Contains(x.Key.ApplicationID))
                .Where(x => x.Value.ContainsKey(status));

            module = mudules.FirstOrDefault();

            return module.Key;
        }
    }
}
