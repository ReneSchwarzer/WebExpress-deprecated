using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Message;
using WebExpress.WebApp.Wql;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebResource
{
    public abstract class ResourceRestCrud<T> : ResourceRest where T : class, new()
    {
        /// <summary>
        /// Liefert oder setzt das Speerobjekt
        /// </summary>
        protected object Guard { get; set; }

        /// <summary>
        /// Processing of the resource. des GET-Request
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Eine Aufzählung, welche JsonSerializer serialisiert werden kann.</returns>
        public abstract IEnumerable<object> GetColumns(Request request);

        /// <summary>
        /// Processing of the resource. des GET-Request
        /// </summary>
        /// <param name="wql">Der Filter</param>
        /// <param name="request">The request.</param>
        /// <returns>Eine Aufzählung, welche JsonSerializer serialisiert werden kann.</returns>
        public abstract IEnumerable<T> GetData(WqlStatement wql, Request request);

        /// <summary>
        /// Processing of the resource. des GET-Request
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Eine Aufzählung, welche JsonSerializer serialisiert werden kann.</returns>
        public override object GetData(Request request)
        {
            var itemCount = 50;
            var wql = request.HasParameter("wql") ? request.GetParameter("wql").Value : string.Empty;
            var page = request.GetParameter("page");
            var columns = request.HasParameter("columns");
            var pagenumber = !string.IsNullOrWhiteSpace(page?.Value) ? Convert.ToInt32(page?.Value) : 0;

            if (columns)
            {
                return new { Columns = GetColumns(request) };
            }

            lock (Guard ?? new object())
            {
                var data = GetData(new WqlStatement(wql), request);

                var count = data.Count();
                var totalpage = Math.Round(count / (double)itemCount, MidpointRounding.ToEven);

                if (page == null)
                {
                    return new { Data = data };
                }

                return new { data = data.Skip(itemCount * pagenumber).Take(itemCount), pagination = new { pagenumber = pagenumber, totalpage = totalpage } };
            }
        }
    }
}
