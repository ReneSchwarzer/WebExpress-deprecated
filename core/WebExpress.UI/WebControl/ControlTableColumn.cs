using System.Linq;
using WebExpress.Html;
using WebExpress.WebPage;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Zeile der Tabelle
    /// </summary>
    public class ControlTableColumn : Control
    {
        /// <summary>
        /// Liefert oder setzt das Layout
        /// </summary>
        public TypesLayoutTableRow Layout { get; set; }

        /// <summary>
        /// Liefert oder setzt den Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Liefert oder setzt das Icon
        /// </summary>
        public PropertyIcon Icon { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlTableColumn(string id)
            : base(id)
        {

        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var classes = Classes.ToList();

            switch (Layout)
            {
                case TypesLayoutTableRow.Primary:
                    classes.Add("table-primary");
                    break;
                case TypesLayoutTableRow.Secondary:
                    classes.Add("table-secondary");
                    break;
                case TypesLayoutTableRow.Success:
                    classes.Add("table-success");
                    break;
                case TypesLayoutTableRow.Info:
                    classes.Add("table-info");
                    break;
                case TypesLayoutTableRow.Warning:
                    classes.Add("table-warning");
                    break;
                case TypesLayoutTableRow.Danger:
                    classes.Add("table-danger");
                    break;
                case TypesLayoutTableRow.Light:
                    classes.Add("table-light");
                    break;
                case TypesLayoutTableRow.Dark:
                    classes.Add("table-dark");
                    break;
            }

            var html = new HtmlElementTextContentDiv()
            {
                ID = Id,
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };

            if (Icon != null && Icon.HasIcon)
            {
                html.Elements.Add(new ControlIcon()
                {
                    Icon = Icon,
                    Margin = !string.IsNullOrWhiteSpace(Text) ? new PropertySpacingMargin
                   (
                       PropertySpacing.Space.None,
                       PropertySpacing.Space.Two,
                       PropertySpacing.Space.None,
                       PropertySpacing.Space.None
                   ) : new PropertySpacingMargin(PropertySpacing.Space.None),
                    VerticalAlignment = Icon.IsUserIcon ? TypeVerticalAlignment.TextBottom : TypeVerticalAlignment.Default
                }.Render(context));
            }

            if (!string.IsNullOrWhiteSpace(Text))
            {
                html.Elements.Add(new HtmlText(I18N(context.Culture, Text)));
            }

            return new HtmlElementTableTh(html)
            {
                ID = Id,
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };
        }
    }
}
