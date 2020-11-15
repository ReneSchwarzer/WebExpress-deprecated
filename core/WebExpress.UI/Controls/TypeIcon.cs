namespace WebExpress.UI.Controls
{
    public enum TypeIcon
    {
        None,
        AlignCenter,
        AlignJustify,
        AlignLeft,
        AlignRight,
        At,
        Bars,
        Bomb,
        Book,
        Bug,
        Calendar,
        CalendarMinus,
        CalendarPlus,
        Car,
        ChartBar,
        Clock,
        Clone,
        Code,
        Cog,
        Comment,
        CommentAlt,
        Download,
        Edit,
        EuroSign,
        ExclamationTriangle,
        Fire,
        Folder,
        FolderOpen,
        Font,
        Forward,
        GraduationCap,
        Hashtag,
        Home,
        InfoCircle,
        Image,
        Industry,
        Info,
        Lightbulb,
        Link,
        Map,
        MapMarker,
        Microchip,
        PaperPlane,
        PlayCircle,
        Plus,
        PowerOff,
        Print,
        Save,
        ShoppingBag,
        Star,
        Stopwatch,
        Sun,
        TachometerAlt,
        ThermometerQuarter,
        Tag,
        Tags,
        Times,
        Trash,
        TrashAlt,
        Truck,
        ThumbsUp,
        Undo,
        Upload,
        Users
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
                case TypeIcon.AlignCenter:
                    return "fas fa-align-center";
                case TypeIcon.AlignJustify:
                    return "fas fa-align-justify";
                case TypeIcon.AlignLeft:
                    return "fas fa-align-left";
                case TypeIcon.AlignRight:
                    return "fas fa-align-right";
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
                case TypeIcon.Car:
                    return "fas fa-car";
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
                case TypeIcon.Edit:
                    return "fas fa-edit";
                case TypeIcon.EuroSign:
                    return "fas fa-euro-sign";
                case TypeIcon.ExclamationTriangle:
                    return "fas fa-exclamation-triangle";
                case TypeIcon.Fire:
                    return "fas fa-fire";
                case TypeIcon.Folder:
                    return "fas fa-folder";
                case TypeIcon.FolderOpen:
                    return "fas fa-folder-open";
                case TypeIcon.Font:
                    return "fas fa-font";
                case TypeIcon.Forward:
                    return "fas fa-forward";
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
                case TypeIcon.Link:
                    return "fas fa-link";
                case TypeIcon.Map:
                    return "fas fa-map";
                case TypeIcon.MapMarker:
                    return "fas fa-map-marker";
                case TypeIcon.Microchip:
                    return "fas fa-microchip";
                case TypeIcon.PaperPlane:
                    return "paper-plane";
                case TypeIcon.PlayCircle:
                    return "fas fa-play-circle";
                case TypeIcon.Plus:
                    return "fas fa-plus";
                case TypeIcon.PowerOff:
                    return "fas fa-power-off";
                case TypeIcon.Print:
                    return "fas fa-print";
                case TypeIcon.Save:
                    return "fas fa-save";
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
                case TypeIcon.Tag:
                    return "fas fa-tag";
                case TypeIcon.Tags:
                    return "fas fa-tags";
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
                case TypeIcon.Users:
                    return "fas fa-users";
            }

            return string.Empty;
        }
    }
}
