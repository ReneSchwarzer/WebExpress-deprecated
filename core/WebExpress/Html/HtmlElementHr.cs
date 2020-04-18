using System.Text;

namespace WebExpress.Html
{
    public class HtmlElementHr : HtmlElement
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementHr()
            : base("hr", false)
        {

        }

        /// <summary>
        /// In String konvertieren unter Zuhilfenahme eines StringBuilder
        /// </summary>
        /// <param name="builder">Der StringBuilder</param>
        /// <param name="deep">Die Aufrufstiefe</param>
        public override void ToString(StringBuilder builder, int deep)
        {
            base.ToString(builder, deep);
        }
    }
}
