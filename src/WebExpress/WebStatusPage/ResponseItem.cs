﻿using System;
using WebExpress.WebPlugin;

namespace WebExpress.WebStatusPage
{
    public class ResponseItem
    {
        /// <summary>
        /// Returns the associated plugin context.
        /// </summary>
        public IPluginContext PluginContext { get; internal set; }

        /// <summary>
        /// Returns or sets the resource id.
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// Returns or sets the status code.
        /// </summary>
        public int StatusCode { get; internal set; }

        /// <summary>
        /// Returns or sets the type of status page.
        /// </summary>
        public Type StatusPageClass { get; internal set; }

        /// <summary>
        /// Returns or sets the module id.
        /// </summary>
        public string ModuleId { get; internal set; }
    }
}
