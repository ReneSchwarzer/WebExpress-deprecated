using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        protected Func<Request, string> Content { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="url">Die URL</param>
        public WorkerPage(Path url)
            : base(url)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="path">Die URL</param>
        public WorkerPage(Path path, string content)
            : base(path)
        {
            Content = (request) =>
            {
                return content;
            };
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="path">Die URL</param>
        public WorkerPage(Path path, IPage content)
            : base(path)
        {
            Content = (request) =>
            {
                return content.ToString();
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
                        "<html><body</body></html>" :
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
        /// <param name="path">Die URL</param>
        public WorkerPage(Path path)
            : base(path)
        {
            var dict = new Dictionary<int, KeyValuePair<string, PathItem>>();
            var index = 0;
            Path = path;

            foreach (var item in path.Items)
            {
                if (item.Fragment != null)
                {
                    foreach (Match match in Regex.Matches(item.Fragment, @"\$[0-9A-Za-z]+"))
                    {
                        dict.Add(index++, new KeyValuePair<string, PathItem>(match.Value, item));
                    }
                }
            }

            Content = (request) =>
            {
                var p = new Path(path);
                var session = CurrentSession;
                if (session == null) return null;

                if (session.GetProperty<SessionPropertyParameter>() == null)
                {
                    session.SetProperty(new SessionPropertyParameter());
                }

                var page = new T() { Request = request };

                if (dict.Count > 0)
                {
                    var url = Regex.Replace(p.ToString(), @"\$[0-9A-Za-z]+", "([0-9A-Za-z.]*)");
                    var group = Regex.Match(request.URL, url).Groups;

                    // Url-Parameter
                    foreach (var v in dict)
                    {
                        try
                        {
                            var value = group[v.Key + 1].ToString();
                            var key = v.Value.Key;
                            var item = v.Value.Value;

                            page.AddParam(key.Substring(1), value, ParameterScope.Url);

                            var i = p.Items.Where(x => x.Name == item.Name && x.Fragment == item.Fragment && x.Tag == item.Tag).FirstOrDefault();

                            i.Name = item.Name.Replace(key, value);
                            i.Fragment = item.Fragment.Replace(key, value);
                        }
                        catch
                        {
                        }
                    }
                }

                page.Init(p, request.URL, session);
                page.Process();

                return page.ToString();
            };
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="url">Die URL</param>
        public WorkerPage(Path url, IPage content)
            : base(url)
        {
            Content = (request) =>
            {
                return content.ToString();
            };
        }
    }
}
