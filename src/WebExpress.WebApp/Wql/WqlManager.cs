using System;
using System.Collections.Generic;
using WebExpress.Internationalization;
using WebExpress.UI.WebFragment;
using WebExpress.WebApp.Wql.Condition;
using WebExpress.WebComponent;
using WebExpress.WebPlugin;

namespace WebExpress.WebApp.Wql
{
    public sealed class WqlManager : IComponentPlugin
    {
        /// <summary>
        /// An event that fires when an condition is added.
        /// </summary>
        public event EventHandler<IFragmentContext> AddCondition;

        /// <summary>
        /// An event that fires when an condition is removed.
        /// </summary>
        public event EventHandler<IFragmentContext> RemoveCondition;

        /// <summary>
        /// Returns the reference to the context of the host.
        /// </summary>
        public IHttpServerContext HttpServerContext { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        internal WqlManager()
        {
            ComponentManager.PluginManager.AddPlugin += (s, pluginContext) =>
            {
                Register(pluginContext);
            };
            ComponentManager.PluginManager.RemovePlugin += (s, pluginContext) =>
            {
                Remove(pluginContext);
            };

            WqlParser.Register<WqlExpressionConditionBinaryEqual>();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The reference to the context of the host.</param>
        public void Initialization(IHttpServerContext context)
        {
            HttpServerContext = context;

            HttpServerContext.Log.Debug
            (
                InternationalizationManager.I18N("webexpress.webapp:wqlmanager.initialization")
            );
        }

        /// <summary>
        /// Discovers and registers entries from the specified plugin.
        /// </summary>
        /// <param name="pluginContext">A context of a plugin whose elements are to be registered.</param>
        public void Register(IPluginContext pluginContext)
        {

        }

        /// <summary>
        /// Discovers and registers entries from the specified plugin.
        /// </summary>
        /// <param name="pluginContexts">A list with plugin contexts that contain the components.</param>
        public void Register(IEnumerable<IPluginContext> pluginContexts)
        {
            foreach (var pluginContext in pluginContexts)
            {
                Register(pluginContext);
            }
        }

        /// <summary>
        /// Removes all components associated with the specified plugin context.
        /// </summary>
        /// <param name="pluginContext">The context of the plugin that contains the components to remove.</param>
        public void Remove(IPluginContext pluginContext)
        {
            //Dictionary.Remove(pluginContext);
        }

        /// <summary>
        /// Raises the AddCondition event.
        /// </summary>
        /// <param name="fragmentContext">The condition context.</param>
        private void OnAddCondition(IFragmentContext fragmentContext)
        {
            AddCondition?.Invoke(this, fragmentContext);
        }

        /// <summary>
        /// Raises the RemoveCondition event.
        /// </summary>
        /// <param name="fragmentContext">The condition context.</param>
        private void OnRemoveCondition(IFragmentContext fragmentContext)
        {
            RemoveCondition?.Invoke(this, fragmentContext);
        }

        /// <summary>
        /// Information about the component is collected and prepared for output in the log.
        /// </summary>
        /// <param name="pluginContext">The context of the plugin.</param>
        /// <param name="output">A list of log entries.</param>
        /// <param name="deep">The shaft deep.</param>
        public void PrepareForLog(IPluginContext pluginContext, IList<string> output, int deep)
        {

        }
    }
}
