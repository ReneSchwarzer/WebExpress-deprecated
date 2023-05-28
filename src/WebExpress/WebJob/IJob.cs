﻿using WebExpress.Internationalization;

namespace WebExpress.WebJob
{
    /// <summary>
    /// A task that can be performed cyclically.
    /// </summary>
    public interface IJob : II18N
    {
        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context in which the job is executed.</param>
        public void Initialization(JobContext context);

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        public void Process();

    }
}
