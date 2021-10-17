﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace WebExpress.WebJob
{
    /// <summary>
    /// Verwaltet Befehle, welche zeitgesteuert ausgeführt werden (CRON = command run on notice) 
    /// </summary>
    public class Cron
    {
        /// <summary>
        /// Die Minute 0-59 oder * für belibig. Möglich sind auch kommaseperierte Werte oder Bereiche (-)
        /// </summary>
        private List<int> Minute { get; } = new List<int>();

        /// <summary>
        /// Die Stunde 0-23 oder * für belibig. Möglich sind auch kommaseperierte Werte oder Bereiche (-)
        /// </summary>
        private List<int> Hour { get; } = new List<int>();

        /// <summary>
        /// Der Tag 1-31 oder * für belibig. Möglich sind auch kommaseperierte Werte oder Bereiche (-)
        /// </summary>
        private List<int> Day { get; } = new List<int>();

        /// <summary>
        /// Der Monat 1-12 oder * für belibig. Möglich sind auch kommaseperierte Werte oder Bereiche (-)
        /// </summary>
        private List<int> Month { get; } = new List<int>();

        /// <summary>
        /// Der Wochentag 1-7, Mon-Sun oder * für belibig. Möglich sind auch kommaseperierte Werte oder Bereiche (-)
        /// </summary>
        private List<int> Weekday { get; } = new List<int>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="minute">Die Minute 0-59 oder * für belibig. Möglich sind auch kommaseperierte Werte oder Bereiche (-)</param>
        /// <param name="hour">Die Stunde 0-23 oder * für belibig. Möglich sind auch kommaseperierte Werte oder Bereiche (-)</param>
        /// <param name="day">Der Tag 0-31 oder * für belibig. Möglich sind auch kommaseperierte Werte oder Bereiche (-)</param>
        /// <param name="month">Der Monat 0-12 oder * für belibig. Möglich sind auch kommaseperierte Werte oder Bereiche (-)</param>
        /// <param name="weekday">Der Wochentag 0-7, Mon-Sun oder * für belibig. Möglich sind auch kommaseperierte Werte oder Bereiche (-)</param>
        public Cron(string minute = "*", string hour = "*", string day = "*", string month = "*", string weekday = "*")
        {
            Minute.AddRange(Parse(minute, 0, 60));
            Hour.AddRange(Parse(hour, 0, 24));
            Day.AddRange(Parse(day, 1, 31));
            Month.AddRange(Parse(month, 1, 12));
            Weekday.AddRange(Parse(weekday, 1, 7));
        }

        /// <summary>
        /// Parst den Wert
        /// </summary>
        /// <param name="value">Der zu parsende Wert</param>
        /// <param name="minValue">Das Minimum</param>
        /// <param name="maxValue">Das Maximum</param>
        /// <returns></returns>
        private IEnumerable<int> Parse(string value, int minValue, int maxValue)
        {
            var items = new List<int>() as IEnumerable<int>;
            value = value?.ToLower().Trim();

            if (value.Equals("*"))
            {
                return Enumerable.Range(minValue, maxValue).ToList();
            }

            var commaSeparatedList = value.Split
            (   
                ',', 
                System.StringSplitOptions.RemoveEmptyEntries | System.StringSplitOptions.TrimEntries
            );

            foreach (var i in commaSeparatedList)
            {
                var range = i.Split('-', System.StringSplitOptions.RemoveEmptyEntries | System.StringSplitOptions.TrimEntries);
                if (range.Length == 2)
                {
                    // Bereichswerte
                    var min = int.MinValue;
                    var max = int.MinValue;
                                        
                    if (int.TryParse(range[0], out int minResult))
                    {
                        min = Math.Max(minResult, minValue);
                    }

                    if (int.TryParse(range[1], out int maxResult))
                    {
                        max = Math.Min(maxResult, maxValue);
                    }

                    if (min != int.MinValue && max != int.MinValue && min < max)
                    {
                        items = items.Union(Enumerable.Range(min, (max - min) + 1));
                    }
                }
                else if (range.Length == 1)
                {
                    if (int.TryParse(range[0], out int result))
                    {
                        if (result >= minValue && result <= maxValue)
                        {
                            items = items.Union(new List<int> { result });
                        }
                    }
                }
            }

            return items;
        }

        /// <summary>
        /// Prüft ob die zyklische Abarbeitung eines Befehles erfolgen kann
        /// </summary>
        /// <returns>True, wenn eine Übereinstimmung vorliegt, false sonst</returns>
        public bool Matching(Clock clock)
        {
            return Minute.Contains(clock.Minute) && 
                   Hour.Contains(clock.Hour) && 
                   Day.Contains(clock.Day) && 
                   Month.Contains(clock.Month);
        }
    }
}
