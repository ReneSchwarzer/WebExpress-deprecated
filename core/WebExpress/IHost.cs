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
    }
}
