﻿using System;
using WebExpress.WebApplication;

namespace WebExpress.WebAttribute
{
    /// <summary>
    /// Interface of a application assignment attribute.
    /// </summary>
    {

    }

    /// <summary>
    /// Application assignment attribute of the application ID.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ApplicationAttribute : Attribute, IWebExApplicationAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="applicationId">A specific ApplicationId, regular expression, or * for any application.</param>
        public ApplicationAttribute(string applicationId)
        {

        }
    }

    /// <summary>
    /// An application expression attribute, which is determined by the type.
    /// </summary>
    /// <typeparamref name="T">The type of the application.</typeparamref/>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class WebExApplicationAttribute<T> : Attribute, IWebExApplicationAttribute where T : class, IApplication
    {

    }
}
