using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Numerischer Indikator
    /// </summary>
    public class ControlBadge : Control
    {
        /// <summary>
        /// Liefert oder setzt das Layout
        /// </summary>
        public TypesLayoutBadge Layout { get; set; }

        /// <summary>
        /// Liefert oder setzt die Hintergrundfarbe
        /// </summary>
        public string BackgroundColor { get; set; }

        /// <summary>
        /// Liefert oder setzt ob abgerundete Ecken verwendet werden soll
        /// </summary>
        public bool Pill { get; set; }

        /// <summary>
        /// Liefert oder setzt den Wert
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlBadge(IPage page, string id)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="text">Der Text</param>
        /// <param name="layout">Das Layout</param>
        public ControlBadge(IPage page, string id, string value, TypesLayoutBadge layout = TypesLayoutBadge.Default)
            : base(page, id)
        {
            Value = Convert.ToInt32(value);
            Layout = layout;

            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="text">Der Text</param>
        /// <param name="layout">Das Layout</param>
        public ControlBadge(IPage page, string id, int value, TypesLayoutBadge layout = TypesLayoutBadge.Default)
            : base(page, id)
        {
            Value = value;
            Layout = layout;

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
            Classes.Add("badge");
            Classes.Add(Layout.ToClass());

            switch (Layout)
            {
                case TypesLayoutBadge.Color:
                    Styles.Add("background-color: " + BackgroundColor + ";");
                    break;
            }

            if (Pill)
            {
                Classes.Add("badge-pill");
            }

            return new HtmlElementTextSemanticsSpan(new HtmlText(Value.ToString()))
            {
                ID = ID,
                Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };
        }
    }
}
