using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
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
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlTableColumn(IPage page, string id)
            : base(page, id)
        {

        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            switch (Layout)
            {
                case TypesLayoutTableRow.Primary:
                    Classes.Add("table-primary");
                    break;
                case TypesLayoutTableRow.Secondary:
                    Classes.Add("table-secondary");
                    break;
                case TypesLayoutTableRow.Success:
                    Classes.Add("table-success");
                    break;
                case TypesLayoutTableRow.Info:
                    Classes.Add("table-info");
                    break;
                case TypesLayoutTableRow.Warning:
                    Classes.Add("table-warning");
                    break;
                case TypesLayoutTableRow.Danger:
                    Classes.Add("table-danger");
                    break;
                case TypesLayoutTableRow.Light:
                    Classes.Add("table-light");
                    break;
                case TypesLayoutTableRow.Dark:
                    Classes.Add("table-dark");
                    break;
            }

            var html = new HtmlElementTextContentDiv()
            {
                ID = ID,
                Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };

            if (Icon != null && Icon.HasIcon)
            {
                html.Elements.Add(new ControlIcon(Page)
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
                }.ToHtml());
            }
            
            if (!string.IsNullOrWhiteSpace(Text))
            {
                html.Elements.Add(new HtmlText(Text));
            }

            return new HtmlElementTableTh(html)
            {
                ID = ID,
                Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };
        }
    }
}
