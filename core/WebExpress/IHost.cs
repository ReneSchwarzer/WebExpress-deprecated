using System;

namespace WebExpress
{
    /// <summary>
    /// Hostschnittstelle
    /// </summary>
    public interface IHost
    {
        /// <summary>
        /// Liefert den Kontext
        /// </summary>
        IHttpServerContext Context { get; }

        /// <summary>
        /// Event wird nach dem Start des Webservers ausgelöst
        /// </summary>
        event EventHandler Started;
    }
}
