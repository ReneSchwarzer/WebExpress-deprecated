﻿using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.WebAttribute;
using WebExpress.WebCondition;
using WebExpress.WebModule;
using WebExpress.Uri;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.WebResource
{
    public static class ResourceManager
    {
        /// <summary>
        /// Liefert oder setzt den Verweis auf Kontext des Hostes
        /// </summary>
        private static IHttpServerContext Context { get; set; }

        /// <summary>
        /// Liefert oder setzt das Verzeichnis, indem die Anwendungen gelistet sind
        /// </summary>
        private static ResourceDictionary Dictionary { get; } = new ResourceDictionary();

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Verweis auf den Kontext des Hostes</param>
        internal static void Initialization(IHttpServerContext context)
        {
            Context = context;

            Context.Log.Info(message: I18N("webexpress:resourcemanager.initialization"));
        }

        /// <summary>
        /// Registriert die Ressourcen der Module
        /// </summary>
        internal static void Register()
        {
            foreach (var module in ModuleManager.Modules)
            {
                Register(module);
            }
        }

        /// <summary>
        /// Registriert die Ressourcen eines Moduls
        /// </summary>
        /// <param name="moduleContext">Der Kontext des Moduls, welches die Ressourcen enthält</param>
        public static void Register(IModuleContext moduleContext)
        {
            var assembly = moduleContext?.Assembly;
            var contextPath = moduleContext.ContextPath;
            var root = new SitemapNode() { Dummy = true };
            var applicationID = moduleContext?.Application.ApplicationID;

            foreach (var resource in assembly.GetTypes().Where(x => x.IsClass == true && x.IsSealed && x.GetInterface(typeof(IResource).Name) != null))
            {
                var id = resource.Name?.ToLower();
                var segment = null as ISegmentAttribute;
                var title = resource.Name;
                var paths = new List<string>();
                var includeSubPaths = false;
                var moduleID = string.Empty;
                var resourceContext = new List<string>();
                var optional = false;
                var conditions = new List<ICondition>();
                var cache = false;

                foreach (var customAttribute in resource.CustomAttributes.Where(x => x.AttributeType.GetInterfaces().Contains(typeof(IResourceAttribute))))
                {
                    if (customAttribute.AttributeType == typeof(IDAttribute))
                    {
                        id = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                    }
                    else if (customAttribute.AttributeType.GetInterfaces().Contains(typeof(ISegmentAttribute)))
                    {
                        segment = resource.GetCustomAttributes(customAttribute.AttributeType, false).FirstOrDefault() as ISegmentAttribute;
                    }
                    else if (customAttribute.AttributeType == typeof(TitleAttribute))
                    {
                        title = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                    }
                    else if (customAttribute.AttributeType == typeof(PathAttribute))
                    {
                        paths.Add(customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString());
                    }
                    else if (customAttribute.AttributeType == typeof(IncludeSubPathsAttribute))
                    {
                        includeSubPaths = Convert.ToBoolean(customAttribute.ConstructorArguments.FirstOrDefault().Value);
                    }
                    else if (customAttribute.AttributeType == typeof(ModuleAttribute))
                    {
                        moduleID = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString().ToLower();
                    }
                    else if (customAttribute.AttributeType == typeof(ContextAttribute))
                    {
                        resourceContext.Add(customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString().ToLower());
                    }
                    else if (customAttribute.AttributeType == typeof(OptionalAttribute))
                    {
                        optional = true;
                    }
                    else if (customAttribute.AttributeType == typeof(ConditionAttribute))
                    {
                        var condition = (Type)customAttribute.ConstructorArguments.FirstOrDefault().Value;

                        if (condition.GetInterfaces().Contains(typeof(ICondition)))
                        {
                            conditions.Add(condition?.Assembly.CreateInstance(condition?.FullName) as ICondition);
                        }
                        else
                        {
                            Context.Log.Warning(message: I18N("webexpress:resourcemanager.wrongtype"), args: new object[] { condition.Name, typeof(ICondition).Name });
                        }
                    }
                    else if (customAttribute.AttributeType == typeof(CacheAttribute))
                    {
                        cache = true;
                    }
                }

                // Prüfe ob eine optionale Ressource
                if (optional && !(moduleContext.Application.Options.Contains($"{moduleID}.*".ToLower()) || moduleContext.Application.Options.Contains($"{moduleID}.{id}".ToLower())))
                {
                    continue;
                }

                // Zugehöriges Modul ermitteln. 
                var module = ModuleManager.GetModule(moduleContext.Application.ApplicationID, moduleID);
                if (string.IsNullOrEmpty(moduleID))
                {
                    // Es wurde kein Modul angebgeben
                    Context.Log.Warning(message: I18N("webexpress:resourcemanager.moduleless"), args: id);
                }
                else if (module == null)
                {
                    // Modul wurde nicht gefunden 
                    Context.Log.Warning(message: I18N("webexpress:resourcemanager.modulenotfound"), args: new object[] { id, moduleID });
                }
                else if (!module.ModuleID.Equals(moduleContext.ModuleID, StringComparison.OrdinalIgnoreCase))
                {
                    // Ressource gehört nicht zum Modul
                    Context.Log.Warning(message: I18N("webexpress:resourcemanager.wrongmodule"), args: new object[] { applicationID, moduleID, id });
                }
                else if (paths.Count == 0 || (paths.Count == 1 && string.IsNullOrWhiteSpace(paths.FirstOrDefault())))
                {
                    // Root setzen
                    if (root.Dummy)
                    {
                        root.ID = id;
                        root.Title = title;
                        root.Type = resource;
                        root.Context = new ResourceContext(moduleContext) { Context = resourceContext, Conditions = conditions, Cache = cache };
                        root.ResourceContext = resourceContext;
                        root.IncludeSubPaths = includeSubPaths;
                        root.PathSegment = segment.ToPathSegment();
                        root.Dummy = false;
                        
                        Context.Log.Info(message: I18N("webexpress:resourcemanager.addresource"), args: new object[] { "ROOT", applicationID, moduleID });
                    }
                }
                else
                {
                    foreach (var p in paths)
                    {
                        var uri = new UriRelative(p);
                        var skip = uri.Skip(1);

                        Context.Log.Info(message: I18N("webexpress:resourcemanager.addresource"), args: new object[] { id, applicationID, moduleID });

                        // Ressourceneintrag erstellen und Eigenschaften setzen
                        var node = root.Insert(skip, id);

                        if (node != null)
                        {
                            node.PathSegment = segment.ToPathSegment();
                            node.Title = title;
                            node.Type = resource;
                            node.Context = new ResourceContext(moduleContext) { Context = resourceContext, Conditions = conditions, Cache = cache };
                            node.ResourceContext = resourceContext;
                            node.IncludeSubPaths = includeSubPaths;
                        }
                        else
                        {
                            Context.Log.Warning(message: I18N("webexpress:resourcemanager.addresource.error"), args: new object[] { id, moduleID });
                        }
                    }
                }
            }

            if (!Dictionary.ContainsKey(moduleContext))
            {
                Dictionary[moduleContext] = root;

                Context.Log.Info(message: I18N("webexpress:resourcemanager.register"), args: new object[] { root.GetPreOrder().Count, moduleContext?.ModuleID?.ToLower() });
            }

            Context.Log.Debug(message: I18N("webexpress:resourcemanager.sitemap"), args: moduleContext.ModuleID);

            foreach (var item in root.GetPreOrder())
            {
                Context.Log.Debug(message: "   {0} => '{1}'", args: new object[] { item.IDPath, item.ExpressionPath });
            }
        }

        /// <summary>
        /// Sucht die zur URL gehörende Ressource
        /// </summary>
        /// <param name="uri">Die Uri</param>
        /// <param name="context">Der Suchkontext</param>
        /// <returns>Die Ressource oder null</returns>
        public static SearchResult Find(string url, SearchContext context)
        {
            foreach (var module in Dictionary)
            {
                var root = module.Value;
                var contextPath = module.Key.ContextPath;
                var requestUri = new UriRelative(url);

                if (requestUri.StartsWith(contextPath))
                {
                    var uri = new UriRelative(requestUri.ToString()[contextPath.ToString().Length..]);
                    var result = root.Find(uri, context);

                    if (result != null && result.Context != null)
                    {
                        if (!result.Context.Conditions.Any() || result.Context.Conditions.All(x => x.Fulfillment(context.Request)))
                        {
                            return result;
                        }
                    }
                }
            }

            // 404
            return null;
        }

        /// <summary>
        /// Sucht die Ressource anhand seiner ID
        /// </summary>
        /// <param name="applicationID">Die AnwendungsID</param>
        /// <param name="uri">Die ID</param>
        /// <returns>Die Ressource oder null</returns>
        public static SitemapNode FindByID(string applicationID, string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return null;

            foreach (var module in Dictionary.Where(x => x.Key.Application.ApplicationID.Equals(applicationID, StringComparison.OrdinalIgnoreCase)))
            {
                var item = module.Value.Root.FindItem(id);
                if (item != null)
                {
                    return item;
                }
            }

            // 404
            return null;
        }

        /// <summary>
        /// Liefert die Sitemap eines Ressourceneintrages
        /// </summary>
        /// <param name="applicationID">Die AnwendungsID</param>
        /// <param name="moduleID">Die ModulID</param>
        /// <returns>Die Sitemap als pre-order-Liste</returns>
        public static ICollection<SitemapNode> GetSitemap(string applicationID, string moduleID)
        {
            var module = ModuleManager.GetModule(applicationID, moduleID);

            if (Dictionary.ContainsKey(module))
            {
                return Dictionary[module].GetPreOrder();
            }

            return null;
        }
    }
}
