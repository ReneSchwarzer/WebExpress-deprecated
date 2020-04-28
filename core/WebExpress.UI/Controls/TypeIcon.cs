namespace WebExpress.UI.Controls
{
    public enum TypeIcon
    {
        None,
        At,
        Bars,
        Bomb,
        Book,
        Bug,
        Calendar,
        CalendarMinus,
        CalendarPlus,
        ChartBar,
        Clock, 
        Clone,
        Code,
        Cog,
        Comment,
        CommentAlt,
        Download,
        EuroSign,
        ExclamationTriangle,
        Fire,
        GraduationCap,
        Hashtag,
        Home,
        InfoCircle,
        Image,
        Industry,
        Info,
        Lightbulb,
        Map,
        MapMarker,
        Microchip,
        PlayCircle,
        Plus,
        PowerOff,
        ShoppingBag,
        Star,
        Stopwatch,
        Sun,
        TachometerAlt,
        ThermometerQuarter,
        Times,
        Trash,
        TrashAlt,
        Truck,
        ThumbsUp,
        Undo,
        Upload,
        UserIcon
    }


    public static class TypeIconExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="icon">Das Icon, welches umgewandelt werden soll</param>
        /// <returns>Die zum Icon gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeIcon icon)
        {
            switch (icon)
            {
                case TypeIcon.At:
                    return "fas fa-at";
                case TypeIcon.Bars:
                    return "fas fa-bars";
                case TypeIcon.Bomb:
                    return "fas fa-bomb";
                case TypeIcon.Book:
                    return "fas fa-book";
                case TypeIcon.Bug:
                    return "fas fa-bug";
                case TypeIcon.Calendar:
                    return "fas fa-calendar";
                case TypeIcon.CalendarMinus:
                    return "fas fa-calendar-minus";
                case TypeIcon.CalendarPlus:
                    return "fas fa-calendar-plus";
                case TypeIcon.ChartBar:
                    return "fas fa-chart-bar";
                case TypeIcon.Clock:
                    return "fas fa-clock";
                case TypeIcon.Clone:
                    return "fas fa-clone";
                case TypeIcon.Code:
                    return "fas fa-code";
                case TypeIcon.Cog:
                    return "fas fa-cog";
                case TypeIcon.Comment:
                    return "fas fa-comment";
                case TypeIcon.CommentAlt:
                    return "fas fa-comment-alt";
                case TypeIcon.Download:
                    return "fas fa-upload";
                case TypeIcon.EuroSign:
                    return "fas fa-euro-sign";
                case TypeIcon.ExclamationTriangle:
                    return "fas fa-exclamation-triangle";
                case TypeIcon.Fire:
                    return "fas fa-fire";
                case TypeIcon.GraduationCap:
                    return "fas fa-graduation-cap";
                case TypeIcon.Hashtag:
                    return "fas fa-hashtag";
                case TypeIcon.Home:
                    return "fas fa-home";
                case TypeIcon.InfoCircle:
                    return "fas fa-info-circle";
                case TypeIcon.Image:
                    return "fas fa-image";
                case TypeIcon.Industry:
                    return "fas fa-industry";
                case TypeIcon.Info:
                    return "fas fa-info";
                case TypeIcon.Lightbulb:
                    return "fas fa-lightbulb";
                case TypeIcon.Map:
                    return "fas fa-map";
                case TypeIcon.MapMarker:
                    return "fas fa-map-marker";
                case TypeIcon.Microchip:
                    return "fas fa-microchip";
                case TypeIcon.PlayCircle:
                    return "fas fa-play-circle";
                case TypeIcon.Plus:
                    return "fas fa-plus";
                case TypeIcon.PowerOff:
                    return "fas fa-power-off";
                case TypeIcon.ShoppingBag:
                    return "fas fa-shopping-bag";
                case TypeIcon.Star:
                    return "fas fa-star";
                case TypeIcon.Stopwatch:
                    return "fas fa-stopwatch";
                case TypeIcon.Sun:
                    return "fas fa-sun";
                case TypeIcon.TachometerAlt:
                    return "fas fa-tachometer-alt";
                case TypeIcon.ThermometerQuarter:
                    return "fas fa-thermometer-quarter";
                case TypeIcon.Times:
                    return "fas fa-times";
                case TypeIcon.Trash:
                    return "fas fa-trash";
                case TypeIcon.TrashAlt:
                    return "fas fa-trash-alt";
                case TypeIcon.Truck:
                    return "fas fa-truck";
                case TypeIcon.ThumbsUp:
                    return "fas fa-thumbs-up";
                case TypeIcon.Undo:
                    return "fas fa-undo";
                case TypeIcon.Upload:
                    return "fas fa-download";
            }

            return string.Empty;
        }
    }
}
