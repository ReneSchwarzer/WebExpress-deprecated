using System;

namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Eventargument für Formulare
    /// </summary>
    public class FormularEventArgs : EventArgs
    {
        /// <summary>
        /// Der Formular-Kontext
        /// </summary>
        public RenderContextFormular Context { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public FormularEventArgs()
        {
        }
    }
}
