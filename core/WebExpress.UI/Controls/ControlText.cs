using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlText : Control
    {
        /// <summary>
        /// Liefert oder setzt das Format des Textes
        /// </summary>
        public TypesTextFormat Format { get; set; }

        /// <summary>
        /// Liefert oder setzt die Größe des Textes
        /// </summary>
        public TypesSize Size
        {
            get => (TypesSize)GetProperty(TypesSize.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setzt die Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Liefert oder setzt einen Tooltiptext
        /// </summary>
        public string Tooltip { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlText(IPage page, string id = null)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="value">Der Text</param>
        public ControlText(IPage page, string id, int value)
            : base(page, id)
        {
            Text = value.ToString();

            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            var html = null as HtmlElement;

            switch (Format)
            {
                case TypesTextFormat.Paragraph:
                    html = new HtmlElementTextContentP(Text)
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypesTextFormat.Italic:
                    html = new HtmlElementTextSemanticsI(Text)
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypesTextFormat.Bold:
                    html = new HtmlElementTextSemanticsB(Text)
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypesTextFormat.H1:
                    html = new HtmlElementSectionH1(Text)
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypesTextFormat.H2:
                    html = new HtmlElementSectionH2(Text)
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypesTextFormat.H3:
                    html = new HtmlElementSectionH3(Text)
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypesTextFormat.H4:
                    html = new HtmlElementSectionH4(Text)
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypesTextFormat.H5:
                    html = new HtmlElementSectionH5(Text)
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypesTextFormat.H6:
                    html = new HtmlElementSectionH6(Text)
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypesTextFormat.Span:
                    html = new HtmlElementTextSemanticsSpan(new HtmlText(Text))
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypesTextFormat.Small:
                    html = new HtmlElementTextSemanticsSmall(new HtmlText(Text))
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypesTextFormat.Center:
                    html = new HtmlElementTextContentDiv(new HtmlText(Text))
                    {
                        ID = ID,
                        Class = Css.Concatenate("text-center",  GetClasses()),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypesTextFormat.Code:
                    html = new HtmlElementTextSemanticsCode(new HtmlText(Text))
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                default:
                    html = new HtmlElementTextContentDiv(new HtmlText(Text))
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
            }

            if (!string.IsNullOrWhiteSpace(Tooltip))
            {
                html.AddUserAttribute("data-toggle", "tooltip");
                html.AddUserAttribute("title", Tooltip);
            }

            return html;
        }
    }
}
