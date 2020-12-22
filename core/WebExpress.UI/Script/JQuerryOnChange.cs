namespace WebExpress.UI.Script
{
    public class JQuerryOnChange : JQuerryOn
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="selector">Der Selektor</param>
        public JQuerryOnChange(string selector)
            : base(selector, "change")
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="selector">Der Selektor</param>
        /// <param name="functions">Die Funktionen</param>
        public JQuerryOnChange(string selector, params string[] functions)
            : base(selector, "change")
        {
            Function.AddRange(functions);
        }
    }
}
