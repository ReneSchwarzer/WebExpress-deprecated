using System.Collections.Generic;
using System.Linq;
using WebExpress.Messages;
using WebExpress.Pages;
using WebServer.Html;

namespace WebExpress.UI.Controls
{
    public class ControlLink : Control
    {
        /// <summary>
        /// Liefert oder setzt das Format des Textes
        /// </summary>
        public TypesTextColor Color { get; set; }

        /// <summary>
        /// Liefert oder setzt das Format des Textes
        /// </summary>
        public TypesBackgroundColor BackgroundColor { get; set; }

        /// <summary>
        /// Liefert oder setzt den Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Liefert oder setzt den ToolTip
        /// </summary>
        public string Alt { get; set; }

        /// <summary>
        /// Liefert oder setzt die Ziel-Url
        /// </summary>
        public IPath Url { get; set; }

        /// <summary>
        /// Liefert oder setzt das Ziel
        /// </summary>
        public string Target { get; set; }

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
        public Icon Icon { get; set; }

        /// <summary>
        /// Liefert oder setzt einen Tooltiptext
        /// </summary>
        public string Tooltip { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlLink(IPage page, string id = null)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="text">Der Inhalt</param>
        public ControlLink(IPage page, string id, string text)
            : this(page, id)
        {
            Text = text;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="content">Der Inhalt</param>
        public ControlLink(IPage page, string id, params Control[] content)
            : this(page, id)
        {
            Content.AddRange(content);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="content">Der Inhalt</param>
        public ControlLink(IPage page, string id, List<Control> content)
            : base(page, id)
        {
            Content = content;
            Params = new List<Parameter>();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Icon = Icon.None;
            Content = new List<Control>();
            Params = new List<Parameter>();
        }

        /// <summary>
        /// Liefert alle lokalen und temporären Parameter
        /// </summary>
        /// <returns>Die Parameter</returns>
        public string GetParams()
        {
            var dict = new Dictionary<string, Parameter>();

            // Übernahme der Parameter von der Seite
            foreach (var v in Page.Params)
            {
                if (v.Value.Scope == ParameterScope.Global)
                {
                    if (!dict.ContainsKey(v.Key.ToLower()))
                    {
                        dict.Add(v.Key.ToLower(), v.Value);
                    }
                    else
                    {
                        dict[v.Key.ToLower()] = v.Value;
                    }
                }
                else if (string.IsNullOrWhiteSpace(Url?.ToString()))
                {
                    if (!dict.ContainsKey(v.Key.ToLower()))
                    {
                        dict.Add(v.Key.ToLower(), v.Value);
                    }
                    else
                    {
                        dict[v.Key.ToLower()] = v.Value;
                    }
                }
            }

            // Übernahme der Parameter des Link
            if (Params != null)
            {
                foreach (var v in Params)
                {
                    if (v.Scope == ParameterScope.Global)
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
                    else if (string.IsNullOrWhiteSpace(Url?.ToString()))
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
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            var classes = new List<string>
            {
                Class
            };

            switch (Color)
            {
                case TypesTextColor.Muted:
                    classes.Add("text-muted");
                    break;
                case TypesTextColor.Primary:
                    classes.Add("text-primary");
                    break;
                case TypesTextColor.Success:
                    classes.Add("text-success");
                    break;
                case TypesTextColor.Info:
                    classes.Add("text-info");
                    break;
                case TypesTextColor.Warning:
                    classes.Add("text-warning");
                    break;
                case TypesTextColor.Danger:
                    classes.Add("text-danger");
                    break;
                case TypesTextColor.Light:
                    classes.Add("text-light");
                    break;
                case TypesTextColor.Dark:
                    classes.Add("text-dark");
                    break;
                case TypesTextColor.White:
                    classes.Add("text-white");
                    break;
            }

            switch (BackgroundColor)
            {
                case TypesBackgroundColor.Primary:
                    classes.Add("bg-primary");
                    break;
                case TypesBackgroundColor.Secondary:
                    classes.Add("bg-secondary");
                    break;
                case TypesBackgroundColor.Success:
                    classes.Add("bg-success");
                    break;
                case TypesBackgroundColor.Info:
                    classes.Add("bg-info");
                    break;
                case TypesBackgroundColor.Warning:
                    classes.Add("bg-warning");
                    break;
                case TypesBackgroundColor.Danger:
                    classes.Add("bg-danger");
                    break;
                case TypesBackgroundColor.Light:
                    classes.Add("bg-light");
                    break;
                case TypesBackgroundColor.Dark:
                    classes.Add("bg-dark");
                    break;
                case TypesBackgroundColor.White:
                    classes.Add("bg-white");
                    break;
                case TypesBackgroundColor.Transparent:
                    classes.Add("bg-transparent");
                    break;
            }

            var param = GetParams();

            var html = new HtmlElementA(from x in Content select x.ToHtml())
            {
                ID = ID,
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = Style,
                Role = Role,
                Alt = Alt,
                Href = Url.ToString() + (param.Length > 0 ? "?" + param : string.Empty),
                Target = Target,
                OnClick = OnClick
            };

            if (Icon != Icon.None && !string.IsNullOrWhiteSpace(Text))
            {
                html.Elements.Add(new HtmlElementSpan() { Class = Icon.ToClass() });

                html.Elements.Add(new HtmlNbsp());
                html.Elements.Add(new HtmlNbsp());
                html.Elements.Add(new HtmlNbsp());
            }
            else if (Icon != Icon.None && string.IsNullOrWhiteSpace(Text))
            {
                html.AddClass(Icon.ToClass());
            }

            if (!string.IsNullOrWhiteSpace(Text))
            {
                html.Elements.Add(new HtmlText(Text));
            }

            if (Modal != null)
            {
                html.AddUserAttribute("data-toggle", "modal");
                html.AddUserAttribute("data-target", "#" + Modal.ID);

                return new HtmlList(html, Modal.ToHtml());
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
