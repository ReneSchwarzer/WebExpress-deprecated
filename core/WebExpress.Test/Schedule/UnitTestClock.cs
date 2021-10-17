using System;
using System.Linq;
using WebExpress.WebJob;
using Xunit;

namespace WebExpress.Test.Schedule
{
    public class UnitTestClock
    {
        [Fact]
        public void Synchronize_1()
        {
            var dateTime = DateTime.Now;

            var clock = new Clock(DateTime.Now.AddMinutes(-5));

            var elapsed = clock.Synchronize();

            Assert.True
            (
               elapsed.Count() == 5,
               "Fehler in Synchronize_1"
            );
        }

        [Fact]
        public void Synchronize_2()
        {
            var dateTime = DateTime.Now;

            var clock = new Clock(DateTime.Now.AddDays(-1));

            var elapsed = clock.Synchronize();

            Assert.True
            (
               elapsed.Count() == 60 * 24,
               "Fehler in Synchronize_2"
            );
        }

        [Fact]
        public void Compare_Equals_1()
        {
            var clock1 = new Clock();
            var clock2 = new Clock(clock1);

            var res = clock1 == clock2;

            Assert.True
            (
               res,
               "Fehler in Compare_Equals_1"
            );
        }

        [Fact]
        public void Compare_Equals_2()
        {
            var clock1 = new Clock();
            var clock2 = new Clock(DateTime.Now.AddMinutes(5));

            var res = clock1 == clock2;

            Assert.True
            (
               !res,
               "Fehler in Compare_Equals_2"
            );
        }

        [Fact]
        public void Compare_Inequality_1()
        {
            var clock1 = new Clock();
            var clock2 = new Clock(clock1);

            var res = clock1 != clock2;

            Assert.True
            (
               !res,
               "Fehler in Compare_Inequality_1"
            );
        }

        [Fact]
        public void Compare_Inequality_2()
        {
            var clock1 = new Clock();
            var clock2 = new Clock(DateTime.Now.AddMinutes(5));

            var res = clock1 != clock2;

            Assert.True
            (
               res,
               "Fehler in Compare_Inequality_2"
            );
        }

        [Fact]
        public void Compare_Less_1()
        {
            var clock1 = new Clock();
            var clock2 = new Clock(clock1);

            var res = clock1 < clock2;

            Assert.True
            (
               !res,
               "Fehler in Compare_Less_1"
            );
        }

        [Fact]
        public void Compare_Less_2()
        {
            var clock1 = new Clock();
            var clock2 = new Clock(DateTime.Now.AddMinutes(5));

            var res = clock1 < clock2;

            Assert.True
            (
               res,
               "Fehler in Compare_Less_2"
            );
        }

        [Fact]
        public void Compare_Greater_1()
        {
            var clock1 = new Clock();
            var clock2 = new Clock(clock1);

            var res = clock1 > clock2;

            Assert.True
            (
               !res,
               "Fehler in Compare_Greater_1"
            );
        }

        [Fact]
        public void Compare_Greater_2()
        {
            var clock1 = new Clock();
            var clock2 = new Clock(DateTime.Now.AddMinutes(-5).AddDays(-5));

            var res = clock1 > clock2;

            Assert.True
            (
               res,
               "Fehler in Compare_Greater_2"
            );
        }

        [Fact]
        public void Compare_LessOrEqual_1()
        {
            var clock1 = new Clock();
            var clock2 = new Clock(clock1);

            var res = clock1 <= clock2;

            Assert.True
            (
               res,
               "Fehler in Compare_LessOrEqual_1"
            );
        }

        [Fact]
        public void Compare_LessOrEqual_2()
        {
            var clock1 = new Clock();
            var clock2 = new Clock(DateTime.Now.AddMinutes(5));

            var res = clock1 <= clock2;

            Assert.True
            (
               res,
               "Fehler in Compare_LessOrEqual_2"
            );
        }

        [Fact]
        public void Compare_GreaterOrEqual_1()
        {
            var clock1 = new Clock();
            var clock2 = new Clock(clock1);

            var res = clock1 >= clock2;

            Assert.True
            (
               res,
               "Fehler in Compare_GreaterOrEqual_1"
            );
        }

        [Fact]
        public void Compare_GreaterOrEqual_2()
        {
            var clock1 = new Clock();
            var clock2 = new Clock(DateTime.Now.AddMinutes(-5).AddDays(-5));

            var res = clock1 >= clock2;

            Assert.True
            (
               res,
               "Fehler in Compare_GreaterOrEqual_2"
            );
        }

        [Fact]
        public void Carry_1()
        {
            var clock1 = new Clock(new DateTime(2020, 12, 31, 23, 59, 0));
            var clock2 = new Clock(new DateTime(2021, 1, 1, 0, 0, 0));
            clock1.Tick();


            Assert.True
            (
               clock1 == clock2,
               "Fehler in Carry"
            );
        }

        [Fact]
        public void Carry_2()
        {
            var clock1 = new Clock(new DateTime(2021, 2, 28, 23, 59, 0));
            var clock2 = new Clock(new DateTime(2021, 3, 1, 0, 0, 0));
            clock1.Tick();


            Assert.True
            (
               clock1 == clock2,
               "Fehler in Carry"
            );
        }
    }
}
