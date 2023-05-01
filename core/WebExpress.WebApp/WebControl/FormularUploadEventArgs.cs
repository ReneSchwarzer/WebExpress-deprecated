using WebExpress.WebMessage;
using WebExpress.UI.WebControl;

namespace WebExpress.WebApp.WebControl
{
    public class FormularUploadEventArgs : FormularEventArgs
    {
        /// <summary>
        /// Liefert oder setzt die Datei
        /// </summary>
        public ParameterFile File { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="args">Die Eventargumente</param>
        public FormularUploadEventArgs(FormularEventArgs args)
        {
            Context = args.Context;
        }
    }
}
