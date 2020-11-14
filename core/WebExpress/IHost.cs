using System;
using System.Collections.Generic;
using WebExpress.Plugins;

namespace WebExpress
{
    /// <summary>
    /// Hostschnittstelle
    /// </summary>
    public interface IHost
    {
        /// <summary>
        /// Der Kontext
        /// </summary>
        static HttpServerContext Context { get; }

        /// <summary>
        /// Erstellt Instanzen aus den Typen der geladenen Plugins
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> CreatePluginComponet<T>() where T : IPluginComponent;
    }
}
