﻿using System;

namespace WebExpress.WebAttribute
{
    /// <summary>
    /// Indicates that a status page ist default
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DefaultAttribute : Attribute, IStatusPageAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DefaultAttribute()
        {

        }
    }
}
