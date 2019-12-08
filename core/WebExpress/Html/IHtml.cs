using System.Text;

namespace WebServer.Html
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
