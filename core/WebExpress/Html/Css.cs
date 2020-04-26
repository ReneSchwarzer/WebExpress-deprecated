using System.Linq;

namespace WebExpress.Html
{
    public static class Css
    {
        public static string Concatenate(params string[] values)
        {
            return string.Join(" ", values.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct());
        }
    }
}
