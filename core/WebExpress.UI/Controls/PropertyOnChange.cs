namespace WebExpress.UI.Controls
{
    public class PropertyOnChange
    {
        /// <summary>
        /// Der System-Wert
        /// </summary>
        public TypeOnChange SystemValue { get; protected set; }

        /// <summary>
        /// Der benutzerdefinierte Wert
        /// </summary>
        public string UserValue { get; protected set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="value">Der System-Wert</param>
        public PropertyOnChange(TypeOnChange value)
        {
            SystemValue = value;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="value">Der benutzerdefinierte Wert</param>
        public PropertyOnChange(string value)
        {
            SystemValue = TypeOnChange.None;
            UserValue = value;
        }

        /// <summary>
        /// Umwandlung in String
        /// </summary>
        /// <returns>Die Stringrepräsentation</returns>
        public override string ToString()
        {
            if (SystemValue != TypeOnChange.None)
            {
                return SystemValue.ToValue();
            }
            else if (!string.IsNullOrWhiteSpace(UserValue))
            {
                return UserValue;
            }

            return null;
        }
    }
}
