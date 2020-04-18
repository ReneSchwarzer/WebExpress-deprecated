using System.Text;

namespace WebExpress.Html
{
    public interface IHtml
    {
        /// <summary>
        /// In String konvertieren unter Zuhilfenahme eines StringBuilder
        /// </summary>
        /// <param name="builder">Der StringBuilder</param>
        /// <param name="deep">Die Aufrufstiefe</param>
        void ToString(StringBuilder builder, int deep);
    }
}
