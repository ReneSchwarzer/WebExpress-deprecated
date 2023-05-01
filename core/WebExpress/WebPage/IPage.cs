﻿using System;
using WebExpress.WebResource;

namespace WebExpress.WebPage
{
    public interface IPage : IResource
    {
        /// <summary>
        /// Returns or sets the page title.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Redirect to another page.
        /// The function throws the RedirectException.
        /// </summary>
        /// <param name="uri">The uri to redirect to.</param>
        void Redirecting(Uri uri);
    }
}
