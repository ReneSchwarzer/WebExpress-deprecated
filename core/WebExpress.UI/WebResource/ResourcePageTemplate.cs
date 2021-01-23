using WebExpress.Html;
using WebExpress.Module;
using WebExpress.Uri;

namespace WebExpress.UI.WebResource
{
    /// <summary>
    /// Seite, die aus einem vertikal angeordneten Kopf-, Inhalt- und Fuss-Bereich besteht
    /// </summary>
    public abstract class ResourcePageTemplate : ResourcePageBlank
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ResourcePageTemplate()
        {
            var module = ModuleManager.GetModule("webexpress");
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
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();
        }
    }
}
