using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using WebExpress.Setting;

namespace WebExpress
{
    /// <summary>
    /// Class for logging events to your log file
    /// 
    /// The program writes a variety of information to an event log file. The log 
    /// is stored in the log directory. The name consists of the date and the ending ".log". 
    /// The structure is designed in such a way that the log file can be read and analyzed with a text editor.
    /// Error messages and notes are made available persistently in the log, so the event log files 
    /// are suitable for error analysis and for checking the correct functioning of the program. The minutes 
    /// are organized in tabular form. In the first column, the primeval time is indicated. The second 
    /// column defines the level of the log entry. The third column lists the function that produced the entry. 
    /// The last column indicates a note or error description.
    /// </summary>
    /// <example>
    /// <b>Example:</b><br>
    /// 08:26:30 Info      Program.Main                   Startup<br>
    /// 08:26:30 Info      Program.Main                   --------------------------------------------------<br>
    /// 08:26:30 Info      Program.Main                   Version: 0.0.0.1<br>
    /// 08:26:30 Info      Program.Main                   Arguments: -test <br>
    /// 08:26:30 Info      Program.Main                   Configuration version: V1<br>
    /// 08:26:30 Info      Program.Main                   Processing: sequentiell<br>
    /// </example>
    public class Log : ILogger
    {
        /// <summary>
        /// Enumeration defines the different log levels.
        /// </summary>
        public enum Level { Info, Warning, FatalError, Error, Exception, Debug, Seperartor };

        /// <summary>
        /// Enumerations of the log mode.
        /// </summary>
        public enum Mode { Off, Append, Override };

        /// <summary>
        /// Returns or sets the encoding.
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// Determines whether to display debug output.
        /// </summary>
        public bool DebugMode { get; private set; } = false;

        /// <summary>
        /// Returns the file name of the log
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Returns the number of exceptions.
        /// </summary>
        public int ExceptionCount { get; protected set; }

        /// <summary>
        /// Returns the number of errors (errors + exceptions).
        /// </summary>
        public int ErrorCount { get; protected set; }

        /// <summary>
        /// Returns the number of warnings.
        /// </summary>
        public int WarningCount { get; protected set; }

        /// <summary>
        /// Checks if the log has been opened for writing.
        /// </summary>
        public bool IsOpen => m_workerThread != null;

        /// <summary>
        /// Returns the log mode.
        /// </summary>
        public Mode LogMode { get; set; }

        /// <summary>
        /// The default instance of the logger.
        /// </summary>
        public static Log Current { get; } = new Log();

        /// <summary>
        /// The directory where the log is created.
        /// </summary>
        private string m_path;

        /// <summary>
        /// The thread that takes care of the cyclic writing in the log file.
        /// </summary>
        private Thread m_workerThread;

        /// <summary>
        /// Constant that determines the further of the separator rows.
        /// </summary>
        private const int m_seperatorWidth = 260;

        /// <summary>
        /// End worker thread lifecycle.
        /// </summary>
        private bool m_done = false;

        /// <summary>
        /// Unsaved Entries Queue.
        /// </summary>
        private readonly Queue<LogItem> m_queue = new Queue<LogItem>();

        /// <summary>
        /// Constructor
        /// </summary>
        public Log()
        {
            Encoding = Encoding.UTF8;
            FilePattern = "yyyyMMdd";
            TimePattern = "yyyMMddHHmmss";
            LogMode = Log.Mode.Append;
        }

        /// <summary>
        /// Starts logging
        /// </summary>
        /// <param name="path">The path where the log file is created.</param>
        /// <param name="name">The file name of the log file.</param>
        public void Begin(string path, string name)
        {
            Filename = Path.Combine(path, name);
            m_path = path;

            // check directory
            if (!Directory.Exists(m_path))
            {
                // no log directory exists yet -create >
                Directory.CreateDirectory(m_path);
            }

            // Delete existing log file when overwrite mode is active
            if (LogMode == Mode.Override)
            {
                try
                {
                    File.Delete(Filename);
                }
                catch
                {
                }
            }

            // create thread
            m_workerThread = new Thread(new ThreadStart(ThreadProc))
            {

                // Background thread
                IsBackground = true
            };

            m_workerThread.Start();
        }

