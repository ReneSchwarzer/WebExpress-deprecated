using System;
using System.Globalization;
using WebExpress.Internationalization;
using WebExpress.WebJob;
using Xunit;

namespace WebExpress.Test.Schedule
{
    public class UnitTestCron
    {
        [Fact]
        public void Create_1()
        {
            var context = new HttpServerContext(null, null, null, null, null, CultureInfo.CurrentCulture, Log.Current);

            InternationalizationManager.Register(typeof(HttpServer).Assembly, "webexpress");
            InternationalizationManager.Initialization(context);

            var clock = new Clock();
            var cron = new Cron(context, "0-59", "*", "1-31", "1-2,3,4,5,6,7,8-10,11,12");

            Assert.True
            (
               cron.Matching(clock),
               "Fehler in Create_1"
            );
        }

        [Fact]
        public void Create_2()
        {
            var context = new HttpServerContext(null, null, null, null, null, CultureInfo.CurrentCulture, Log.Current);
            var dateTime = DateTime.Now;

            InternationalizationManager.Register(typeof(HttpServer).Assembly, "webexpress");
            InternationalizationManager.Initialization(context);

            var clock = new Clock(new DateTime(dateTime.Year, 2, dateTime.Day, dateTime.Hour, dateTime.Minute, 0));
            var cron = new Cron(context, "*", "*", "0-33", "2, 1-4, x");

            Assert.True
            (
               cron.Matching(clock),
               "Fehler in Create_2"
            );
        }

        [Fact]
        public void Create_3()
        {
            var context = new HttpServerContext(null, null, null, null, null, CultureInfo.CurrentCulture, Log.Current);
            var dateTime = DateTime.Now;

            InternationalizationManager.Register(typeof(HttpServer).Assembly, "webexpress");
            InternationalizationManager.Initialization(context);

            var clock = new Clock(new DateTime(dateTime.Year, 12, 31, dateTime.Hour, dateTime.Minute, 0));
            var cron = new Cron(context, "*", "*", "31", "12");

            Assert.True
            (
               cron.Matching(clock),
               "Fehler in Create_3"
            );
        }

        [Fact]
        public void Create_4()
        {
            var context = new HttpServerContext(null, null, null, null, null, CultureInfo.CurrentCulture, Log.Current);
            var dateTime = DateTime.Now;
            Log.Current.Clear();

            InternationalizationManager.Register(typeof(HttpServer).Assembly, "webexpress");
            InternationalizationManager.Initialization(context);

            var clock = new Clock(new DateTime(dateTime.Year, 12, 31, dateTime.Hour, dateTime.Minute, 0));
            var cron = new Cron(context, "*", "*", "*", "a");

            Assert.True
            (
               context.Log.WarningCount == 1,
               "Fehler in Create_4"
            );
        }

        [Fact]
        public void Create_5()
        {
            var context = new HttpServerContext(null, null, null, null, null, CultureInfo.CurrentCulture, Log.Current);
            var dateTime = DateTime.Now;

            InternationalizationManager.Register(typeof(HttpServer).Assembly, "webexpress");
            InternationalizationManager.Initialization(context);

            var clock = new Clock(new DateTime(dateTime.Year, 12, 31, dateTime.Hour, dateTime.Minute, 0));
            var cron = new Cron(context, "*", "*", "*", "");

            Assert.True
            (
               cron.Matching(clock),
               "Fehler in Create_5"
            );
        }

        [Fact]
        public void Create_6()
        {
            var context = new HttpServerContext(null, null, null, null, null, CultureInfo.CurrentCulture, Log.Current);
            var dateTime = DateTime.Now;
            Log.Current.Clear();

            InternationalizationManager.Register(typeof(HttpServer).Assembly, "webexpress");
            InternationalizationManager.Initialization(context);

            var clock = new Clock(new DateTime(dateTime.Year, 12, 31, dateTime.Hour, dateTime.Minute, 0));
            var cron = new Cron(context, "99", "*", "*", "*");

            Assert.True
            (
               context.Log.WarningCount == 1,
               "Fehler in Create_6"
            );
        }

        [Fact]
        public void Matching_1()
        {
            var context = new HttpServerContext(null, null, null, null, null, CultureInfo.CurrentCulture, Log.Current);
            var dateTime = DateTime.Now;

            InternationalizationManager.Register(typeof(HttpServer).Assembly, "webexpress");
            InternationalizationManager.Initialization(context);

            var clock = new Clock(new DateTime(dateTime.Year, 12, 31, dateTime.Hour, dateTime.Minute, 0));
            var cron = new Cron(context, "*", "*", "31", "1-11");

            Assert.True
            (
               !cron.Matching(clock),
               "Fehler in Matching_1"
            );
        }

        [Fact]
        public void Matching_2()
        {
            var context = new HttpServerContext(null, null, null, null, null, CultureInfo.CurrentCulture, Log.Current);
            var dateTime = DateTime.Now;

            InternationalizationManager.Register(typeof(HttpServer).Assembly, "webexpress");
            InternationalizationManager.Initialization(context);

            var clock = new Clock(new DateTime(2020, 1, 1, dateTime.Hour, dateTime.Minute, 0)); // Mittwoch
            var cron = new Cron(context, "*", "*", "*", "*", "4"); // Mittwoch

            Assert.True
            (
               cron.Matching(clock),
               "Fehler in Matching_2"
            );
        }

        [Fact]
        public void Matching_3()
        {
            var context = new HttpServerContext(null, null, null, null, null, CultureInfo.CurrentCulture, Log.Current);
            var dateTime = DateTime.Now;

            InternationalizationManager.Register(typeof(HttpServer).Assembly, "webexpress");
            InternationalizationManager.Initialization(context);

            var clock = new Clock(new DateTime(2020, 1, 1, dateTime.Hour, dateTime.Minute, 0)); // Mittwoch
            var cron = new Cron(context, "*", "*", "*", "*", "1"); // Sonntag

            Assert.True
            (
               !cron.Matching(clock),
               "Fehler in Matching_3"
            );
        }
    }
}
