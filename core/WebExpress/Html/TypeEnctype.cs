using System;
using System.Collections.Generic;
using System.Text;

namespace WebExpress.Html
{
    /// <summary>
    /// Legt fest, wie die Daten encoded werden, wenn sie zum Server übertragen werden.
    /// </summary>
    public enum TypeEnctype
    {
        /// <summary>
        /// Alle Zeichen werden encoded (Spaces wird zu "+" koncertiert und spezalzeichen in der Hexrepräsentation) 
        /// </summary>
        UrLEncoded,
        /// <summary>
        /// Keine Zeichen werden encodes. Wird verwendet, wenn Dateien übertragen werden
        /// </summary>
        None,
        /// <summary>
        /// Nur Space-Zeichen werden encodiert.
        /// </summary>
        Text,
        /// <summary>
        /// Nicht zuordbar
        /// </summary>
        Default
    }

    public static class TypeEnctypeExtensions
    {
        /// <summary>
        /// Umwandlung von String in TypeEnctype
        /// </summary>
        /// <param name="enctype">Die Kodierung</param>
        /// <returns>Die umgewandelte Kodierung</returns>
        public static TypeEnctype Convert(string enctype )
        {
            switch(enctype?.ToLower())
            {
                case "multipart/form-data":
                    return TypeEnctype.None;
                case "text/plain":
                    return TypeEnctype.Text;
                case "application/x-www-form-urlencoded":
                    return TypeEnctype.UrLEncoded;
            }

            return TypeEnctype.Default;
        }


        /// <summary>
        /// Umwandlung in Stringrepräsentation
        /// </summary>
        /// <param name="enctype">Die Kodierung</param>
        /// <returns>Die umgewandelte Kodierung</returns>
        public static string Convert(this TypeEnctype enctype)
        {
            switch (enctype)
            {
                case TypeEnctype.None:
                    return "multipart/form-data";
                case TypeEnctype.Text:
                    return "text/plain";
            }

            return "application/x-www-form-urlencoded";
        }
    }
}
