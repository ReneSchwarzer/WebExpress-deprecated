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
        public TypeSizeButton Size { get; set; }

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
            Size = TypeSizeButton.Default;
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

            Classes.Add("btn");
            Classes.Add(Layout.ToClass(Outline));
            Classes.Add(Size.ToClass());
            Classes.Add(HorizontalAlignment.ToClass());

            if (Block)
            {
                Classes.Add("btn-block");
            }

            var html = new HtmlElementFieldButton()
            {
                Name = Name,
                Type = Type,
                Value = Value,
                Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role,
                Disabled = Disabled
            };

            if (!string.IsNullOrWhiteSpace(Icon) && !string.IsNullOrWhiteSpace(Text))
            {
                html.Elements.Add(new HtmlElementTextSemanticsSpan() { Class = Icon });

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
