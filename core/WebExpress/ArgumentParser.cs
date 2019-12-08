using System;
using System.Collections.Generic;
using System.Linq;

namespace WebExpress
{
    /// <summary>
    /// Parst die Übergabeargumente
    /// </summary>
    public class ArgumentParser
    {
        private static ArgumentParser m_this = null;

        private List<ArgumentParserCommand> Commands { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ArgumentParser()
        {
            Commands = new List<ArgumentParserCommand>();
        }

        /// <summary>
        /// Registriert ein Befehl
        /// </summary>
        /// <param name="command">Der zu registrierende Befehl</param>
        public void Register(ArgumentParserCommand command)
        {
            Commands.Add(command);
        }

        /// <summary>
        /// Programmargumente aufbereiten
        /// Ein Programmargument besteht aus einem Befehl und einen Wert. 
        /// Der Wert kann die leere Zeichenfolge enthalten z.B. -help.
        /// Befehle, die mit -- beginnen werden als Kommentare gewertet und 
        /// nicht weiter betrachtet
        /// </summary>
        /// <param name="args">Programmargumente</param>
        /// <returns>Verzeichnis mit den aufbereiteten Programmargumenten</returns>
        public ArguemtParserResult Parse(string[] args)
        {
            var argsDict = new ArguemtParserResult();

            var key = "";
            var value = "";

            foreach (var s in args)
            {
                if (s.StartsWith("--") == true)
                {
                }
                else if (s.StartsWith("-") == true)
                {
                    if (!string.IsNullOrEmpty(key))
                    {
                        var command = (from x in Commands
                                       where x.FullName.Equals(key.Substring(1), StringComparison.OrdinalIgnoreCase) ||
                                             x.ShortName.Equals(key.Substring(1), StringComparison.OrdinalIgnoreCase)
                                       select x).FirstOrDefault();

                        if (command != null)
                        {
                            argsDict.Add(command.FullName.ToLower(), value.Trim());
                        }

                        value = "";
                    }
                    key = s;
                }
                else
                {
                    value += " " + s;
                }
            }

            if (!string.IsNullOrEmpty(key))
            {
                var command = (from x in Commands
                               where x.FullName.Equals(key.Substring(1), StringComparison.OrdinalIgnoreCase) ||
                                     x.ShortName.Equals(key.Substring(1), StringComparison.OrdinalIgnoreCase)
                               select x).FirstOrDefault();

                if (command != null)
                {
                    argsDict.Add(command.FullName.ToLower(), value.Trim());
                }
            }

            return argsDict;
        }

        /// <summary>
        /// Liefert die erkannten Argumente
        /// </summary>
        /// <param name="args">Programmargumente</param>
        /// <returns></returns>
        public string GetValidArguments(string[] args)
        {
            var argumentDict = Parse(args);

            var v = from x in argumentDict
                    select "-" + x.Key + (string.IsNullOrWhiteSpace(x.Value) ? "" : " " + x.Value);

            return string.Join(" ", v);
        }

        /// <summary>
        /// Liefert das aktuelle ArgumentParser-Objekt 
        /// </summary>
        public static ArgumentParser Current
        {
            get
            {
                if (m_this == null)
                {
                    m_this = new ArgumentParser();
                }

                return m_this;
            }
        }
    }
}
