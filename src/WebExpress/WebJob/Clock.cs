﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WebExpress.Test")]

namespace WebExpress.WebJob
{
    public class Clock
    {
        /// <summary>
        /// The underlying date and time.
        /// </summary>
        private DateTime DateTime { get; set; }

        /// <summary>
        /// The minute 0-59.
        /// </summary>
        public int Minute => DateTime.Minute;

        /// <summary>
        /// The hour 0-23.
        /// </summary>
        public int Hour => DateTime.Hour;

        /// <summary>
        /// The day 1-31.
        /// </summary>
        public int Day => DateTime.Day;

        /// <summary>
        /// The month 1-12.
        /// </summary>
        public int Month => DateTime.Month;

        /// <summary>
        /// The weekday 0-6 (Sunday-Saturday).
        /// </summary>
        public int Weekday => (int)DateTime.DayOfWeek;

        /// <summary>
        /// Constructor
        /// </summary>
        public Clock()
        {
            var dateTime = DateTime.Now;

            DateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dateTime">The time to copy.</param>
        public Clock(DateTime dateTime)
        {
            DateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0);
        }

        /// <summary>
        /// Copy-Constructor
        /// </summary>
        /// <param name="clock">The clock to be copied.</param>
        public Clock(Clock clock)
        {
            DateTime = clock.DateTime;
        }

        /// <summary>
        /// Move the clock one tick (one minute) further.
        /// </summary>
        internal void Tick()
        {
            DateTime = DateTime.AddMinutes(1);
        }

        /// <summary>
        /// Synchronizes the clock with the current time.
        /// </summary>
        /// <returns>The missed times.</returns>
        public IEnumerable<Clock> Synchronize()
        {
            var dateTime = DateTime.Now;
            var current = new Clock();
            var next = new Clock(this);

            var elapsed = new List<Clock>();

            while (next < current)
            {
                elapsed.Add(new Clock(next));
                next.Tick();
            }

            DateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0);

            return elapsed;
        }

        /// <summary>
        /// Compare two times.
        /// </summary>
        /// <param name="obj1">The first time.</param>
        /// <param name="obj2">The second time.</param>
        /// <returns>True if both times match, false otherwise.</returns>
        public static bool operator ==(Clock obj1, Clock obj2)
        {
            return obj1.Minute == obj2.Minute && obj1.Hour == obj2.Hour && obj1.Day == obj2.Day && obj1.Month == obj2.Month;
        }

        /// <summary>
        /// Compares two times for inequality
        /// </summary>
        /// <param name="obj1">The first time.</param>
        /// <param name="obj2">The second time.</param>
        /// <returns>True if both times do not match, false otherwise.</returns>
        public static bool operator !=(Clock obj1, Clock obj2)
        {
            return obj1.Minute != obj2.Minute || obj1.Hour != obj2.Hour || obj1.Day != obj2.Day || obj1.Month != obj2.Month;
        }

        /// <summary>
        /// Compares two times to less than.
        /// </summary>
        /// <param name="obj1">The first time.</param>
        /// <param name="obj2">The second time.</param>
        /// <returns>True wenn die linke Uhrzeit kleiner ist als die Rechte, false sonst</returns>
        public static bool operator <(Clock obj1, Clock obj2)
        {
            return obj1.DateTime < obj2.DateTime;
        }

        /// <summary>
        /// Compares two times to greater than.
        /// </summary>
        /// <param name="obj1">The first time.</param>
        /// <param name="obj2">The second time.</param>
        /// <returns>True if the time on the left is greater than the time on the right, false otherwise.</returns>
        public static bool operator >(Clock obj1, Clock obj2)
        {
            return obj1.DateTime > obj2.DateTime;
        }

        /// <summary>
        /// Compares two times to less than or equal to.
        /// </summary>
        /// <param name="obj1">The first time.</param>
        /// <param name="obj2">The second time.</param>
        /// <returns>True if the time on the left is less than the time on the right, otherwise it is false.</returns>
        public static bool operator <=(Clock obj1, Clock obj2)
        {
            return obj1 < obj2 || obj1 == obj2;
        }

        /// <summary>
        /// Compares two times to greater than or equal to.
        /// </summary>
        /// <param name="obj1">The first time.</param>
        /// <param name="obj2">The second time.</param>
        /// <returns>True if the time on the left is greater than the time on the right, false otherwise.</returns>
        public static bool operator >=(Clock obj1, Clock obj2)
        {
            return obj1 > obj2 || obj1 == obj2;
        }

        /// <summary>
        /// The comparison function.
        /// </summary>
        /// <param name="obj">The time to compare.</param>
        /// <returns>True if both times are the same, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return Minute == (obj as Clock).Minute &&
                   Hour == (obj as Clock).Hour &&
                   Day == (obj as Clock).Day &&
                   Month == (obj as Clock).Month;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return DateTime.GetHashCode();
        }
    }
}
