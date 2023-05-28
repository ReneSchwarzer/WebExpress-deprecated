using System;

namespace WebExpress.UI.WebSettingPage
{
    public class TimeSpanConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return null;
            }

            var ts = TimeSpan.Parse(value.ToString());
            return string.Format
                (
                    "{0}d {1:D2}h {2:D2}m {3:D2}s {4:D2}ms",
                    ts.Days,
                    ts.Hours,
                    ts.Minutes,
                    ts.Seconds,
                    ts.Milliseconds
                );
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
