namespace WebExpress.Attribute
{
    public class JobAttribute : System.Attribute
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="minute">Die Minute 0-59 oder * für belibig. Möglich sind auch kommaseperierte Werte oder Bereiche (-)</param>
        /// <param name="hour">Die Stunde 0-23 oder * für belibig. Möglich sind auch kommaseperierte Werte oder Bereiche (-)</param>
        /// <param name="day">Der Tag 0-31 oder * für belibig. Möglich sind auch kommaseperierte Werte oder Bereiche (-)</param>
        /// <param name="month">Der Monat 0-12 oder * für belibig. Möglich sind auch kommaseperierte Werte oder Bereiche (-)</param>
        /// <param name="weekday">Der Wochentag 0-7, Mon-Sun oder * für belibig. Möglich sind auch kommaseperierte Werte oder Bereiche (-)</param>
        public JobAttribute(string minute = "*", string hour = "*", string day = "*", string month = "*", string weekday = "*")
        {

        }
    }
}
