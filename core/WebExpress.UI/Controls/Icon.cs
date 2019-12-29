using System;
using System.Collections.Generic;
using System.Text;

namespace WebExpress.UI.Controls
{
    public enum Icon
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
        Code,
        Cog,
        Comment,
        CommentAlt,
        Download,
        EuroSign,
        ExclamationTriangle,
        Fire,
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
        Upload
    }

    
    public static class IconExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="icon">Das Icon, welches umgewandelt werden soll</param>
        /// <returns>Die zum Icon gehörende CSS-KLasse</returns>
        public static string ToClass(this Icon icon)
        {
            switch (icon)
            {
                case Icon.At:
                    return "fas fa-at";
                case Icon.Bars:
                    return "fas fa-bars";
                case Icon.Bomb:
                    return "fas fa-bomb";
                case Icon.Book:
                    return "fas fa-book";
                case Icon.Bug:
                    return "fas fa-bug";
                case Icon.Calendar:
                    return "fas fa-calendar-plus";
                case Icon.CalendarMinus:
                    return "fas fa-calendar-minus";
                case Icon.CalendarPlus:
                    return "fas fa-calendar-plus";
                case Icon.ChartBar:
                    return "fas fa-chart-bar";
                case Icon.Clock:
                    return "fas fa-clock";
                case Icon.Code:
                    return "fas fa-code";
                case Icon.Cog:
                    return "fas fa-cog";
                case Icon.Comment:
                    return "fas fa-comment";
                case Icon.CommentAlt:
                    return "fas fa-comment-alt";
                case Icon.Download:
                    return "fas fa-upload";
                case Icon.EuroSign:
                    return "fas fa-euro-sign";
                case Icon.ExclamationTriangle:
                    return "fas fa-exclamation-triangle";
                case Icon.Fire:
                    return "fas fa-fire";
                case Icon.Hashtag:
                    return "fas fa-hashtag";
                case Icon.Home:
                    return "fas fa-home";
                case Icon.InfoCircle:
                    return "fas fa-info-circle";
                case Icon.Image:
                    return "fas fa-image";
                case Icon.Industry:
                    return "fas fa-industry";
                case Icon.Info:
                    return "fas fa-info";
                case Icon.Lightbulb:
                    return "fas fa-lightbulb";
                case Icon.Map:
                    return "fas fa-map";
                case Icon.MapMarker:
                    return "fas fa-map-marker";
                case Icon.Microchip:
                    return "fas fa-microchip";
                case Icon.PowerOff:
                    return "fas fa-power-off";
                case Icon.ShoppingBag:
                    return "fas fa-shopping-bag";
                case Icon.Star:
                    return "fas fa-star";
                case Icon.Stopwatch:
                    return "fas fa-stopwatch";
                case Icon.Sun:
                    return "fas fa-sun";
                case Icon.TachometerAlt:
                    return "fas fa-tachometer-alt";
                case Icon.ThermometerQuarter:
                    return "fas fa-thermometer-quarter";
                case Icon.Times:
                    return "fas fa-times";
                case Icon.Trash:
                    return "fas fa-trash";
                case Icon.TrashAlt:
                    return "fas fa-trash-alt";
                case Icon.Truck:
                    return "fas fa-truck";
                case Icon.ThumbsUp:
                    return "fas fa-thumbs-up";
                case Icon.Undo:
                    return "fas fa-undo";
                case Icon.Upload:
                    return "fas fa-download";
            }

            return string.Empty;
        }
    }
}