        /// <summary>
        /// Starts logging
        /// </summary>
        /// <param name="path">The path where the log file is created.</param>
        public void Begin(string path)
        {
            Begin(path, DateTime.Today.ToString(FilePattern) + ".log");
        }

        /// <summary>
        /// Starts logging
        /// </summary>
        /// <param name="settings">The log settings</param>
        public void Begin(SettingLogItem settings)
        {
            Filename = settings.Filename;
            LogMode = (Mode)Enum.Parse(typeof(Mode), settings.Modus);
            Encoding = Encoding.GetEncoding(settings.Encoding);
            TimePattern = settings.Timepattern;
            DebugMode = settings.Debug;

            Begin(settings.Path, Filename);
        }

        /// <summary>
        /// Adds a message to the log.
        /// </summary>
        /// <param name="level">The Level.</param>
        /// <param name="message">The log message.</param>
        /// <param name="instance">Method/ function that wants to log.</param>
        /// <param name="line">The line number.</param>
        /// <param name="file">The source file.</param>
        protected virtual void Add(Level level, string message, [CallerMemberName] string instance = null, [CallerLineNumber] int? line = null, [CallerFilePath] string file = null)
        {
            foreach (var l in message?.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                lock (m_queue)
                {
                    var item = new LogItem(level, instance, l, TimePattern);
                    switch (level)
                    {
                        case Level.Error:
                        case Level.FatalError:
                        case Level.Exception:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case Level.Warning:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        default:
                            break;
                    }

                    Console.WriteLine(item.ToString().Length > m_seperatorWidth ? item.ToString().Substring(0, m_seperatorWidth - 3) + "..." : item.ToString().PadRight(Console.WindowWidth, ' '));
                    Console.ResetColor();

                    m_queue.Enqueue(item);
                }
            }
        }

        /// <summary>
        /// A dividing line with * characters
        /// </summary>
        public void Seperator()
        {
            Seperator('*');
        }

        /// <summary>
        /// A separator with custom characters
        /// </summary>
        /// <param name="sepChar">The separator.</param>
        public void Seperator(char sepChar)
        {
            Add(Level.Seperartor, "".PadRight(m_seperatorWidth, sepChar));
        }

        /// <summary>
        /// Logs an info message.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="instance">>Method/ function that wants to log.</param>
        /// <param name="line">The line number.</param>
        /// <param name="file">The source file.</param>
        public void Info(string message, [CallerMemberName] string instance = null, [CallerLineNumber] int? line = null, [CallerFilePath] string file = null)
        {
            var methodInfo = new StackTrace().GetFrame(1).GetMethod();
            var className = methodInfo.ReflectedType.Name;

            Add(Level.Info, message, $"{className}.{instance}", line, file);
        }

