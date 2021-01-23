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
        public IHttpServerContext Context { get; }
    }
}
