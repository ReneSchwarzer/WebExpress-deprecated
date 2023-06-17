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
        /// Returns or sets the layout.
        /// </summary>
        public TypesLayoutTableRow Layout { get; set; }

        /// <summary>
        /// Returns or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Returns or sets the icon.
        /// </summary>
        public PropertyIcon Icon { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlTableColumn(string id)
            : base(id)
        {

        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
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
                Id = Id,
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
                Id = Id,
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };
        }
    }
}
