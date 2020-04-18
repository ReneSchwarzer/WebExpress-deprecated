using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Informationszähler
    /// </summary>
    public class ControlCardCounter : ControlPanelCard
    {
        /// <summary>
        /// Liefert oder setzt die Textfarbe
        /// </summary>
        public TypesTextColor Color { get; set; }

        /// <summary>
        /// Liefert oder setzt das Icon
        /// </summary>
        public Icon Icon { get; set; }

        /// <summary>
        /// Liefert oder setzt den Wert des Counters
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Liefert oder setzt den Wert des Fortschrittbalkens
        /// </summary>
        public int Progress { get; set; }

        /// <summary>
        /// Liefert oder setzt den Text des Counters
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlCardCounter(IPage page, string id = null)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Progress = -1;
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            base.Content.Clear();

            if (Icon != Icon.None)
            {
                base.Content.Add(new ControlText(Page) { Class = Icon.ToClass(), Color = Color, HorizontalAlignment = TypesHorizontalAlignment.Right });
            }

            var text = new ControlText(Page, string.IsNullOrWhiteSpace(ID) ? null : ID + "_header") { Text = Value, Format = TypesTextFormat.H4 };
            var info = new ControlText(Page) { Text = Text, Format = TypesTextFormat.Span, Color = TypesTextColor.Muted };

            base.Content.Add(new ControlPanel(Page, text, info) { });

            var color = TypesLayoutProgressBar.Default;

            switch (Color)
            {
                case TypesTextColor.Primary:
                    color = TypesLayoutProgressBar.Primary;
                    break;
                case TypesTextColor.Success:
                    color = TypesLayoutProgressBar.Success;
                    break;
                case TypesTextColor.Info:
                    color = TypesLayoutProgressBar.Info;
                    break;
                case TypesTextColor.Warning:
                    color = TypesLayoutProgressBar.Warning;
                    break;
                case TypesTextColor.Danger:
                    color = TypesLayoutProgressBar.Danger;
                    break;
                case TypesTextColor.Light:
                    color = TypesLayoutProgressBar.Light;
                    break;
                case TypesTextColor.Dark:
                    color = TypesLayoutProgressBar.Dark;
                    break;
            }

            if (Progress > -1)
            {
                base.Content.Add(new ControlProgressBar(Page) { Value = Progress, Format = TypesProgressBarFormat.Striped, Layout = color, Size = TypesSize.Small });
            }

            return base.ToHtml();
        }
    }
}
