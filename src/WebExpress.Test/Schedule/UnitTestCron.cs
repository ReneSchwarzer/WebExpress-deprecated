﻿using System;
using System.Globalization;
using WebExpress.WebComponent;
using WebExpress.WebJob;
using Xunit;

namespace WebExpress.Test.Schedule
{
    /// <summary>
    /// Test the cron job of the scheduler.
    /// </summary>
    public class UnitTestCron
    {
        [Fact]
        public void Create_1()
        {
            var context = new HttpServerContext(null, null, null, null, null, null, null, CultureInfo.CurrentCulture, Log.Current, null);

            ComponentManager.Initialization(context);

            var clock = new Clock();
            var cron = new Cron(context, "0-59", "*", "1-31", "1-2,3,4,5,6,7,8-10,11,12");

            Assert.True
            (
               cron.Matching(clock)
            );
        }

        [Fact]
        public void Create_2()
        {
            var context = new HttpServerContext(null, null, null, null, null, null, null, CultureInfo.CurrentCulture, Log.Current, null);
            var dateTime = DateTime.Now;

            ComponentManager.Initialization(context);

            var clock = new Clock(new DateTime(dateTime.Year, 2, dateTime.Day, dateTime.Hour, dateTime.Minute, 0));
            var cron = new Cron(context, "*", "*", "0-33", "2, 1-4, x");

            Assert.True
            (
               cron.Matching(clock)
            );
        }

        [Fact]
        public void Create_3()
        {
            var context = new HttpServerContext(null, null, null, null, null, null, null, CultureInfo.CurrentCulture, Log.Current, null);
            var dateTime = DateTime.Now;

            ComponentManager.Initialization(context);

            var clock = new Clock(new DateTime(dateTime.Year, 12, 31, dateTime.Hour, dateTime.Minute, 0));
            var cron = new Cron(context, "*", "*", "31", "12");

            Assert.True
            (
               cron.Matching(clock)
            );
        }

        [Fact]
        public void Create_4()
        {
            var context = new HttpServerContext(null, null, null, null, null, null, null, CultureInfo.CurrentCulture, Log.Current, null);
            var dateTime = DateTime.Now;
            Log.Current.Clear();

            ComponentManager.Initialization(context);

            var clock = new Clock(new DateTime(dateTime.Year, 12, 31, dateTime.Hour, dateTime.Minute, 0));
            var cron = new Cron(context, "*", "*", "*", "a");

            Assert.True
            (
               context.Log.WarningCount == 1
            );
        }

        [Fact]
        public void Create_5()
        {
            var context = new HttpServerContext(null, null, null, null, null, null, null, CultureInfo.CurrentCulture, Log.Current, null);
            var dateTime = DateTime.Now;

            ComponentManager.Initialization(context);

            var clock = new Clock(new DateTime(dateTime.Year, 12, 31, dateTime.Hour, dateTime.Minute, 0));
            var cron = new Cron(context, "*", "*", "*", "");

            Assert.True
            (
               cron.Matching(clock)
            );
        }

        [Fact]
        public void Create_6()
        {
            var context = new HttpServerContext(null, null, null, null, null, null, null, CultureInfo.CurrentCulture, Log.Current, null);
            var dateTime = DateTime.Now;
            Log.Current.Clear();

            ComponentManager.Initialization(context);

            var clock = new Clock(new DateTime(dateTime.Year, 12, 31, dateTime.Hour, dateTime.Minute, 0));
            var cron = new Cron(context, "99", "*", "*", "*");

            Assert.True
            (
               context.Log.WarningCount == 1
            );
        }

        [Fact]
        public void Matching_1()
        {
            var context = new HttpServerContext(null, null, null, null, null, null, null, CultureInfo.CurrentCulture, Log.Current, null);
            var dateTime = DateTime.Now;

            ComponentManager.Initialization(context);

            var clock = new Clock(new DateTime(dateTime.Year, 12, 31, dateTime.Hour, dateTime.Minute, 0));
            var cron = new Cron(context, "*", "*", "31", "1-11");

            Assert.True
            (
               !cron.Matching(clock)
            );
        }

        [Fact]
        public void Matching_2()
        {
            var context = new HttpServerContext(null, null, null, null, null, null, null, CultureInfo.CurrentCulture, Log.Current, null);
            var dateTime = DateTime.Now;

            ComponentManager.Initialization(context);

            var clock = new Clock(new DateTime(2020, 1, 1, dateTime.Hour, dateTime.Minute, 0)); // wednesday
            var cron = new Cron(context, "*", "*", "*", "*", "3"); // wednesday

            Assert.True
            (
               cron.Matching(clock)
            );
        }

        [Fact]
        public void Matching_3()
        {
            var context = new HttpServerContext(null, null, null, null, null, null, null, CultureInfo.CurrentCulture, Log.Current, null);
            var dateTime = DateTime.Now;

            ComponentManager.Initialization(context);

            var clock = new Clock(new DateTime(2020, 1, 1, dateTime.Hour, dateTime.Minute, 0)); // wednesday
            var cron = new Cron(context, "*", "*", "*", "*", "1"); // sunday

            Assert.True
            (
               !cron.Matching(clock)
            );
        }
    }
}
