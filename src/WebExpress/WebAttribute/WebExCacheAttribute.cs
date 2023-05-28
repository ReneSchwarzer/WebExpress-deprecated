﻿using System;

namespace WebExpress.WebAttribute
{
    /// <summary>
    /// Indicates that a page or component can be reused
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class WebExCacheAttribute : System.Attribute, IResourceAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public WebExCacheAttribute()
        {

        }
    }
}
