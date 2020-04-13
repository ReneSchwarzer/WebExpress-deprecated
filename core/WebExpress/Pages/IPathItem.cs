using System;
using System.Collections.Generic;
using System.Text;

namespace WebExpress.Pages
{
    public interface IPathItem
    {
        /// <summary>
        /// Der Name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Das Pfadfragment
        /// </summary>
        string Fragment { get; set; }

        /// <summary>
        /// Das Etikett
        /// </summary>
        string Tag { get; set; }
    }
}
