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
        public TypeFavicon Mediatype { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="url">Die URL</param>
        /// <param name="mediatype">Den Mediatyp</param>
        public Favicon(string url, TypeFavicon mediatype)
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
                    Mediatype = TypeFavicon.ICON;
                    break;
                case "image/jpg":
                    Mediatype = TypeFavicon.JPG;
                    break;
                case "image/png":
                    Mediatype = TypeFavicon.PNG;
                    break;
                case "image/svg+xml":
                    Mediatype = TypeFavicon.SVG;
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
                case TypeFavicon.ICON:
                    return "image/x-icon";
                case TypeFavicon.JPG:
                    return "image/jpg";
                case TypeFavicon.PNG:
                    return "image/png";
                case TypeFavicon.SVG:
                    return "image/svg+xml";
            }

            return "";
        }
    }
}
