﻿using System;
using System.Text.Json.Serialization;

namespace WebExpress.WebApp.Model
{
    public class WebItem
    {
        /// <summary>
        /// Returns or sets the guid. des Objektes
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Die Uri
        /// </summary>
        [JsonPropertyName("uri")]
        public virtual string Uri { get; set; }

        /// <summary>
        /// Die Bezeichnung des Objektes
        /// </summary>
        [JsonPropertyName("label")]
        public virtual string Label { get; set; }

        /// <summary>
        /// Die Name des Objektes
        /// </summary>
        [JsonPropertyName("name")]
        public virtual string Name { get; set; }

        /// <summary>
        /// Das Bild des Objektes
        /// </summary>
        [JsonPropertyName("image")]
        public virtual string Image { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public WebItem()
        {
        }

        /// <summary>
        /// Copy-Constructor
        /// Erstellt eine Tiefenkopie.
        /// </summary>
        /// <param name="item">Das zu kopierende Objekt</param>
        public WebItem(WebItem item)
        {
            Id = item.Id;
            Uri = item.Uri;
            Label = item.Label;
            Name = item.Name;
            Image = item.Image;
        }

        /// <summary>
        /// Conversion to string.form
        /// </summary>
        /// <returns>Das Objekt in seiner Stringrepräsentation</returns>
        public override string ToString()
        {
            return $"{Name} ({Id})";
        }
    }
}