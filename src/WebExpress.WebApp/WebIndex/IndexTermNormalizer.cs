using System.Collections.Generic;
using System.Linq;

namespace WebExpress.WebApp.WebIndex
{
    public static class IndexTermNormalizer
    {
        public static IEnumerable<IndexTermToken> Normalize(IEnumerable<IndexTermToken> input)
        {
            return input.Select(x => new IndexTermToken()
            {
                Position = x.Position,
                Value = Normalize(x.Value)
            });
        }

        public static string Normalize(string input)
        {
            var str = input?.ToLower();

            str = str?.Replace("ä", "ae");
            str = str?.Replace("ö", "oe");
            str = str?.Replace("ü", "ue");
            str = str?.Replace("ß", "ss");
            str = str?.Replace("á", "a");
            str = str?.Replace("â", "a");
            str = str?.Replace("à", "a");
            str = str?.Replace("é", "e");
            str = str?.Replace("ê", "e");
            str = str?.Replace("è", "e");
            str = str?.Replace("í", "i");
            str = str?.Replace("î", "i");
            str = str?.Replace("ì", "i");
            str = str?.Replace("ó", "o");
            str = str?.Replace("ô", "o");
            str = str?.Replace("ò", "o");
            str = str?.Replace("ú", "u");
            str = str?.Replace("û", "u");
            str = str?.Replace("ù", "u");

            return str;
        }
    }
}
