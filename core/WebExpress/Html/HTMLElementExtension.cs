using System;
using System.Collections.Generic;
using System.Linq;

namespace WebExpress.Html
{
    /// <summary>
    /// Erweiterungsmethoden für HTMLElemente
    /// </summary>
    public static class HTMLElementExtension
    {
        /// <summary>
        /// Fügt eine Klasse Hinzu
        /// </summary>
        /// <param name="html">Das zu erweiternde HTML-Element</param>
        /// <param name="cssClass">Die Klasse, welche hinzugefügt werden soll</param>
        /// <returns>Des um die Kasse erweiterte HTML-Element</returns>
        public static IHtmlNode AddClass(this IHtmlNode html, string cssClass)
        {
            if (html is HtmlElement)
            {
                var element = html as HtmlElement;

                var list = new List<string>(element.Class.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)).Select(x => x.ToLower()).ToList();

                if (!list.Contains(cssClass.ToLower()))
                {
                    list.Add(cssClass.ToLower());
                }

                var css = string.Join(" ", list);

                element.Class = css;
            }

            return html;
        }

    }
}
