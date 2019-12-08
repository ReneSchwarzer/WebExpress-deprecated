namespace WebExpress.Html
{
    public class Favicon
    {
        /// <summary>
        /// Liefert oder setzt die URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Liefert oder setzt den Mediatyp
        /// </summary>
        public TypesFavicon Mediatype { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="url">Die URL</param>
        /// <param name="mediatype">Den Mediatyp</param>
        public Favicon(string url, TypesFavicon mediatype)
        {
            Url = url;
            Mediatype = mediatype;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="url">Die URL</param>
        /// <param name="mediatype">Den Mediatyp</param>
        public Favicon(string url, string mediatype)
        {
            Url = url;

            switch (mediatype)
            {
                case "image/x-icon":
                    Mediatype = TypesFavicon.ICON;
                    break;
                case "image/jpg":
                    Mediatype = TypesFavicon.JPG;
                    break;
                case "image/png":
                    Mediatype = TypesFavicon.PNG;
                    break;
                case "image/svg+xml":
                    Mediatype = TypesFavicon.SVG;
                    break;
            }
        }

        /// <summary>
        /// Liefert den Medientyp
        /// </summary>
        /// <returns></returns>
        public string GetMediatyp()
        {
            switch (Mediatype)
            {
                case TypesFavicon.ICON:
                    return "image/x-icon";
                case TypesFavicon.JPG:
                    return "image/jpg";
                case TypesFavicon.PNG:
                    return "image/png";
                case TypesFavicon.SVG:
                    return "image/svg+xml";
            }

            return "";
        }
    }
}
