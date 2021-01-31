namespace WebExpress.UI.WebControl
{
    public enum TypeOnChange
    {
        None,
        Submit
    }


    public static class TypeOnChangeExtensions
    {
        /// <summary>
        /// Umwandlung in String
        /// </summary>
        /// <param name="value">Der Wert</param>
        /// <returns>Der String</returns>
        public static string ToValue(this TypeOnChange value)
        {
            return value switch
            {
                TypeOnChange.Submit => "this.form.submit()",
                _ => string.Empty,
            };
        }
    }
}
