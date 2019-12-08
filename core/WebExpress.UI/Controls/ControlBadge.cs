using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Pages;
using WebServer.Html;

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
            Class = "";
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            var classes = new List<string>
            {
                Class,
                "badge"
            };

            var styles = new List<string>
            {
                Style
            };

            switch (Layout)
            {
                case TypesLayoutBadge.Primary:
                    classes.Add("badge-primary");
                    break;
                case TypesLayoutBadge.Success:
                    classes.Add("badge-success");
                    break;
                case TypesLayoutBadge.Info:
                    classes.Add("badge-info");
                    break;
                case TypesLayoutBadge.Warning:
                    classes.Add("badge-warning");
                    break;
                case TypesLayoutBadge.Danger:
                    classes.Add("badge-danger");
                    break;
                case TypesLayoutBadge.Light:
                    classes.Add("badge-light");
                    break;
                case TypesLayoutBadge.Dark:
                    classes.Add("badge-dark");
                    break;
                case TypesLayoutBadge.Color:
                    classes.Add("badge-dark");
                    styles.Add("background-color: " + BackgroundColor + ";");
                    break;
            }

            if (Pill)
            {
                classes.Add("badge-pill");
            }

            return new HtmlElementSpan(new HtmlText(Value.ToString()))
            {
                ID = ID,
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join(" ", styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };
        }
    }
}
