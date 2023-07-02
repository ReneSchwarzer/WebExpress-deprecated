using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.WebApp.Wql;
using WebExpress.WebComponent;
using WebExpress.WebMessage;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebResource
{
    public abstract class ResourceRestCrud<T> : ResourceRest where T : class, new()
    {
        /// <summary>
        /// Returns or sets the lock object.
        /// </summary>
        protected object Guard { get; set; }

        /// <summary>
        /// Processing of the resource that was called via the get request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>An enumeration of which json serializer can be serialized.</returns>
        public abstract IEnumerable<object> GetColumns(Request request);

        /// <summary>
        /// Processing of the resource that was called via the get request.
        /// </summary>
        /// <param name="wql">The filtering and sorting options.</param>
        /// <param name="request">The request.</param>
        /// <returns>An enumeration of which json serializer can be serialized.</returns>
        public abstract IEnumerable<T> GetData(WqlStatement wql, Request request);

        /// <summary>
        /// Processing of the resource that was called via the get request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>An enumeration of which json serializer can be serialized.</returns>
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
                var wqlStatement = ComponentManager.GetComponent<WqlManager>().Parser.Parse<T>(wql);
                var data = GetData(wqlStatement, request);

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
