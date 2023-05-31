using System;
using WebExpress.WebMessage;
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
        /// Constructor
        /// </summary>
        /// <param name="parameter">The type of the variable.</param>
        /// <param name="display">The display string.</param>
        /// <param name="displayFormat">The display format.</param>
        public WebExSegmentGuidAttribute(Type parameter, string display, Format displayFormat = Format.Simple)
        {
            VariableName = (Activator.CreateInstance(parameter) as Parameter)?.Key?.ToLower();
            Display = display;
            DisplayFormat = displayFormat;
        }

        /// <summary>
        /// Conversion to a path segment.
        /// </summary>
        /// <returns>The path segment.</returns>
        public IUriPathSegment ToPathSegment()
        {
            return new UriPathSegmentVariableGuid(VariableName, Display);
        }
    }
}
