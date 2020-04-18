using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;

namespace WebExpress.UI.Controls
{
    public class ControlFormularItemButton : ControlFormularItem
    {
        /// <summary>
        /// Liefert oder setzt das Layout
        /// </summary>
        public TypesLayoutButton Layout { get; set; }

        /// <summary>
        /// Liefert oder setzt die Größe
        /// </summary>
        public TypesSize Size { get; set; }

        /// <summary>
        /// Liefert oder setzt doe Outline-Eigenschaft
        /// </summary>
        public bool Outline { get; set; }

        /// <summary>
        /// Liefert oder setzt ob die Schaltfläche die volle Breite einnehmen soll
        /// </summary>
        public bool Block { get; set; }

        /// <summary>
        /// Liefert oder setzt ob die Schaltfläche deaktiviert ist
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        public List<Control> Content { get; private set; }

        /// <summary>
        /// Event wird ausgelöst, wenn die Schlatfläche geklickt wurde
        /// </summary>
        public EventHandler Click;

        /// <summary>
        /// Liefert oder setzt den Text der TextBox
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Liefert oder setzt den Typ
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Liefert oder setzt den Wert
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Liefert oder setzt das Icon
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="formular">Das zugehörige Formular</param>
        /// <param name="id">Die ID</param>
        public ControlFormularItemButton(ControlPanelFormular formular, string id = null)
            : base(formular, id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Disabled = false;
            Size = TypesSize.Default;
            Content = new List<Control>();
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            if (Page.HasParam(Name))
            {
                Value = Page.GetParam(Name);

                var value = Page.GetParam(Name);

                if (!string.IsNullOrWhiteSpace(Value) && value == Value)
                {
                    OnClickEvent(new EventArgs());
                }
            }

            var classes = new List<string>
            {
                Class,
                "btn"
            };

            if (Outline)
            {
                switch (Layout)
                {
                    case TypesLayoutButton.Primary:
                        classes.Add("btn-outline-primary");
                        break;
                    case TypesLayoutButton.Success:
                        classes.Add("btn-outline-success");
                        break;
                    case TypesLayoutButton.Info:
                        classes.Add("btn-outline-info");
                        break;
                    case TypesLayoutButton.Warning:
                        classes.Add("btn-outline-warning");
                        break;
                    case TypesLayoutButton.Danger:
                        classes.Add("btn-outline-danger");
                        break;
                    case TypesLayoutButton.Dark:
                        classes.Add("btn-outline-dark");
                        break;
                }
            }
            else
            {
                switch (Layout)
                {
                    case TypesLayoutButton.Primary:
                        classes.Add("btn-primary");
                        break;
                    case TypesLayoutButton.Success:
                        classes.Add("btn-success");
                        break;
                    case TypesLayoutButton.Info:
                        classes.Add("btn-info");
                        break;
                    case TypesLayoutButton.Warning:
                        classes.Add("btn-warning");
                        break;
                    case TypesLayoutButton.Danger:
                        classes.Add("btn-danger");
                        break;
                    case TypesLayoutButton.Light:
                        classes.Add("btn-light");
                        break;
                    case TypesLayoutButton.Dark:
                        classes.Add("btn-dark");
                        break;
                }
            }

            switch (Size)
            {
                case TypesSize.Large:
                    classes.Add("btn-lg");
                    break;
                case TypesSize.Small:
                    classes.Add("btn-sm");
                    break;
            }

            switch (HorizontalAlignment)
            {
                case TypesHorizontalAlignment.Left:
                    classes.Add("float-left");
                    break;
                case TypesHorizontalAlignment.Right:
                    classes.Add("float-right");
                    break;
            }

            if (Block)
            {
                classes.Add("btn-block");
            }

            var html = new HtmlElementButton()
            {
                Name = Name,
                Type = Type,
                Value = Value,
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role,
                Disabled = Disabled
            };

            if (!string.IsNullOrWhiteSpace(Icon) && !string.IsNullOrWhiteSpace(Text))
            {
                html.Elements.Add(new HtmlElementSpan() { Class = Icon });

                html.Elements.Add(new HtmlNbsp());
                html.Elements.Add(new HtmlNbsp());
                html.Elements.Add(new HtmlNbsp());
            }
            else if (!string.IsNullOrWhiteSpace(Icon) && string.IsNullOrWhiteSpace(Text))
            {
                html.AddClass(Icon);
            }

            if (!string.IsNullOrWhiteSpace(Text))
            {
                html.Elements.Add(new HtmlText(Text));
            }

            if (Content.Count > 0)
            {
                html.Elements.AddRange(Content.Select(x => x.ToHtml()));
            }

            return html;
        }

        /// <summary>
        /// Löst das Click-Event aus
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnClickEvent(EventArgs e)
        {
            Click?.Invoke(this, e);
        }
    }
}
