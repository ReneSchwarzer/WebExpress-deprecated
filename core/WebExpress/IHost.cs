using System;

namespace WebExpress
{
    /// <summary>
    /// The host interface.
    /// </summary>
    public interface IHost
    {
        /// <summary>
        /// Returns the context of the host.
        /// </summary>
        IHttpServerContext Context { get; }

        /// <summary>
        /// Event is triggered after the web server starts.
        /// </summary>
        event EventHandler Started;
    }
}
