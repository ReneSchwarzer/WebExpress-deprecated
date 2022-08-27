using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebExpress.WebApp.WebNotificaation
{
    /// <summary>
    /// Umwandlung des Benachrichtigungstyps von und nach JSON
    /// </summary>
    public class TypeNotificationConverter : JsonConverter<TypeNotification>
    {
        /// <summary>
        /// Lese und konvertiert JSON zu TypeNotification.
        /// </summary>
        /// <param name="reader">Der Reader</param>
        /// <param name="typeToConvert">Der Typ</param>
        /// <param name="options">Die Optionen</param>
        /// <returns></returns>
        public override TypeNotification Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return (TypeNotification)Enum.Parse(typeof(TypeNotification), reader.GetString(), true);
        }

        /// <summary>
        /// Scheift den Wert als JSON
        /// </summary>
        /// <param name="writer">Der Writer</param>
        /// <param name="type">Der Wert</param>
        /// <param name="options">Die Optionen</param>
        public override void Write(Utf8JsonWriter writer, TypeNotification type, JsonSerializerOptions options)
        {
            writer.WriteStringValue(type.ToClass());
        }
    }
}
