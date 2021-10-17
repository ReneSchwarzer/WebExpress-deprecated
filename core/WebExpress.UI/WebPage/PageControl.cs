using System.Collections.Generic;
using WebExpress.Module;
using WebExpress.Uri;
using WebExpress.WebPage;
using WebExpress.WebResource;

namespace WebExpress.UI.WebPage
{
    /// <summary>
    /// Seite, welche aus Steuerelemente (Controls) aufgebaut wird
    /// </summary>
    public abstract class PageControl<T> : Page<T> where T : RenderContextControl, new()
    {
        /// <summary>
        /// Liefert oder setzt die Links auf die zu verwendenden JavaScript-Dateien, welche im Header eingefügt werden
        /// </summary>
        private ICollection<IUri> HeaderScriptLinks { get; } = new List<IUri>();

        /// <summary>
        /// Liefert oder setzt die Links auf die zu verwendenden Css-Dateien
        /// </summary>
        private ICollection<IUri> CssLinks { get; } = new List<IUri>();

        /// <summary>
        /// Liefert die Metainformationen
        /// </summary>
        private List<KeyValuePair<string, string>> Meta { get; } = new List<KeyValuePair<string, string>>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageControl()
        {

        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            var module = ModuleManager.GetModule(Context?.ApplicationID, "webexpress.ui");
            if (module != null)
            {
                CssLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/css/fontawesome.min.css")));
                CssLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/css/bootstrap.min.css")));
                CssLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/css/express.css")));
                CssLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/css/express.form.css")));
                CssLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/css/solid.css")));
                //CssLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/css/summernote-bs4.min.css")));
                //CssLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/css/simpletags.css")));

                HeaderScriptLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/js/jquery-3.5.1.min.js")));
                HeaderScriptLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/js/popper.min.js")));
                HeaderScriptLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/js/bootstrap.min.js")));
                //HeaderScriptLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/js/summernote-bs4.min.js")));
                //HeaderScriptLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/js/simpletags.js")));
            }

            Meta.Add(new KeyValuePair<string, string>("charset", "UTF-8"));
            Meta.Add(new KeyValuePair<string, string>("viewport", "width=device-width, initial-scale=1"));
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(T context)
        {
            context.VisualTree.CssLinks.AddRange(CssLinks);
            context.VisualTree.HeaderScriptLinks.AddRange(HeaderScriptLinks);
            context.VisualTree.Meta.AddRange(Meta);
        }
    }
}
