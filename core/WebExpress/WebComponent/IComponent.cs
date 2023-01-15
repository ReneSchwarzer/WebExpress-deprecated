using System.Collections.Generic;
using WebExpress.WebModule;
using WebExpress.WebPlugin;

namespace WebExpress.WebComponent
{
    /// <summary>
    /// Interface of the manager classes.
    /// </summary>
    public interface IComponent
    {
        /// <summary>
        /// Returns the reference to the context of the host.
        /// </summary>
        static IHttpServerContext Context { get; }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The reference to the context of the host.</param>
        void Initialization(IHttpServerContext context);
    }
}
