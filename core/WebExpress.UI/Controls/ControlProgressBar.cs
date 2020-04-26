using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlProgressBar : Control
    {
        /// <summary>
        /// Liefert oder setzt das Format des Fortschrittbalkens
        /// </summary>
        public TypesProgressBarFormat Format
        {
            get => (TypesProgressBarFormat)GetProperty(TypesProgressBarFormat.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setzt die Größe
        /// </summary>
        public TypesSize Size
        {
            get => (TypesSize)GetProperty(TypesSize.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setzt die Farbe des Textes
        /// </summary>
        public PropertyColorText Color
        {
            get => (PropertyColorText)GetPropertyObject();
            set => SetProperty(value, () => value.ToClass(), () => value.ToStyle());
        }

        /// <summary>
        /// Liefert oder setzt den Wert
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Liefert oder setzt den Minimumwert
        /// </summary>
        public int Min { get; set; }

        /// <summary>
        /// Liefert oder setzt dem Maximumwert
        /// </summary>
        public int Max { get; set; }

        /// <summary>
        /// Liefert oder setzt die Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlProgressBar(IPage page, string id = null)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="value">Der Wert</param>
        public ControlProgressBar(IPage page, string id, int value)
            : this(page, id)
        {
            Value = value;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="value">Der Wert</param>
        public ControlProgressBar(IPage page, string id, int value, int min = 0, int max = 100)
            : this(page, id)
        {
            Value = value;
            Min = min;
            Max = max;
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Min = 0;
            Max = 100;
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {

            if (Format == TypesProgressBarFormat.Default)
            {
                return new HtmlElementFormProgress(Value + "%")
                {
                    ID = ID,
                    Class = GetClasses(),
                    Style = GetStyles(),
                    Role = Role,
                    Min = Min.ToString(),
                    Max = Max.ToString(),
                    Value = Value.ToString()
                };
            }

            var bar = new HtmlElementTextContentDiv(new HtmlText(Text))
            {
                Role = "progressbar",
                Class = Css.Concatenate
                (
                    "progress-bar",
                    BackgroundColor?.ToClass(),
                    Format.ToClass()
                ),
                Style = Css.Concatenate
                (
                    "width: " + Value + "%;"
                )
            };
            bar.AddUserAttribute("aria-valuenow", Value.ToString());
            bar.AddUserAttribute("aria-valuemin", Min.ToString());
            bar.AddUserAttribute("aria-valuemax", Max.ToString());

            var html = new HtmlElementTextContentDiv(bar)
            {
                ID = ID,
                Role = Role,
                Class = Css.Concatenate
                (
                    "progress",
                    Margin.ToClass(),
                    Padding.ToClass()
                )
            };

            return html;

        }
    }
}
