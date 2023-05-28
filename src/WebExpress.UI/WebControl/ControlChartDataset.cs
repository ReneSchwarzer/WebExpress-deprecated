using System.Collections.Generic;

namespace WebExpress.UI.WebControl
{
    public class ControlChartDataset
    {
        /// <summary>
        /// Returns or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Returns or sets the data.
        /// </summary>
        public float[] Data { get; set; }

        /// <summary>
        /// Liefert oder setzt die Hintergrundfarbe
        /// </summary>
        public List<PropertyColorChart> BackgroundColor { get; set; } = new List<PropertyColorChart> { new PropertyColorChart(TypeColorChart.Primary) };

        /// <summary>
        /// Liefert oder setzt die Rahmenfarbe
        /// </summary>
        public List<PropertyColorChart> BorderColor { get; set; } = new List<PropertyColorChart> { new PropertyColorChart(TypeColorChart.Primary) };

        /// <summary>
        /// Bestimmt, wie die Datenreihen ausgefüllt werden
        /// </summary>
        public TypeFillChart Fill { get; set; } = TypeFillChart.None;

        /// <summary>
        /// Bestimmt, die Datenwerte angezeigt werden
        /// </summary>
        public TypePointChart Point { get; set; } = TypePointChart.Circle;

        /// <summary>
        /// Constructor
        /// </summary>
        public ControlChartDataset()
        {
        }
    }
}
