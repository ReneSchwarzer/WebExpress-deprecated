using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Message;
using WebExpress.Uri;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlLink : Control, IControlLink
    {
        /// <summary>
        /// Bestimmt ob der Link aktiv ist oder nicht
        /// </summary>
        public TypeActive Active
        {
            get => (TypeActive)GetProperty(TypeActive.None);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Bestimmt ob der Link unterstrichen ist oder nicht
        /// </summary>
        public TypeTextDecoration Decoration
        {
            get => (TypeTextDecoration)GetProperty(TypeTextDecoration.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setzt den Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Liefert oder setzt den ToolTip
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Liefert oder setzt die Ziel-Uri
        /// </summary>
        public IUri Uri { get; set; }

        /// <summary>
        /// Liefert oder setzt das Ziel
        /// </summary>
        public TypeTarget Target { get; set; }

        /// <summary>
        /// Liefert oder setzt einen modalen Dialag
        /// </summary>
        public ControlModal Modal { get; set; }

        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        public List<Control> Content { get; private set; }

        /// <summary>
        /// Liefert oder setzt die für den Link gültigen Parameter
        /// </summary>
        public List<Parameter> Params { get; set; }

        /// <summary>
        /// Liefert oder setzt das Icon
        /// </summary>
        public PropertyIcon Icon { get; set; }

        /// <summary>
        /// Liefert oder setzt einen Tooltiptext
        /// </summary>
        public string Tooltip { get; set; }

        /// <summary>
        /// Die vertikale Ausrichtung
        /// </summary>
        public TypeVerticalAlignment VerticalAlignment
        {
            get => (TypeVerticalAlignment)GetProperty(TypeVerticalAlignment.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setzt die Größe
        /// </summary>
        public PropertySizeText Size
        {
            get => (PropertySizeText)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass(), () => value?.ToStyle());
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlLink(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="text">Der Inhalt</param>
        public ControlLink(string id, string text)
            : this(id)
        {
            Text = text;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="content">Der Inhalt</param>
        public ControlLink(params Control[] content)
            : this((string)null)
        {
            Content.AddRange(content);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="content">Der Inhalt</param>
        public ControlLink(string id, params Control[] content)
            : this(id)
        {
            Content.AddRange(content);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="content">Der Inhalt</param>
        public ControlLink(string id, List<Control> content)
            : base(id)
        {
            Content = content;
            Params = new List<Parameter>();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Content = new List<Control>();
            Params = new List<Parameter>();
        }

        /// <summary>
        /// Liefert alle lokalen und temporären Parameter
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Die Parameter</returns>
        public string GetParams(IPage page)
        {
            var dict = new Dictionary<string, Parameter>();


            // Übernahme der Parameter des Link
            if (Params != null)
            {
                foreach (var v in Params)
                {
                    if (v.Scope == ParameterScope.Parameter)
                    {
                        if (!dict.ContainsKey(v.Key.ToLower()))
                        {
                            dict.Add(v.Key.ToLower(), v);
                        }
                        else
                        {
                            dict[v.Key.ToLower()] = v;
                        }
                    }
                }
            }

            return string.Join("&amp;", from x in dict where !string.IsNullOrWhiteSpace(x.Value.Value) select x.Value.ToString());
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var param = GetParams(context?.Page);

            var html = new HtmlElementTextSemanticsA(from x in Content select x.Render(context))
            {
                ID = ID,
                Class = GetClasses(),
                Style = GetStyles(),
                Role = Role,
                Href = Uri?.ToString() + (param.Length > 0 ? "?" + param : string.Empty),
                Target = Target,
                Title = Title,
                OnClick = OnClick
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
                html.Elements.Add(new HtmlText(Text));
            }

            if (Modal != null)
            {
                html.AddUserAttribute("data-toggle", "modal");
                html.AddUserAttribute("data-target", "#" + Modal.ID);

                return new HtmlList(html, Modal.Render(context));
            }

            if (!string.IsNullOrWhiteSpace(Tooltip))
            {
                html.AddUserAttribute("data-toggle", "tooltip");
            }

            return html;
        }
    }
}
