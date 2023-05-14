using WebExpress.Html;
using WebExpress.WebPage;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.UI.WebControl
{
    public class ControlText : Control
    {
        /// <summary>
        /// Liefert oder setzt die Farbe des Textes
        /// </summary>
        public new virtual PropertyColorText TextColor
        {
            get => (PropertyColorText)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass(), () => value?.ToStyle());
        }

        /// <summary>
        /// Liefert oder setzt das Format des Textes
        /// </summary>
        public TypeFormatText Format { get; set; }

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
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlText()
            : base(null)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlText(string id)
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
            var text = I18N(context.Culture, Text);
            HtmlElement html;

            switch (Format)
            {
                case TypeFormatText.Paragraph:
                    html = new HtmlElementTextContentP(text)
                    {
                        ID = Id,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeFormatText.Italic:
                    html = new HtmlElementTextSemanticsI(text)
                    {
                        ID = Id,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeFormatText.Bold:
                    html = new HtmlElementTextSemanticsB(text)
                    {
                        ID = Id,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeFormatText.Underline:
                    html = new HtmlElementTextSemanticsU(text)
                    {
                        ID = Id,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeFormatText.StruckOut:
                    html = new HtmlElementTextSemanticsS(text)
                    {
                        ID = Id,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeFormatText.Cite:
                    html = new HtmlElementTextSemanticsCite(text)
                    {
                        ID = Id,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeFormatText.H1:
                    html = new HtmlElementSectionH1(text)
                    {
                        ID = Id,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeFormatText.H2:
                    html = new HtmlElementSectionH2(text)
                    {
                        ID = Id,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeFormatText.H3:
                    html = new HtmlElementSectionH3(text)
                    {
                        ID = Id,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeFormatText.H4:
                    html = new HtmlElementSectionH4(text)
                    {
                        ID = Id,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeFormatText.H5:
                    html = new HtmlElementSectionH5(text)
                    {
                        ID = Id,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeFormatText.H6:
                    html = new HtmlElementSectionH6(text)
                    {
                        ID = Id,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeFormatText.Span:
                    html = new HtmlElementTextSemanticsSpan(new HtmlText(text))
                    {
                        ID = Id,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeFormatText.Small:
                    html = new HtmlElementTextSemanticsSmall(new HtmlText(text))
                    {
                        ID = Id,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeFormatText.Strong:
                    html = new HtmlElementTextSemanticsStrong(new HtmlText(text))
                    {
                        ID = Id,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeFormatText.Center:
                    html = new HtmlElementTextContentDiv(new HtmlText(text))
                    {
                        ID = Id,
                        Class = Css.Concatenate("text-center", GetClasses()),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeFormatText.Code:
                    html = new HtmlElementTextSemanticsCode(new HtmlText(text))
                    {
                        ID = Id,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeFormatText.Output:
                    html = new HtmlElementTextSemanticsSamp(new HtmlText(text))
                    {
                        ID = Id,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeFormatText.Time:
                    html = new HtmlElementTextSemanticsTime(new HtmlText(text))
                    {
                        ID = Id,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeFormatText.Mark:
                    html = new HtmlElementTextSemanticsMark(new HtmlText(text))
                    {
                        ID = Id,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeFormatText.Highlight:
                    html = new HtmlElementTextSemanticsEm(new HtmlText(text))
                    {
                        ID = Id,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeFormatText.Definition:
                    html = new HtmlElementTextSemanticsDfn(new HtmlText(text))
                    {
                        ID = Id,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeFormatText.Abbreviation:
                    html = new HtmlElementTextSemanticsAbbr(new HtmlText(text))
                    {
                        ID = Id,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeFormatText.Input:
                    html = new HtmlElementTextSemanticsKdb(new HtmlText(text))
                    {
                        ID = Id,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeFormatText.Blockquote:
                    html = new HtmlElementTextContentBlockquote(new HtmlText(text))
                    {
                        ID = Id,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeFormatText.Figcaption:
                    html = new HtmlElementTextContentFigcaption(new HtmlText(text))
                    {
                        ID = Id,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeFormatText.Preformatted:
                    html = new HtmlElementTextContentPre(new HtmlText(text))
                    {
                        ID = Id,
                        Class = GetClasses(),
                        Style = GetStyles(),
                        Role = Role
                    };
                    break;
                case TypeFormatText.Markdown:
                    return Markdown.Markdown.Transform(text);
                default:
                    html = new HtmlElementTextContentDiv(new HtmlText(text))
                    {
                        ID = Id,
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
