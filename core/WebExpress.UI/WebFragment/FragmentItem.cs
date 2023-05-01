using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Internationalization;
using WebExpress.WebCondition;
using WebExpress.WebModule;
using WebExpress.WebPlugin;

namespace WebExpress.UI.WebFragment
{
    /// <summary>
    /// Fragments are components that can be integrated into pages to dynamically expand functionalities.
    /// </summary>
    internal class FragmentItem : IDisposable
    {
        /// <summary>
        /// An event that fires when an fragment is added.
        /// </summary>
        public event EventHandler<IFragmentContext> AddFragment;

        /// <summary>
        /// An event that fires when an fragment is removed.
        /// </summary>
        public event EventHandler<IFragmentContext> RemoveFragment;

        /// <summary>
        /// Returns the context of the associated plugin.
        /// </summary>
        public IPluginContext PluginContext { get; set; }

        /// <summary>
        /// Returns the module id.
        /// </summary>
        public string ModuleID { get; set; }

        /// <summary>
        /// The type of fragment.
        /// </summary>
        public Type FragmentClass { get; set; }

        /// <summary>
        /// Returns the section.
        /// </summary>
        public string Section { get; set; }

        /// <summary>
        /// Returns the contexts.
        /// </summary>
        public IEnumerable<string> Contexts { get; set; }

        /// <summary>
        /// Returns the conditions that must be met for the component to be active.
        /// </summary>
        public ICollection<ICondition> Conditions { get; set; }

        /// <summary>
        /// The order of the fragment.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Determines whether the component is created once and reused on each execution.
        /// </summary>
        public bool Cache { get; set; }

        /// <summary>
        /// Returns the log to write status messages to the console and to a log file.
        /// </summary>
        public Log Log { get; internal set; }

        /// <summary>
        /// Returns the directory where the module instances are listed.
        /// </summary>
        private IDictionary<IModuleContext, IFragmentContext> Dictionary { get; }
            = new Dictionary<IModuleContext, IFragmentContext>();

        /// <summary>
        /// Returns all fragment contexts.
        /// </summary>
        public IEnumerable<IFragmentContext> FragmentContexts => Dictionary.Values
            .Select(x => x);

        /// <summary>
        /// Adds an module assignment
        /// </summary>
        /// <param name="moduleContext">The context of the module.</param>
        public void AddModule(IModuleContext moduleContext)
        {
            // only if no instance has been created yet
            if (Dictionary.ContainsKey(moduleContext))
            {
                Log.Warning(message: InternationalizationManager.I18N("webexpress:fragmentmanager.addfragment.duplicate", FragmentClass.Name, moduleContext.ModuleID));

                return;
            }

            // create context
            var fragmentContext = new FragmentContext()
            {
                PluginContext = PluginContext,
                ModuleContext = moduleContext,
                Conditions = Conditions,
                Cache = Cache
            };

            Dictionary.Add(moduleContext, fragmentContext);

            OnAddFragment(fragmentContext);
        }

        /// <summary>
        /// Remove an module assignment
        /// </summary>
        /// <param name="moduleContext">The context of the module.</param>
        public void DetachModule(IModuleContext moduleContext)
        {
            // not an assignment has been created yet
            if (!Dictionary.ContainsKey(moduleContext))
            {
                return;
            }

            foreach (var fragmentContext in Dictionary.Values)
            {
                OnRemoveFragment(fragmentContext);
            }

            Dictionary.Remove(moduleContext);
        }

        /// <summary>
        /// Checks whether a module context is already assigned to the item.
        /// </summary>
        /// <param name="moduleContext">The module context.</param>
        /// <returns>True a mapping exists, false otherwise.</returns>
        public bool IsAssociatedWithModule(IModuleContext moduleContext)
        {
            return Dictionary.ContainsKey(moduleContext);
        }

        /// <summary>
        /// Raises the AddFragment event.
        /// </summary>
        /// <param name="fragmentContext">The fragment context.</param>
        private void OnAddFragment(IFragmentContext fragmentContext)
        {
            AddFragment?.Invoke(this, fragmentContext);
        }

        /// <summary>
        /// Raises the RemoveFragment event.
        /// </summary>
        /// <param name="fragmentContext">The fragment context.</param>
        private void OnRemoveFragment(IFragmentContext fragmentContext)
        {
            RemoveFragment?.Invoke(this, fragmentContext);
        }

        /// <summary>
        /// Performs application-specific tasks related to sharing, returning, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            foreach (Delegate d in AddFragment.GetInvocationList())
            {
                AddFragment -= (EventHandler<IFragmentContext>)d;
            }

            foreach (Delegate d in RemoveFragment.GetInvocationList())
            {
                RemoveFragment -= (EventHandler<IFragmentContext>)d;
            }
        }

        /// <summary>
        /// Convert the resource element to a string.
        /// </summary>
        /// <returns>The resource element in its string representation.</returns>
        public override string ToString()
        {
            return $"Fragment '{FragmentClass.Name}'";
        }
    }
}
