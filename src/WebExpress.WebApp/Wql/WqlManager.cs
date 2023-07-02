using System;
using System.Collections.Generic;
using WebExpress.Internationalization;
using WebExpress.WebApp.Wql.Condition;
using WebExpress.WebApp.Wql.Function;
using WebExpress.WebComponent;
using WebExpress.WebPlugin;

namespace WebExpress.WebApp.Wql
{
    public sealed class WqlManager : IComponentPlugin
    {
        /// <summary>
        /// An event that fires when an condition is added.
        /// </summary>
        public event EventHandler<IWqlExpressionNodeFilterConditionContext> AddCondition;

        /// <summary>
        /// An event that fires when an condition is removed.
        /// </summary>
        public event EventHandler<IWqlExpressionNodeFilterConditionContext> RemoveCondition;

        /// <summary>
        /// An event that fires when an function is added.
        /// </summary>
        public event EventHandler<IWqlExpressionNodeFilterFunctionContext> AddFunction;

        /// <summary>
        /// An event that fires when an function is removed.
        /// </summary>
        public event EventHandler<IWqlExpressionNodeFilterFunctionContext> RemoveFunction;


        /// <summary>
        /// Returns the reference to the context of the host.
        /// </summary>
        public IHttpServerContext HttpServerContext { get; private set; }

        /// <summary>
        /// Returns the wql parser.
        /// </summary>
        public WqlParser Parser { get; private set; } = new WqlParser();

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

            Parser.RegisterCondition<WqlExpressionNodeFilterConditionBinaryEqual>();
            Parser.RegisterCondition<WqlExpressionNodeFilterConditionBinaryGreaterThan>();
            Parser.RegisterCondition<WqlExpressionNodeFilterConditionBinaryGreaterThanOrEqual>();
            Parser.RegisterCondition<WqlExpressionNodeFilterConditionBinaryLessThan>();
            Parser.RegisterCondition<WqlExpressionNodeFilterConditionBinaryLessThanOrEqual>();
            Parser.RegisterCondition<WqlExpressionNodeFilterConditionBinaryLike>();

            Parser.RegisterCondition<WqlExpressionNodeFilterConditionSetIn>();
            Parser.RegisterCondition<WqlExpressionNodeFilterConditionSetNotIn>();

            Parser.RegisterFunction<WqlExpressionNodeFilterFunctionDay>();
            Parser.RegisterFunction<WqlExpressionNodeFilterFunctionNow>();
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
        /// <param name="conditionContext">The condition context.</param>
        private void OnAddCondition(IWqlExpressionNodeFilterConditionContext conditionContext)
        {
            AddCondition?.Invoke(this, conditionContext);
        }

        /// <summary>
        /// Raises the RemoveCondition event.
        /// </summary>
        /// <param name="conditionContext">The condition context.</param>
        private void OnRemoveCondition(IWqlExpressionNodeFilterConditionContext conditionContext)
        {
            RemoveCondition?.Invoke(this, conditionContext);
        }

        /// <summary>
        /// Raises the AddFunction event.
        /// </summary>
        /// <param name="functionContext">The function context.</param>
        private void OnAddFunction(IWqlExpressionNodeFilterFunctionContext functionContext)
        {
            AddFunction?.Invoke(this, functionContext);
        }

        /// <summary>
        /// Raises the RemoveFunction event.
        /// </summary>
        /// <param name="functionContext">The function context.</param>
        private void OnRemoveFunction(IWqlExpressionNodeFilterFunctionContext functionContext)
        {
            RemoveFunction?.Invoke(this, functionContext);
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
