using System;
using System.Collections.Generic;
using System.Globalization;
using WebExpress.WebResource;

namespace WebExpress.Attribute
{
    public class SegmentUIntAttribute : System.Attribute, IResourceAttribute, ISegmentAttribute
    {
        /// <summary>
        /// Liefert oder setzt den Namen der Variable
        /// </summary>
        private string VariableName { get; set; }

        /// <summary>
        /// Liefert oder setzt den Anzeigestring
        /// </summary>
        private string Display { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="variableName">Der Name der Variable</param>
        /// <param name="display">Der Anzeigestring</param>
        public SegmentUIntAttribute(string variableName, string display)
        {
            VariableName = variableName;
            Display = display;
        }

        /// <summary>
        /// Umwandlung in ein Pfadsegment
        /// </summary>
        /// <returns>Das Pfadsegment</returns>
        public IPathSegment ToPathSegment()
        {
            var expression = @"^\d$";

            var callBackDisplay = new Func<string, string, CultureInfo, string>((segment, moduleID, culture) =>
            {
                return Display;
            });

            var callBackValiables = new Func<string, IDictionary<string, string>>(segment =>
            {
                var dict = new Dictionary<string, string>
                {
                    { VariableName, segment }
                };

                return dict;
            });

            return new PathSegmentVariable(expression, callBackDisplay, callBackValiables);
        }
    }
}
