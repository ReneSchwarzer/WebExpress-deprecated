﻿using WebExpress.WebAttribute;

namespace WebExpress.WebApp.WebResource
{
    /// <summary>
    /// Lieferung einer im Assamby eingebetteten Ressource
    /// </summary>
    [Id("Asset")]
    [Segment("assets")]
    [ContextPath("/")]
    [IncludeSubPaths(true)]
    [Module("webexpress.webapp")]
    public sealed class Asset : WebExpress.WebResource.ResourceAsset
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Asset()
        {
        }
    }
}