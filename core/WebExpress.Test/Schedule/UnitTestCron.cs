using System;
using System.Linq;
using WebExpress.WebJob;
using Xunit;

namespace WebExpress.Test.Schedule
{
    public class UnitTestCron
    {
        [Fact]
        public void Create_1()
        {
            var clock = new Clock();
            var cron = new Cron("0-59", "*", "1-31", "1-2,3,4,5,6,7,8-10,11,12");

            Assert.True
            (
               cron.Matching(clock),
               "Fehler in Create_1"
            );
        }

        [Fact]
        public void Create_2()
        {
            var dateTime = DateTime.Now;
            var clock = new Clock(new DateTime(dateTime.Year, 2, dateTime.Day, dateTime.Hour, dateTime.Minute, 0));
            var cron = new Cron("*", "*", "0-33", "2, 1-4, x");

            Assert.True
            (
               cron.Matching(clock),
               "Fehler in Create_2"
            );
        }

        [Fact]
        public void Create_3()
        {
            var dateTime = DateTime.Now;
            var clock = new Clock(new DateTime(dateTime.Year, 12, 31, dateTime.Hour, dateTime.Minute, 0));
            var cron = new Cron("*", "*", "31", "12");

            Assert.True
            (
               cron.Matching(clock),
               "Fehler in Create_3"
            );
        }

        [Fact]
        public void Matching_1()
        {
            var dateTime = DateTime.Now;
            var clock = new Clock(new DateTime(dateTime.Year, 12, 31, dateTime.Hour, dateTime.Minute, 0));
            var cron = new Cron("*", "*", "31", "1-11");

            Assert.True
            (
               !cron.Matching(clock),
               "Fehler in Matching_1"
            );
        }
    }
}
