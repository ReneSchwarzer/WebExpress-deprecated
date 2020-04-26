using System;
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
        private TypesLayoutBadge Layout
        {
            get => (TypesLayoutBadge)GetProperty(TypesLayoutBadge.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setzt ob abgerundete Ecken verwendet werden soll
        /// </summary>
        public TypesBadgePill Pill
        {
            get => (TypesBadgePill)GetProperty(TypesBadgePill.None);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setzt die Ziel-Uri
        /// </summary>
        public IUri Uri { get; set; }

        /// <summary>
        /// Liefert oder setzt den Wert
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlBadge(IPage page, string id = null)
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
            Value = value;
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
            Value = value.ToString();
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
            if (Enum.TryParse(typeof(TypesLayoutBadge), BackgroundColor.Value.ToString(), out var result))
            {
                Layout = (TypesLayoutBadge)result;

                // Hintergrundfarbe entfernen
                var bgColor = BackgroundColor;
                BackgroundColor = new PropertyColorBackground(TypesBackgroundColor.Default);

                var html = null as IHtmlNode;

                if (Uri != null)
                {
                    html = new HtmlElementTextSemanticsA(new HtmlText(Value.ToString()))
                    {
                        ID = ID,
                        Class = Css.Concatenate("badge", GetClasses()),
                        Style = GetStyles(),
                        Href = Uri.ToString(),
                        Role = Role
                    };
                }
                else
                {
                    html = new HtmlElementTextSemanticsSpan(new HtmlText(Value.ToString()))
                    {
                        ID = ID,
                        Class = Css.Concatenate("badge", GetClasses()),
                        Style = GetStyles(),
                        Role = Role
                    };
                }

                // Hintergrundfarbe wiederherstellen
                BackgroundColor = bgColor;

                return html;
            }

            if (Uri != null)
            {
                return new HtmlElementTextSemanticsA(new HtmlText(Value.ToString()))
                {
                    ID = ID,
                    Class = Css.Concatenate("badge", GetClasses()),
                    Style = GetStyles(),
                    Href = Uri.ToString(),
                    Role = Role
                };
            }
            
            return new HtmlElementTextSemanticsSpan(new HtmlText(Value.ToString()))
            {
                ID = ID,
                Class = Css.Concatenate("badge", GetClasses()),
                Style = GetStyles(),
                Role = Role
            };
        }
    }
}
