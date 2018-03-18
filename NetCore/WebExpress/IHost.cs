using System;
using System.Collections.Generic;
using System.Text;
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
        IPluginContext Context { get; }
    }
}
