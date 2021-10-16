namespace WebExpress.Message
{
    /// <summary>
    /// Definiert die Gültigkeitsbereiche des Parameters
    /// </summary>
    public enum ParameterScope
    {
        /// <summary>
        /// Keine Einornung
        /// </summary>
        None,

        /// <summary>
        /// Parameter beziet sich auf einen Teil der URI
        /// </summary>
        Url,

        /// <summary>
        /// Parameter bezieht sich auf die Session
        /// </summary>
        Session,

        /// <summary>
        /// Parameter bezieht sich auf Url-Parameter (GET oder POST)
        /// </summary>
        Parameter
    }
}
