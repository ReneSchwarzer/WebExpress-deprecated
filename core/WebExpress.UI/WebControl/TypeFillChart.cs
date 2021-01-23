namespace WebExpress.UI.WebControl
{
    public enum TypeFillChart
    {
        None = 0,
        Origin = 1,
        Start = 2,
        End = 3
    }

    public static class TypeFillChartExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="color">Die Farbe, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToType(this TypeFillChart color)
        {
            return color switch
            {
                TypeFillChart.None => "false",
                TypeFillChart.Origin => "origin",
                TypeFillChart.Start => "start",
                TypeFillChart.End => "end",
                _ => "false",
            };
        }
    }
}
