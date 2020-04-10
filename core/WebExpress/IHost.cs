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
        HttpServerContext Context { get; }
    }
}
