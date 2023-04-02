﻿using System;
using static WebExpress.Log;

namespace WebExpress
{
    /// <summary>
    /// Log entry
    /// </summary>
    internal class LogItem
    {
        /// <summary>
        /// Level of the entry.
        /// </summary>
        private readonly Level m_level;

        /// <summary>
        /// The instance (location).
        /// </summary>
        private readonly string m_instance;

        /// <summary>
        /// The log message.
        /// </summary>
        private readonly string m_message;

        /// <summary>
        /// The timestamp.
        /// </summary>
        private readonly DateTime m_timestamp;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="instance">The modul/funktion.</param>
        /// <param name="message">The log message.</param>
        public LogItem(Level level, string instance, string message, string timePattern)
        {
            m_level = level;
            m_instance = instance;
            m_message = message;
            m_timestamp = DateTime.Now;
            TimePattern = timePattern;
        }

        /// <summary>
        /// Converts the value of this instance to a string.
        /// </summary>
        /// <returns>The log entry as a string</returns>
        public override string ToString()
        {
            if (m_level != Level.Seperartor)
            {
                return m_timestamp.ToString(TimePattern) + " " + m_level.ToString().PadRight(9, ' ') + " " + m_instance.PadRight(19, ' ').Substring(0, 19) + " " + m_message;
            }
            else
            {
                return m_message;
            }
        }

        /// <summary>
        /// Returns the level of the entry.
        /// </summary>
        public Level Level => m_level;

        /// <summary>
        /// Returns the instance (location).
        /// </summary>
        public string Instance => m_instance;

        /// <summary>
        /// Returns the message.
        /// </summary>
        public string Message => m_message;

        /// <summary>
        /// Returns the timestamp.
        /// </summary>
        public DateTime Timestamp => m_timestamp;


        /// <summary>
        /// Returns the or set the time patterns for log entries.
        /// </summary>
        public string TimePattern { set; get; }
    };
}
