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
        public TypeTextFormat Format { get; set; }

        /// <summary>
        /// Liefert oder setzt die Größe des Textes
        /// </summary>
        public PropertySizeText Size
        {
            get => (PropertySizeText)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass(), () => value?.ToStyle());
        }

        /// <summary>
        /// Liefert oder setzt die Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Liefert oder setzt einen Tooltiptext
        /// </summary>
        public string Title { get; set; }

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
                case TypeTextFormat.Paragraph:
                    html = new HtmlElementTextContentP(Text)
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeTextFormat.Italic:
                    html = new HtmlElementTextSemanticsI(Text)
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeTextFormat.Bold:
                    html = new HtmlElementTextSemanticsB(Text)
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeTextFormat.Underline:
                    html = new HtmlElementTextSemanticsU(Text)
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeTextFormat.StruckOut:
                    html = new HtmlElementTextSemanticsS(Text)
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeTextFormat.Cite:
                    html = new HtmlElementTextSemanticsCite(Text)
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeTextFormat.H1:
                    html = new HtmlElementSectionH1(Text)
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeTextFormat.H2:
                    html = new HtmlElementSectionH2(Text)
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeTextFormat.H3:
                    html = new HtmlElementSectionH3(Text)
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeTextFormat.H4:
                    html = new HtmlElementSectionH4(Text)
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeTextFormat.H5:
                    html = new HtmlElementSectionH5(Text)
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeTextFormat.H6:
                    html = new HtmlElementSectionH6(Text)
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeTextFormat.Span:
                    html = new HtmlElementTextSemanticsSpan(new HtmlText(Text))
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeTextFormat.Small:
                    html = new HtmlElementTextSemanticsSmall(new HtmlText(Text))
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break; 
                case TypeTextFormat.Strong:
                    html = new HtmlElementTextSemanticsStrong(new HtmlText(Text))
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeTextFormat.Center:
                    html = new HtmlElementTextContentDiv(new HtmlText(Text))
                    {
                        ID = ID,
                        Class = Css.Concatenate("text-center",  GetClasses()),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeTextFormat.Code:
                    html = new HtmlElementTextSemanticsCode(new HtmlText(Text))
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeTextFormat.Output:
                    html = new HtmlElementTextSemanticsSamp(new HtmlText(Text))
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeTextFormat.Time:
                    html = new HtmlElementTextSemanticsTime(new HtmlText(Text))
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeTextFormat.Mark:
                    html = new HtmlElementTextSemanticsMark(new HtmlText(Text))
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeTextFormat.Highlight:
                    html = new HtmlElementTextSemanticsEm(new HtmlText(Text))
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeTextFormat.Definition:
                    html = new HtmlElementTextSemanticsDfn(new HtmlText(Text))
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeTextFormat.Abbreviation:
                    html = new HtmlElementTextSemanticsAbbr(new HtmlText(Text))
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeTextFormat.Input:
                    html = new HtmlElementTextSemanticsKdb(new HtmlText(Text))
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeTextFormat.Blockquote:
                    html = new HtmlElementTextContentBlockquote(new HtmlText(Text))
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeTextFormat.Figcaption:
                    html = new HtmlElementTextContentFigcaption(new HtmlText(Text))
                    {
                        ID = ID,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeTextFormat.Preformatted:
                    html = new HtmlElementTextContentPre(new HtmlText(Text))
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

            if (!string.IsNullOrWhiteSpace(Title))
            {
                html.AddUserAttribute("data-toggle", "tooltip");
                html.AddUserAttribute("title", Title);
            }

            return html;
        }
    }
}
