namespace WebExpress.UI.Controls
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
            switch (value)
            {
                case TypeOnChange.Submit:
                    return "this.form.submit()";
            }

            return string.Empty;
        }
    }
}