        /// <summary>
        /// Logs an info message.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="instance">>Method/ function that wants to log.</param>
        /// <param name="line">The line number.</param>
        /// <param name="file">The source file.</param>
        /// <param name="args">Parameter für die Formatierung der Nachricht</param>
        public void Info(string message, [CallerMemberName] string instance = null, [CallerLineNumber] int? line = null, [CallerFilePath] string file = null, params object[] args)
        {
            var methodInfo = new StackTrace().GetFrame(1).GetMethod();
            var className = methodInfo.ReflectedType.Name;

            Add(Level.Info, string.Format(message, args), $"{className}.{instance}", line, file);
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="instance">>Method/ function that wants to log.</param>
        /// <param name="line">The line number.</param>
        /// <param name="file">The source file.</param>
        public void Warning(string message, [CallerMemberName] string instance = null, [CallerLineNumber] int? line = null, [CallerFilePath] string file = null)
        {
            var methodInfo = new StackTrace().GetFrame(1).GetMethod();
            var className = methodInfo.ReflectedType.Name;

            Add(Level.Warning, message, $"{className}.{instance}", line, file);

            WarningCount++;
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="instance">>Method/ function that wants to log.</param>
        /// <param name="line">The line number.</param>
        /// <param name="file">The source file.</param>
        /// <param name="args">Parameter für die Formatierung der Nachricht</param>
        public void Warning(string message, [CallerMemberName] string instance = null, [CallerLineNumber] int? line = null, [CallerFilePath] string file = null, params object[] args)
        {
            var methodInfo = new StackTrace().GetFrame(1).GetMethod();
            var className = methodInfo.ReflectedType.Name;

            Add(Level.Warning, string.Format(message, args), $"{className}.{instance}", line, file);

            WarningCount++;
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="instance">>Method/ function that wants to log.</param>
        /// <param name="line">The line number.</param>
        /// <param name="file">The source file.</param>
        public void Error(string message, [CallerMemberName] string instance = null, [CallerLineNumber] int? line = null, [CallerFilePath] string file = null)
        {
            var methodInfo = new StackTrace().GetFrame(1).GetMethod();
            var className = methodInfo.ReflectedType.Name;

            Add(Level.Error, message, $"{className}.{instance}", line, file);

            ErrorCount++;
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="instance">>Method/ function that wants to log.</param>
        /// <param name="line">The line number.</param>
        /// <param name="file">The source file.</param>
        /// <param name="args">Parameter für die Formatierung der Nachricht</param>
        public void Error(string message, [CallerMemberName] string instance = null, [CallerLineNumber] int? line = null, [CallerFilePath] string file = null, params object[] args)
        {
            var methodInfo = new StackTrace().GetFrame(1).GetMethod();
            var className = methodInfo.ReflectedType.Name;

            Add(Level.Error, string.Format(message, args), $"{className}.{instance}", line, file);

            ErrorCount++;
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="instance">>Method/ function that wants to log.</param>
        /// <param name="line">The line number.</param>
        /// <param name="file">The source file.</param>
        public void FatalError(string message, [CallerMemberName] string instance = null, [CallerLineNumber] int? line = null, [CallerFilePath] string file = null)
        {
            var methodInfo = new StackTrace().GetFrame(1).GetMethod();
            var className = methodInfo.ReflectedType.Name;

            Add(Level.FatalError, message, $"{className}.{instance}", line, file);

            ErrorCount++;
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="instance">>Method/ function that wants to log.</param>
        /// <param name="line">The line number.</param>
        /// <param name="file">The source file.</param>
        /// <param name="args">Parameter für die Formatierung der Nachricht</param>
        public void FatalError(string message, [CallerMemberName] string instance = null, [CallerLineNumber] int? line = null, [CallerFilePath] string file = null, params object[] args)
        {
            var methodInfo = new StackTrace().GetFrame(1).GetMethod();
            var className = methodInfo.ReflectedType.Name;

            Add(Level.FatalError, string.Format(message, args), $"{className}.{instance}", line, file);

            ErrorCount++;
        }

        /// <summary>
        /// Logs an exception message.
        /// </summary>
        /// <param name="ex">The exception</param>
        /// <param name="instance">>Method/ function that wants to log.</param>
        /// <param name="line">The line number.</param>
        /// <param name="file">The source file.</param>
        public void Exception(Exception ex, [CallerMemberName] string instance = null, [CallerLineNumber] int? line = null, [CallerFilePath] string file = null)
        {
            var methodInfo = new StackTrace().GetFrame(1).GetMethod();
            var className = methodInfo.ReflectedType.Name;

            lock (m_queue)
            {
                Add(Level.Exception, ex?.Message.Trim(), $"{className}.{instance}", line, file);
                Add(Level.Exception, ex?.StackTrace != null ? ex?.StackTrace.Trim() : ex?.Message.Trim(), $"{className}.{instance}", line, file);

                ExceptionCount++;
                ErrorCount++;
            }
        }

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="instance">>Method/ function that wants to log.</param>
        /// <param name="line">The line number.</param>
        /// <param name="file">The source file.</param>
        public void Debug(string message, [CallerMemberName] string instance = null, [CallerLineNumber] int? line = null, [CallerFilePath] string file = null)
        {
            var methodInfo = new StackTrace().GetFrame(1).GetMethod();
            var className = methodInfo.ReflectedType.Name;

            if (DebugMode)
            {
                Add(Level.Debug, message, $"{className}.{instance}", line, file);
            }
        }

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="instance">>Method/ function that wants to log.</param>
        /// <param name="line">The line number.</param>
        /// <param name="file">The source file.</param>
        /// <param name="args">Parameter für die Formatierung der Nachricht</param>
        public void Debug(string message, [CallerMemberName] string instance = null, [CallerLineNumber] int? line = null, [CallerFilePath] string file = null, params object[] args)
        {
            var methodInfo = new StackTrace().GetFrame(1).GetMethod();
            var className = methodInfo.ReflectedType.Name;

            if (DebugMode)
            {
                Add(Level.Debug, string.Format(message, args), $"{className}.{instance}", line, file);
            }
        }

        /// <summary>
        /// Stops logging.
        /// </summary>
        public void Close()
        {
            m_done = true;

            // protect file writing from concurrent access
            lock (m_path)
            {
                Flush();
            }
        }

        /// <summary>
        /// Cleans up the log.
        /// </summary>
        public void Clear()
        {
            ErrorCount = 0;
            WarningCount = 0;
            ExceptionCount = 0;
        }

        /// <summary>
        /// Writes the contents of the queue to the log.
        /// </summary>
        public void Flush()
        {
            var list = new List<LogItem>();

            // lock queue before concurrent access
            lock (m_queue)
            {
                list.AddRange(m_queue);
                m_queue.Clear();
            }

            // protect file writing from concurrent access
            if (list.Count > 0 && LogMode != Mode.Off)
            {
                lock (m_path)
                {
                    using var fs = new FileStream(Filename, FileMode.Append);
                    using var w = new StreamWriter(fs, Encoding);
                    foreach (var item in list)
                    {
                        var str = item.ToString();
                        w.WriteLine(str);
                    }
                }
            }
        }

        /// <summary>
        /// Thread Start Function
        /// </summary>
        private void ThreadProc()
        {
            while (!m_done)
            {
                Thread.Sleep(5000);

                // protect file writing from concurrent access
                lock (m_path)
                {
                    Flush();
                }
            }

            m_workerThread = null;
        }

        /// <summary>
        /// Writes a log entry.
        /// </summary>
        /// <typeparam name="TState">The type of object to write.</typeparam>
        /// <param name="logLevel">The entry is written at this level.</param>
        /// <param name="eventId">ID of the event.</param>
        /// <param name="state">The entry to write. Can also be an object.</param>
        /// <param name="exception">The exception that applies to this entry.</param>
        /// <param name="formatter">Function to create a string message of the state and exception parameters.</param>
        void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (logLevel == LogLevel.Error)
            {
                var message = exception?.Message ?? formatter.Invoke(state, exception);
                Error(message, "Kestrel", null, null);
            }

        }

        /// <summary>
        /// Verifies that the specified logLevel parameter is enabled.
        /// </summary>
        /// <param name="logLevel">Level to be checked.</param>
        /// <returns>True in the enabled state, false otherwise.</returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        /// <summary>
        /// Formats the message and creates a range.
        /// </summary>
        /// <typeparam name="TState">The type of object to write.</typeparam>
        /// <param name="state">The ILogger interface in which to create the scope.</param>
        /// <returns>A disposable range object. Can be NULL.</returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        /// <summary>
        /// Set file name time patterns.
        /// </summary>
        public string FilePattern { set; get; }

        /// <summary>
        /// Time patternsspecifying log entries.
        /// </summary>
        public string TimePattern { set; get; }
    }
}
