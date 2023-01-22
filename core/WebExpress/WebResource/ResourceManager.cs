﻿using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebCondition;
using WebExpress.WebPlugin;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.WebResource
{
    /// <summary>
    /// The resource manager manages WebExpress elements, which can be called with a URI (Uniform Resource Identifier).
    /// </summary>
    public sealed class ResourceManager : IComponentPlugin, ISystemComponent
    {
        /// <summary>
        /// Returns the reference to the context of the host.
        /// </summary>
        public IHttpServerContext Context { get; private set; }

        /// <summary>
        /// Returns the directory where the resources are listed.
        /// </summary>
        private static ResourceDictionary Dictionary { get; } = new ResourceDictionary();

        /// <summary>
        /// Returns all resources
        /// </summary>
        internal static IEnumerable<ResourceItem> Resources => Dictionary.Values.SelectMany(x => x.Values);

        /// <summary>
        /// Constructor
        /// </summary>
        internal ResourceManager()
        {

        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The reference to the context of the host.</param>
        public void Initialization(IHttpServerContext context)
        {
            Context = context;

            Context.Log.Info(message: I18N("webexpress:resourcemanager.initialization"));
        }

        /// <summary>
        /// Discovers and registers resources from the specified plugin.
        /// </summary>
        /// <param name="pluginContext">A context of a plugin whose resources are to be registered.</param>
        public void Register(IPluginContext pluginContext)
        {
            var assembly = pluginContext?.Assembly;

            if (!Dictionary.ContainsKey(pluginContext))
            {
                Dictionary.Add(pluginContext, new Dictionary<string, ResourceItem>());
            }

            var dict = Dictionary[pluginContext];

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
                    if (customAttribute.AttributeType == typeof(IdAttribute))
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
                            Context.Log.Warning(message: I18N("webexpress:resourcemanager.wrongtype", condition.Name, typeof(ICondition).Name));
                        }
                    }
                    else if (customAttribute.AttributeType == typeof(CacheAttribute))
                    {
                        cache = true;
                    }
                }

                // determine the associated module 
                var module = ComponentManager.ModuleManager.GetModule(pluginContext, moduleID);
                if (string.IsNullOrEmpty(moduleID))
                {
                    // no module specified
                    Context.Log.Warning(message: I18N("webexpress:resourcemanager.moduleless", id));
                }
                else if (module == null)
                {
                    // module not found 
                    Context.Log.Warning(message: I18N("webexpress:resourcemanager.modulenotfound", id, moduleID));
                }
                else
                {
                    if (!dict.ContainsKey(id))
                    {
                        var context = new ResourceContext(module) { ContentContext = resourceContext, Conditions = conditions, Cache = cache };

                        dict.Add(id, new ResourceItem()
                        {
                            ID = id,
                            Title = title,
                            Type = resource,
                            Context = context,
                            ResourceContext = resourceContext,
                            Cache = cache,
                            Optional = optional,
                            Conditions = conditions,
                            Paths = paths,
                            IncludeSubPaths = includeSubPaths,
                            PathSegment = segment.ToPathSegment()
                        });

                        Context.Log.Info(message: I18N("webexpress:resourcemanager.addresource", id, module.ModuleID));
                    }
                    else
                    {
                        Context.Log.Warning(message: I18N("webexpress:resourcemanager.addresource.duplicate", id, module.ModuleID));
                    }
                }
            }
        }

        /// <summary>
        /// Discovers and registers resources from the specified plugin.
        /// </summary>
        /// <param name="pluginContexts">A list with plugin contexts that contain the resources.</param>
        public void Register(IEnumerable<IPluginContext> pluginContexts)
        {
            foreach (var pluginContext in pluginContexts)
            {
                Register(pluginContext);
            }
        }

        /// <summary>
        /// Removes all resources associated with the specified plugin context.
        /// </summary>
        /// <param name="pluginContext">The context of the plugin that contains the resources to remove.</param>
        public void Remove(IPluginContext pluginContext)
        {

        }
    }
}
