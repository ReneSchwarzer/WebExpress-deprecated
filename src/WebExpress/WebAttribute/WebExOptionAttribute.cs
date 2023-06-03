﻿using System;
using WebExpress.WebModule;
using WebExpress.WebResource;

namespace WebExpress.WebAttribute
{
    /// <summary>
    /// Activation of options (e.g. 'webexpress.webapp.settinglog' or 'webexpress.webapp.*').
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class WebExOptionAttribute : Attribute, IApplicationAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="option">The option to activate.</param>
        public WebExOptionAttribute(string option)
        {

        }
    }

    /// <summary>
    /// Activation of options (e.g. 'webexpress.webapp.settinglog' or 'webexpress.webapp.*').
    /// </summary>
    /// <typeparamref name="M">The module or resource class.</typeparamref/>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class WebExOptionAttribute<M> : Attribute, IApplicationAttribute where M : class, IModule
    {

    }

    /// <summary>
    /// Activation of options (e.g. 'webexpress.webapp.settinglog' or 'webexpress.webapp.*').
    /// <typeparamref name="M">The module or resource class.</typeparamref/>
    /// <typeparamref name="R">The resource or resource class.</typeparamref>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class WebExOptionAttribute<M, R> : Attribute, IApplicationAttribute where M : class, IModule where R : class, IResource
    {
    }
}
