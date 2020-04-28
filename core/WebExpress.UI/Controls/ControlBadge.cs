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
        /// Die Hintergrundfarbe
        /// </summary>
        public new PropertyColorBackgroundBadge BackgroundColor
        {
            get => (PropertyColorBackgroundBadge)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass(), () => value?.ToStyle());
        }

        /// <summary>
        /// Liefert oder setzt das Layout
        /// </summary>
        private TypeColorBackgroundBadge Layout
        {
            get => (TypeColorBackgroundBadge)GetProperty(TypeColorBackgroundBadge.Default);
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
        public ControlBadge(IPage page, string id, string value, TypeColorBackgroundBadge layout = TypeColorBackgroundBadge.Default)
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
        public ControlBadge(IPage page, string id, int value, TypeColorBackgroundBadge layout = TypeColorBackgroundBadge.Default)
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
