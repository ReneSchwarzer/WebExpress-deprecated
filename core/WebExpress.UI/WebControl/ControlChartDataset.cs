using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebExpress.Html;
using WebExpress.Module;
using WebExpress.Uri;

namespace WebExpress.UI.WebControl
{
    public class ControlChartDataset
    {
        /// <summary>
        /// Liefert oder setzt den Titel
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Liefert oder setzt die Daten
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
        /// Konstruktor
        /// </summary>
        public ControlChartDataset()
        {
        }
    }
}
