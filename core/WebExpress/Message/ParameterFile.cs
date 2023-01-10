namespace WebExpress.Message
{
    public class ParameterFile : Parameter
    {
        /// <summary>
        /// Liefert oder setzt den Inhaltstyp
        /// </summary>
        public string ContentType { get; internal set; }

        /// <summary>
        /// Liefert oder setzt die Daten
        /// </summary>
        public byte[] Data { get; internal set; }


        /// <summary>
        /// Constructor
        /// </summary>
        public ParameterFile()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">Der Schlüssel</param>
        /// <param name="value">Der Wert</param>
        /// <param name="scope">Der Gültigkeitsbereich des Parameters</param>
        public ParameterFile(string key, string value, ParameterScope scope)
            : base(key, value, scope)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">Der Schlüssel</param>
        /// <param name="value">Der Wert</param>
        /// <param name="scope">Der Gültigkeitsbereich des Parameters</param>
        public ParameterFile(string key, int value, ParameterScope scope)
            : base(key, value, scope)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">Der Schlüssel</param>
        /// <param name="value">Der Wert</param>
        /// <param name="scope">Der Gültigkeitsbereich des Parameters</param>
        public ParameterFile(string key, char value, ParameterScope scope)
            : base(key, value, scope)
        {
        }
    }
}
