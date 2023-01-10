namespace WebExpress.WebApplication
{
    /// <summary>
    /// Represents an application entry in the application directory.
    /// </summary>
    internal class ApplicationItem
    {
        /// <summary>
        /// The context associated with the application.
        /// </summary>
        public IApplicationContext Context { get; set; }

        /// <summary>
        /// The application.
        /// </summary>
        public IApplication Application { get; set; }
    }
}
