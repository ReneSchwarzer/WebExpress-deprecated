using System;
using WebExpress.WebUri;

namespace WebExpress.WebAttribute
{
    public class WebExSegmentGuidAttribute : Attribute, IResourceAttribute, ISegmentAttribute
    {
        /// <summary>
        /// The display formats of the Guid.
        /// </summary>
        public enum Format { Full, Simple }

        /// <summary>
        /// Returns or sets the name of the variable.
        /// </summary>
        private string VariableName { get; set; }

        /// <summary>
        /// Returns or sets the display string.
        /// </summary>
        private string Display { get; set; }


        /// <summary>
        /// Delivers or sets the display format.
        /// </summary>
        private Format DisplayFormat { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="variableName">The name of the variable.</param>
        /// <param name="display">The display string.</param>
        /// <param name="displayFormat">The display format.</param>
        public WebExSegmentGuidAttribute(string variableName, string display, Format displayFormat = Format.Simple)
        {
            VariableName = variableName;
            Display = display;
            DisplayFormat = displayFormat;
        }

        /// <summary>
        /// Conversion to a path segment.
        /// </summary>
        /// <returns>The path segment.</returns>
        public IUriPathSegment ToPathSegment()
        {
            //var expression = @"^(\{){0,1}(([0-9a-fA-F]{8})\-([0-9a-fA-F]{4})\-([0-9a-fA-F]{4})\-([0-9a-fA-F]{4})\-([0-9a-fA-F]{12}))(\}){0,1}$";

            //var callBackDisplay = new Func<string, string, CultureInfo, string>((segment, moduleId, culture) =>
            //{
            //    var match = Regex.Match(segment, expression, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            //    if (match.Success && string.IsNullOrWhiteSpace(Display))
            //    {
            //        return match.Groups[7].ToString();
            //    }
            //    else if (match.Success)
            //    {
            //        var display = string.Format
            //        (
            //            InternationalizationManager.I18N(culture, moduleId, Display),
            //            DisplayFormat == Format.Simple ? match.Groups[7].ToString() : match.Groups[2].ToString()
            //        );

            //        return display;
            //    }

            //    return null;
            //});



            return new UriPathSegmentVariableGuid(VariableName, Display);
        }
    }
}
