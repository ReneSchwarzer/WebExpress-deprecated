using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Message;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebResource
{
    public abstract class ResourceRestCrud : ResourceRest
    {
        /// <summary>
        /// Verarbeitung des GET-Request
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Eine Aufzählung, welche JsonSerializer serialisiert werden kann.</returns>
        public abstract IEnumerable<object> GetColumns(Request request);

        /// <summary>
        /// Verarbeitung des GET-Request
        /// </summary>
        /// <param name="id">Die ID oder null wenn nicht gefiltert werden soll</param>
        /// <param name="search">Ein Suchstring oder null wenn nicht gefiltert werden soll</param>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Eine Aufzählung, welche JsonSerializer serialisiert werden kann.</returns>
        public abstract IEnumerable<object> GetData(string id, string search, Request request);

        /// <summary>
        /// Verarbeitung des GET-Request
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Eine Aufzählung, welche JsonSerializer serialisiert werden kann.</returns>
        public override object GetData(Request request)
        {
            var itemCount = 50;
            var id = request.GetParameter("id");
            var search = request.GetParameter("search");
            var page = request.GetParameter("page");
            var columns = request.HasParameter("columns");
            var pagenumber = !string.IsNullOrWhiteSpace(page?.Value) ? Convert.ToInt32(page?.Value) : 0;

            if (columns)
            {
                return new { Columns = GetColumns(request) };
            }

            var data = GetData(id?.Value, search?.Value.ToLower(), request);

            var count = data.Count();
            var totalpage = Math.Round(count / (double)itemCount, MidpointRounding.ToEven);

            if (page == null)
            {
                return new { Data = data };
            }

            return new { Data = data.Skip(itemCount * pagenumber).Take(itemCount), Pagination = new { PageNumber = pagenumber, Totalpage = totalpage } };
        }
    }
}
