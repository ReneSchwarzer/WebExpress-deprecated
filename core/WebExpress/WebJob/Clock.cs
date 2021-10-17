using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WebExpress.Test")]

namespace WebExpress.WebJob
{
    public class Clock
    {
        /// <summary>
        /// Das zugrundeliegende Datum und die Zeit
        /// </summary>
        private DateTime DateTime { get; set; }

        /// <summary>
        /// Die Minute 0-59
        /// </summary>
        public int Minute => DateTime.Minute;

        /// <summary>
        /// Die Stunde 0-23
        /// </summary>
        public int Hour => DateTime.Hour;

        /// <summary>
        /// Der Tag 0-31
        /// </summary>
        public int Day => DateTime.Day;

        /// <summary>
        /// Der Monat 0-12
        /// </summary>
        public int Month => DateTime.Month;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Clock()
        {
            var dateTime = DateTime.Now;

            DateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="dateTime">Die zu kopierende Uhrzeit</param>
        public Clock(DateTime dateTime)
        {
            DateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0);
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="clock">Die zu kopierende Uhr</param>
        public Clock(Clock clock)
        {
            DateTime = clock.DateTime;
        }

        /// <summary>
        /// Die Uhr um einen Tick (eine Minute) weiter stellen
        /// </summary>
        internal void Tick()
        {
            DateTime = DateTime.AddMinutes(1);
        }

        /// <summary>
        /// Synchronisiert die Uhr mit der aktuellen Zeit
        /// </summary>
        /// <returns>Die verpassten Zeiten</returns>
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
        /// Verglchsoperator. Vergleicht zweier Urzeiten
        /// </summary>
        /// <param name="obj1">Die erste Urzeit</param>
        /// <param name="obj2">Die zweite Urzeit</param>
        /// <returns>True wenn beide Urzeiten übereinstimmen, false sonst</returns>
        public static bool operator ==(Clock obj1, Clock obj2)
        {
            return obj1.Minute == obj2.Minute && obj1.Hour == obj2.Hour && obj1.Day == obj2.Day && obj1.Month == obj2.Month;
        }

        /// <summary>
        /// Verglchsoperator. Vergleicht zweier Urzeiten
        /// </summary>
        /// <param name="obj1">Die erste Urzeit</param>
        /// <param name="obj2">Die zweite Urzeit</param>
        /// <returns>True wenn beide Urzeiten übereinstimmen, false sonst</returns>
        public static bool operator !=(Clock obj1, Clock obj2)
        {
            return obj1.Minute != obj2.Minute || obj1.Hour != obj2.Hour || obj1.Day != obj2.Day || obj1.Month != obj2.Month;
        }

        /// <summary>
        /// Verglchsoperator. Vergleicht zweier Urzeiten
        /// </summary>
        /// <param name="obj1">Die erste Urzeit</param>
        /// <param name="obj2">Die zweite Urzeit</param>
        /// <returns>True wenn die linke Urzeit kleiner ist als die Rechte, false sonst</returns>
        public static bool operator <(Clock obj1, Clock obj2)
        {
            return obj1.DateTime < obj2.DateTime;
        }

        /// <summary>
        /// Verglchsoperator. Vergleicht zweier Urzeiten
        /// </summary>
        /// <param name="obj1">Die erste Urzeit</param>
        /// <param name="obj2">Die zweite Urzeit</param>
        /// <returns>True wenn die linke Urzeit größer ist als die Rechte, false sonst</returns>
        public static bool operator >(Clock obj1, Clock obj2)
        {
            return obj1.DateTime > obj2.DateTime;
        }

        /// <summary>
        /// Verglchsoperator. Vergleicht zweier Urzeiten
        /// </summary>
        /// <param name="obj1">Die erste Urzeit</param>
        /// <param name="obj2">Die zweite Urzeit</param>
        /// <returns>True wenn die linke Urzeit kleiner ist als die Rechte, false sonst</returns>
        public static bool operator <=(Clock obj1, Clock obj2)
        {
            return obj1 < obj2 || obj1 == obj2;
        }

        /// <summary>
        /// Verglchsoperator. Vergleicht zweier Urzeiten
        /// </summary>
        /// <param name="obj1">Die erste Urzeit</param>
        /// <param name="obj2">Die zweite Urzeit</param>
        /// <returns>True wenn die linke Urzeit größer ist als die Rechte, false sonst</returns>
        public static bool operator >=(Clock obj1, Clock obj2)
        {
            return obj1 > obj2 || obj1 == obj2;
        }

        /// <summary>
        /// Verglichsfunktion
        /// </summary>
        /// <param name="obj">Die zu vergleichende Uhrzeit</param>
        /// <returns>True wenn beide Urzeiten gleich sind, false sonst</returns>
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
        /// Ermittelt den Hashcode für diese Instanz
        /// </summary>
        /// <returns>Der Hashcode</returns>
        public override int GetHashCode()
        {
            return DateTime.GetHashCode();
        }
    }
}
