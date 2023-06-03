using System.Collections.Generic;
using WebExpress.UI.WebSettingPage;
using WebExpress.WebApp.WebScope;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebPage;
using WebExpress.WebPlugin;
using WebExpress.WebResource;

[assembly: WebExSystemPlugin()]

namespace WebExpress.WebApp
{
    [WebExName("WebExpress.WebApp")]
    [WebExDescription("plugin.description")]
    [WebExIcon("/assets/img/Logo.png")]
    [WebExDependency("webexpress.ui")]
    public sealed class Plugin : IPlugin
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Plugin()
        {
            ComponentManager.ResourceManager.AddResource += AddResource;
        }

        /// <summary>
        /// Initialization of the plugin. Here, for example, managed resources can be loaded. 
        /// </summary>
        /// <param name="context">The context of the plugin that applies to the execution of the plugin.</param>
        public void Initialization(IPluginContext context)
        {

        }

        /// <summary>
        /// Called when the plugin starts working. Run is called concurrently.
        /// </summary>
        public void Run()
        {
        }

        /// <summary>
        /// Release of unmanaged resources reserved during use.
        /// </summary>
        public void Dispose()
        {
            ComponentManager.ResourceManager.AddResource -= AddResource;
        }

        /// <summary>
        /// Manipulates the new resources by adding the default scops.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="resourceContext">The resource context.</param>
        private void AddResource(object sender, IResourceContext resourceContext)
        {
            if (resourceContext?.Scopes is List<string> scopes)
            {
                var scopeGeneral = typeof(ScopeGeneral).FullName.ToString().ToLower();
                var scopeSetting = typeof(ScopeSetting).FullName.ToString().ToLower();

                if (resourceContext is IPage && !scopes.Contains(scopeGeneral))
                {
                    scopes.Add(scopeGeneral);
                }

                if (resourceContext is IPageSetting && !scopes.Contains(scopeSetting))
                {
                    scopes.Add(scopeSetting);
                }
            }
        }
    }
}
