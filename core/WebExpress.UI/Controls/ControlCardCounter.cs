using System.Collections.Generic;
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
        /// Liefert oder setzt die Farbe des Textes
        /// </summary>
        public PropertyColorText Color
        {
            get => (PropertyColorText)GetPropertyObject();
            set => SetProperty(value, () => value.ToClass(), () => value.ToStyle());
        }

        /// <summary>
        /// Liefert oder setzt das Icon
        /// </summary>
        public TypeIcon Icon { get; set; }

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
            Color = new PropertyColorText(TypeColorText.Default);
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
            Content.Clear();

            if (Icon != TypeIcon.None)
            {
                Content.Add(new ControlIcon(Page) 
                {
                    Icon = new PropertyIcon(Icon), 
                    TextColor = Color, 
                    HorizontalAlignment = TypeHorizontalAlignment.Right 
                });
            }

            var text = new ControlText(Page, string.IsNullOrWhiteSpace(ID) ? null : ID + "_header") 
            { 
                Text = Value, 
                Format = TypeFormatText.H4 
            };

            var info = new ControlText(Page) 
            { 
                Text = Text, 
                Format = TypeFormatText.Span, 
                TextColor = new PropertyColorText(TypeColorText.Muted) 
            };

            Content.Add(new ControlPanel(Page, text, info) { });

            if (Progress > -1)
            {
                Content.Add(new ControlProgressBar(Page) 
                { 
                    Value = Progress, 
                    Format = TypeFormatProgress.Striped, 
                    BackgroundColor = BackgroundColor, 
                    //Color = Color,
                    Size = TypeSizeProgress.Small 
                });
            }

            return base.ToHtml();
        }
    }
}
