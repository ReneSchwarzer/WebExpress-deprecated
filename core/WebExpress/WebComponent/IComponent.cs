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
        static IHttpServerContext HttpServerContext { get; }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The reference to the context of the host.</param>
        void Initialization(IHttpServerContext context);
    }
}
