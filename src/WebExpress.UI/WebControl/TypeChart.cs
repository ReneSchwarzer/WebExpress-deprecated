namespace WebExpress.UI.WebControl
{
    public enum TypeChart
    {
        Line = 0,
        Bar = 1,
        Pie = 2,
        Doughnut = 3,
        //Polar = 4,
        Radar = 5
    }

    public static class TypeChartExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="color">Die Farbe, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToType(this TypeChart color)
        {
            return color switch
            {
                TypeChart.Line => "line",
                TypeChart.Bar => "bar",
                TypeChart.Pie => "pie",
                TypeChart.Doughnut => "doughnut",
                TypeChart.Radar => "radar",
                _ => "line",
            };
        }
    }
}
