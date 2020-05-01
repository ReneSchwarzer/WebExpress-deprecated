namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Spannt die Schaltfläche über die gesammte Bereite
    /// </summary>
    public enum TypeBlockButton
    {
        None,
        Block
    }

    public static class TypeBlockButtonExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="block">Der Wert, welches umgewandelt werden soll</param>
        /// <returns>Die zum Block gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeBlockButton block)
        {
            switch (block)
            {
                case TypeBlockButton.Block:
                    return "btn-block";
            }

            return string.Empty;
        }
    }
}
