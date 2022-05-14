namespace WebExpress.WebApp.Model
{
    public class WebItem
    {
        /// <summary>
        /// Die Guid des Objektes
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Die Uri
        /// </summary>
        public virtual string Uri { get; set; }

        /// <summary>
        /// Die Bezeichnung des Objektes
        /// </summary>
        public virtual string Label { get; set; }

        /// <summary>
        /// Die Name des Objektes
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Das Bild des Objektes
        /// </summary>
        public virtual string Image { get; set; }
    }
}
