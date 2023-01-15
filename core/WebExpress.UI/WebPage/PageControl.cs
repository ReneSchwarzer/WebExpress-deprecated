using System.Collections.Generic;
using WebExpress.Uri;
using WebExpress.WebComponent;
using WebExpress.WebModule;
using WebExpress.WebPage;
using WebExpress.WebResource;

namespace WebExpress.UI.WebPage
{
    /// <summary>
    /// A (web) page, which is built from controls.
    /// </summary>
    public abstract class PageControl<T> : Page<T> where T : RenderContextControl, new()
    {
        /// <summary>
        /// Returns the links to the JavaScript files to be used, which are inserted in the header.
        /// </summary>
        protected ICollection<IUri> HeaderScriptLinks { get; } = new List<IUri>();

        /// <summary>
        /// Returns the links to the css files to use.
        /// </summary>
        protected ICollection<IUri> CssLinks { get; } = new List<IUri>();

        /// <summary>
        /// Returns the meta information.
        /// </summary>
        protected List<KeyValuePair<string, string>> Meta { get; } = new List<KeyValuePair<string, string>>();

        /// <summary>
        /// Constructor
        /// </summary>
        public PageControl()
        {

        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context of the resource.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            var module = ComponentManager.ModuleManager.GetModule(ApplicationContext, "webexpress.ui");
            if (module != null)
            {
                var contextPath = module.GetContextPath(ApplicationContext);
                CssLinks.Add(new UriResource(contextPath, new UriRelative("/assets/css/fontawesome.min.css")));
                CssLinks.Add(new UriResource(contextPath, new UriRelative("/assets/css/bootstrap.min.css")));
                CssLinks.Add(new UriResource(contextPath, new UriRelative("/assets/css/solid.css")));
                CssLinks.Add(new UriResource(contextPath, new UriRelative("/assets/css/summernote-bs5.min.css")));
                CssLinks.Add(new UriResource(contextPath, new UriRelative("/assets/css/webexpress.ui.css")));
                CssLinks.Add(new UriResource(contextPath, new UriRelative("/assets/css/webexpress.ui.expand.css")));
                CssLinks.Add(new UriResource(contextPath, new UriRelative("/assets/css/webexpress.ui.form.css")));
                CssLinks.Add(new UriResource(contextPath, new UriRelative("/assets/css/webexpress.ui.modalformular.css")));
                CssLinks.Add(new UriResource(contextPath, new UriRelative("/assets/css/webexpress.ui.modalpage.css")));
                CssLinks.Add(new UriResource(contextPath, new UriRelative("/assets/css/webexpress.ui.more.css")));
                CssLinks.Add(new UriResource(contextPath, new UriRelative("/assets/css/webexpress.ui.move.css")));
                CssLinks.Add(new UriResource(contextPath, new UriRelative("/assets/css/webexpress.ui.pagination.css")));
                CssLinks.Add(new UriResource(contextPath, new UriRelative("/assets/css/webexpress.ui.search.css")));
                CssLinks.Add(new UriResource(contextPath, new UriRelative("/assets/css/webexpress.ui.selection.css")));
                CssLinks.Add(new UriResource(contextPath, new UriRelative("/assets/css/webexpress.ui.table.css")));

                HeaderScriptLinks.Add(new UriResource(contextPath, new UriRelative("/assets/js/jquery-3.6.0.min.js")));
                HeaderScriptLinks.Add(new UriResource(contextPath, new UriRelative("/assets/js/popper.min.js")));
                HeaderScriptLinks.Add(new UriResource(contextPath, new UriRelative("/assets/js/bootstrap.min.js")));
                HeaderScriptLinks.Add(new UriResource(contextPath, new UriRelative("/assets/js/summernote-bs5.min.js")));
                HeaderScriptLinks.Add(new UriResource(contextPath, new UriRelative("/assets/js/webexpress.ui.js")));
                HeaderScriptLinks.Add(new UriResource(contextPath, new UriRelative("/assets/js/webexpress.ui.expand.js")));
                HeaderScriptLinks.Add(new UriResource(contextPath, new UriRelative("/assets/js/webexpress.ui.modalformular.js")));
                HeaderScriptLinks.Add(new UriResource(contextPath, new UriRelative("/assets/js/webexpress.ui.modalpage.js")));
                HeaderScriptLinks.Add(new UriResource(contextPath, new UriRelative("/assets/js/webexpress.ui.more.js")));
                HeaderScriptLinks.Add(new UriResource(contextPath, new UriRelative("/assets/js/webexpress.ui.move.js")));
                HeaderScriptLinks.Add(new UriResource(contextPath, new UriRelative("/assets/js/webexpress.ui.pagination.js")));
                HeaderScriptLinks.Add(new UriResource(contextPath, new UriRelative("/assets/js/webexpress.ui.search.js")));
                HeaderScriptLinks.Add(new UriResource(contextPath, new UriRelative("/assets/js/webexpress.ui.selection.js")));
                HeaderScriptLinks.Add(new UriResource(contextPath, new UriRelative("/assets/js/webexpress.ui.table.js")));
            }

            Meta.Add(new KeyValuePair<string, string>("charset", "UTF-8"));
            Meta.Add(new KeyValuePair<string, string>("viewport", "width=device-width, initial-scale=1"));
        }

        /// <summary>
        /// Processing of the page.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(T context)
        {
            context.VisualTree.CssLinks.AddRange(CssLinks);
            context.VisualTree.HeaderScriptLinks.AddRange(HeaderScriptLinks);
            context.VisualTree.Meta.AddRange(Meta);
        }
    }
}
