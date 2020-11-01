using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using WebExpress.Html;
using WebExpress.Messages;
using WebExpress.Pages;

namespace WebExpress.Workers
{
    /// <summary>
    /// Arbeitet eine Anfrage ab. Dies erfolgt nebenläufig
    /// </summary>
    public class WorkerPage : WorkerAuthentication
    {
        /// <summary>
        /// Liefert oder setzt den Seitentitel
        /// </summary>
        public string Titel { get; set; }

        /// <summary>
        /// Liefert oder setzt die Contentliefernde Funktion
        /// </summary>
        protected Func<Request, IHtmlNode> Content { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="url">Die URL</param>
        public WorkerPage(UriPage url)
            : base(url)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="path">Die URL</param>
        public WorkerPage(UriPage path, string content)
            : base(path)
        {
            Content = (request) =>
            {
                return new HtmlRaw(content);
            };
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="path">Die URL</param>
        public WorkerPage(UriPage path, IPage content)
            : base(path)
        {
            Content = (request) =>
            {
                return content.Render();
            };
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Antwort</returns>
        public override Response Process(Request request)
        {
            try
            {
                var content = Content == null ?
                        new HtmlRaw("<html><body</body></html>") :
                        Content(request);

                return new ResponseOK()
                {
                    Content = content
                };
            }
            catch (RedirectException ex)
            {
                if (ex.Permanet)
                {
                    return new ResponseRedirectPermanentlyMoved(ex.Url);
                }

                return new ResponseRedirectTemporarilyMoved(ex.Url);
            }
        }
    }

    /// <summary>
    /// Arbeitet eine Anfrage ab. Dies erfolgt nebenläufig
    /// </summary>
    public class WorkerPage<T> : WorkerPage where T : IPage, new()
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="uri">Die Uri</param>
        public WorkerPage(UriPage uri)
            : base(uri)
        {
            var dict = new Dictionary<int, UriPathSegmentVariable>();
            var index = 0;

            foreach (var segment in Uri.Path.Where(x => x is UriPathSegmentPage).Select(x => x as UriPathSegmentPage))
            {
                if (uri.Variables.ContainsKey(segment.SegmentID))
                {
                    var variable = uri.Variables[segment.SegmentID];

                    foreach (var item in variable.Items)
                    {
                        dict.Add(index++, item);
                    }
                }
            }

            Content = (request) =>
            {
                var newUri = new UriPage(uri);
                var session = CurrentSession;
                if (session == null)
                {
                    return null;
                }

                if (session.GetProperty<SessionPropertyParameter>() == null)
                {
                    session.SetProperty(new SessionPropertyParameter());
                }

                var page = new T() { Request = request, Context = Context };

                if (dict.Count > 0)
                {
                    var paramters = new Dictionary<string, string>();
                    var raw = newUri.ToRawString().Substring(Context.UrlBasePath.Length);
                    var group = Regex.Match(request.URL, raw).Groups;

                    // Parameter
                    foreach (var v in dict)
                    {
                        try
                        {
                            // Parameter
                            var key = v.Value.Name;
                            var value = group[v.Key + 1].ToString();

                            if (!paramters.ContainsKey(key))
                            {
                                page.AddParam(key, value, ParameterScope.Url);
                                paramters.Add(key, value);
                            }
                            else
                            {
                                Context.Log.Warning(MethodBase.GetCurrentMethod(), string.Format("Parameter '{0}' ist mehrfach in Uri '{1}' vorhanden", key, request.URL));
                            }
                         }
                        catch
                        {
                        }
                    }

                    // Uri
                    var split = request.URL.Substring(Context.UrlBasePath.Length).Split('/', StringSplitOptions.RemoveEmptyEntries);
                    var i = 0;

                    foreach (var segment in newUri.Path.Select(x => x as UriPathSegment))
                    {
                        if (!string.IsNullOrWhiteSpace(segment.Value))
                        {
                            segment.Value = split[i++];
                        }
                    }
                    
                    foreach (var segment in newUri.Path.Where(x => x is UriPathSegmentPage).Select(x => x as UriPathSegmentPage))
                    {
                        if (newUri.Variables.ContainsKey(segment.SegmentID))
                        {
                            var variable = uri.Variables[segment.SegmentID];

                            segment.Display = variable.Display.ToString(paramters);
                        }
                    }
                }

                page.Init(newUri, session);
                page.Process();

                return page.Render();
            };
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="uri">Die Uri</param>
        /// <param name="content">Der Inhalt</param>
        public WorkerPage(UriPage uri, IPage content)
            : base(uri)
        {
            Content = (request) =>
            {
                return content.Render();
            };
        }
    }
}
