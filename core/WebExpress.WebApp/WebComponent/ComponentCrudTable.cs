using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebComponent;
using WebExpress.Uri;
using WebExpress.WebModule;
using WebExpress.WebPage;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.WebApp.WebComponent
{
    public abstract class ComponentCrudTable : ComponentControlPanel
    {
        /// <summary>
        /// Liefert oder setzt die Uri zu der REST-API-Schnittstelle
        /// </summary>
        public IUri RestApiUri { get; protected set; }

        /// <summary>
        /// Liefert die Editoren, zum Manipulation der Daten
        /// </summary>
        public ICollection<ComponentCrudTableEditorItem> Editors { get; } = new List<ComponentCrudTableEditorItem>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ComponentCrudTable(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IComponentContext context, IPage page)
        {
            base.Initialization(context, page);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var search = $"Search: {{Placeholder: '{ I18N(context.Culture, "webexpress.webapp:search.placeholder") }'}}";
            var table = $"Table: {{}}";
            var pagination = $"Pagination: {{ CSS: 'justify-content-end' }}";
            var editors = $"Editors: [" + string.Join(",", Editors.Select
            (
                x =>
                {
                    if (x is ComponentCrudTableEditorLinkItem link)
                    {
                        return $"{{Label: '{ I18N(context.Culture, link.Label) }', Icon: '{ link.Icon.ToClass() }', Color: '{ link.Color?.ToClass() }', CSS: 'dropdown-item' }}";
                    }
                    else if (x is ComponentCrudTableEditorSeperatorItem seperator)
                    {
                        return $"{{ CSS: 'dropdown-divider' }}";
                    }

                    return $"{{Label: '', Icon: '', Color: '' }}";
                }
            )) + "]";

            var settings = $"{{{ search }, { table }, { pagination }, { editors }}}";

            context.VisualTree.AddScript
            (
                "webexpress.webapp.usermanagement.user",
                $"var crud = new crudTable('{ RestApiUri }', { settings }); var container = $('#{ ID }'); container.append(crud.getCtrl);"
            );

            return base.Render(context);
        }
    }
}
