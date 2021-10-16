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
        /// Konstruktor
        /// </summary>
        public ParameterFile()
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="key">Der Schlüssel</param>
        /// <param name="value">Der Wert</param>
        public ParameterFile(string key, string value)
            : base(key, value)
        {
            Key = key.ToLower();
            Value = value;
            Scope = ParameterScope.Parameter;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="key">Der Schlüssel</param>
        /// <param name="value">Der Wert</param>
        public ParameterFile(string key, int value)
            : base(key, value)
        {
            Key = key.ToLower();
            Value = value.ToString();
            Scope = ParameterScope.Parameter;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="key">Der Schlüssel</param>
        /// <param name="value">Der Wert</param>
        public ParameterFile(string key, char value)
            : base(key, value)
        {
            Key = key.ToLower();
            Value = value.ToString();
            Scope = ParameterScope.Parameter;
        }
    }
}
