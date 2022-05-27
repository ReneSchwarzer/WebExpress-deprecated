using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebExpress.Html;
using WebExpress.Uri;
using WebExpress.WebSession;

namespace WebExpress.Message
{
    /// <summary>
    /// siehe RFC 2616, Die Request-Klasse kapselt den orginalen Request des HttpListener-Aufrufes und erweitert diesen.
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Liefert den Anfragetyp
        /// </summary>
        public RequestMethod Method { get; private set; }

        /// <summary>
        /// Liefert die URL
        /// </summary>
        public IUri Uri { get; internal set; }

        /// <summary>
        /// Liefert die URL
        /// </summary>
        public IUri BaseUri { get; internal set; }

        /// <summary>
        /// Setzt oder liefert die Parameter
        /// </summary>
        private ParameterDictionary Param { get; } = new ParameterDictionary();

        /// <summary>
        /// Liefert die Session
        /// </summary>
        public Session Session { get; private set; }

        /// <summary>
        /// Setzt oder liefert die HTTP-Version
        /// </summary>
        public string Protocoll { get; private set; }

        /// <summary>
        /// Setzt oder liefert die Optionen im Header
        /// </summary>
        public RequestHeaderFields Header { get; private set; }

        /// <summary>
        /// Ruft die IP-Adresse und Anschlussnummer des Servers ab, an den die Anforderung gerichtet ist.
        /// </summary>
        public EndPoint LocalEndPoint { get; private set; }

        /// <summary>
        /// Ruft die IP-Adresse und Anschlussnummer des Clients ab, von dem die Anforderung stammt.
        /// </summary>
        public EndPoint RemoteEndPoint { get; private set; }

        /// <summary>
        /// Ruft einen Boolean-Wert ab, der angibt, ob der diese Anforderung sendende Client authentifiziert ist.
        /// </summary>
        //public bool IsAuthenticated { get; private set; }  //=> RawRequuest.IsAuthenticated;

        /// <summary>
        /// Ruft einen Boolean-Wert ab, der angibt, ob die Anforderung vom lokalen Computer gesendet wurde.
        /// </summary>
        //public bool IsLocal { get; private set; }  //=> RawRequuest.IsLocal;

        /// <summary>
        /// Ruft einen Boolean-Wert ab, der angibt, ob die TCP-Verbindung, mit der die Anforderung gesendet wird, das SSL (Secure Sockets Layer)-Protokoll verwendet.
        /// </summary>
        public bool IsSecureConnection { get; private set; }

        /// <summary>
        /// Ruft einen Boolean-Wert ab, der angibt, ob die TCP Verbindung eine WEbSocket Anforderung war.
        /// </summary>
        //public bool IsWebSocketRequest { get; private set; }  // => RawRequuest.IsWebSocketRequest;

        /// <summary>
        /// Ruft einen Boolean-Wert ab, der angibt, ob der Client eine permanente Verbindung anfordert.
        /// </summary>
        //public bool KeepAlive { get; private set; }  //=> RawRequuest.KeepAlive;

        /// <summary>
        /// Ruft einen das Schma ab. Dies kann http oder https sein.
        /// </summary>
        public UriScheme Scheme { get; private set; }

        /// <summary>
        /// Ruft den Anforderungsbezeichner der eingehenden HTTP-Anforderung ab.
        /// </summary>
        public string RequestTraceIdentifier { get; private set; }

        /// <summary>
        /// Ermittelt die Kultur
        /// </summary>
        public CultureInfo Culture
        {
            get
            {
                try
                {
                    return new CultureInfo(Header?.AcceptLanguage.FirstOrDefault());
                }
                catch
                {
                    return CultureInfo.CurrentCulture;
                }
            }
        }

        /// <summary>
        /// Liefert den Inhalt
        /// </summary>
        public byte[] Content { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="contextFeatures">Anfänglicher Satz von Features.</param>
        internal Request(IFeatureCollection contextFeatures)
        {
            var connectionFeature = contextFeatures.Get<IHttpConnectionFeature>();
            var requestFeature = contextFeatures.Get<IHttpRequestFeature>();
            var requestIdentifierFeature = contextFeatures.Get<IHttpRequestIdentifierFeature>();
            var sessionFeature = contextFeatures.Get<ISessionFeature>();

            RequestTraceIdentifier = requestIdentifierFeature.TraceIdentifier;
            Protocoll = requestFeature.Protocol;

            Scheme = requestFeature.Scheme.ToLower() switch
            {
                "http" => UriScheme.Http,
                "https" => UriScheme.Https,
                "ftp" => UriScheme.FTP,
                "file" => UriScheme.File,
                "mailto" => UriScheme.Mailto,
                "ldap" => UriScheme.Ldap,
                _ => UriScheme.Http

            };
            Method = requestFeature.Method.ToUpper() switch
            {
                "GET" => RequestMethod.GET,
                "POST" => RequestMethod.POST,
                "PUT" => RequestMethod.PUT,
                "DELETE" => RequestMethod.DELETE,
                "HEAD" => RequestMethod.HEAD,
                "PATCH" => RequestMethod.PATCH,
                _ => RequestMethod.GET
            };

            Uri = new UriRelative(requestFeature.Path);
            Header = new RequestHeaderFields(contextFeatures);

            LocalEndPoint = new IPEndPoint(connectionFeature.LocalIpAddress, connectionFeature.LocalPort);
            RemoteEndPoint = new IPEndPoint(connectionFeature.RemoteIpAddress, connectionFeature.RemotePort);

            BaseUri = new UriAbsolute(Scheme, new UriAuthority(Header.Host, connectionFeature.LocalPort), new UriRelative());

            Content = GetContent(requestFeature.Body, Header.ContentLength);

            ParseQueryParams(requestFeature.QueryString);
            ParseRequestParams();
            ParseSessionParams();
        }

        /// <summary>
        /// Ermittelt den Content
        /// </summary>
        /// <param name="body">Der Inhalt einer Anforderung</param>
        /// <param name="contentLength">Die Anzahl der Bytes, die im Body gesendet wurden oder Null.</param>
        /// <returns>Der Content als Byte-Array</returns>
        internal static byte[] GetContent(Stream body, long? contentLength)
        {
            if (!contentLength.HasValue || contentLength.Value == 0)
            {
                return null;
            }

            MemoryStream ms = new MemoryStream();
            body.CopyTo(ms);

            return ms.ToArray();
        }

        /// <summary>
        /// Ermittelt die Paramerter aus der Anfragequery(z.B. http://www.example.com?key=value) 
        /// </summary>
        /// <param name="query">Die Query</param>
        private void ParseQueryParams(string query)
        {
            query = query.TrimStart('?');

            Parallel.ForEach(query.Split('&'), (param) =>
            {
                if (!string.IsNullOrWhiteSpace(param))
                {
                    var split = param.Split('=');

                    if (split.Length == 1)
                    {
                        AddParameter(new Parameter(split[0], null, ParameterScope.Parameter));
                    }
                    else if (split.Length == 2)
                    {
                        AddParameter(new Parameter(split[0], split[1], ParameterScope.Parameter));
                    }
                }
            });
        }

        /// <summary>
        /// Ermittelt den Content einer Anfrage
        /// </summary>
        private void ParseRequestParams()
        {
            if (string.IsNullOrWhiteSpace(Header.ContentType))
            {
                return;
            }

            var contentType = Header.ContentType?.Split(';');
            //var contentStr = Encoding.UTF8.GetString(Content);

            switch (TypeEnctypeExtensions.Convert(contentType.FirstOrDefault()))
            {
                case TypeEnctype.None:
                    {
                        var boundary = Header.ContentType;
                        var boundaryValue = "--" + boundary?.Split('=').Skip(1)?.FirstOrDefault();
                        var offset = 0;
                        int pos = 0;
                        var dispositions = new List<Tuple<int, int>>(); // Item1=Position, Item2=Länge

                        // ermittle Dispositionen
                        for (var i = 0; i < Content.Length; i++)
                        {
                            if (Content[i] == '\r')
                            {
                                var c = Encoding.UTF8.GetString(Content, offset, boundaryValue.Length).Trim();
                                if (c.StartsWith(boundaryValue))
                                {
                                    if (i - boundaryValue.Length - pos > 0)
                                    {
                                        dispositions.Add(new Tuple<int, int>(pos, i - boundaryValue.Length - pos));
                                    }

                                    pos = i + 2;

                                    if (c.EndsWith("--"))
                                    {
                                        break;
                                    }
                                }
                            }
                            else if (Content[i] == '\n')
                            {
                                offset = i + 1;
                            }
                            else if (i == Content.Length - 1)
                            {
                                // Am Ende
                                var c = Encoding.UTF8.GetString(Content, offset, boundaryValue.Length).Trim();
                                if (c.StartsWith(boundaryValue))
                                {
                                    dispositions.Add(new Tuple<int, int>(pos, i - boundaryValue.Length - pos));
                                }
                            }
                        }

                        foreach (var item in dispositions)
                        {
                            var disposition = string.Empty;
                            var name = string.Empty;
                            var filename = string.Empty;
                            var contenttype = string.Empty;
                            offset = 0;

                            var str = Encoding.UTF8.GetString(Content, item.Item1, item.Item2 > 256 ? 256 : item.Item2);
                            var match = Regex.Match(str, @"^Content-Disposition: (.*)$", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                            if (match.Groups[1].Success)
                            {
                                offset += match.Length + 1; // + Zeilenende
                                var dispositionParam = match.Groups[1].ToString().Split(';');
                                disposition = dispositionParam.FirstOrDefault();
                                foreach (var v in dispositionParam.Skip(1))
                                {
                                    match = Regex.Match(v.Trim(), @"^name=""(.*)""$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                                    if (match.Groups[1].Success)
                                    {
                                        name = match.Groups[1].ToString();
                                    }

                                    match = Regex.Match(v.Trim(), @"^filename=""(.*)""$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                                    if (match.Groups[1].Success)
                                    {
                                        filename = match.Groups[1].ToString();
                                    }
                                }
                            }

                            match = Regex.Match(str, @"^Content-Type: (.*)$", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                            if (match.Groups[1].Success)
                            {
                                offset += match.Length + 1; // + Zeilenende
                                contenttype = match.Groups[1].ToString().Trim();
                            }

                            if (string.IsNullOrWhiteSpace(filename))
                            {
                                offset += 2; // + Leerzeile
                                if (item.Item2 - offset - 1 >= 0)
                                {
                                    var value = Encoding.UTF8.GetString(Content, item.Item1 + offset, item.Item2 - offset - 2);

                                    var param = new Parameter(name, value.TrimEnd(), ParameterScope.Parameter);
                                    AddParameter(param);
                                }
                                else
                                {
                                    var param = new Parameter(name, string.Empty, ParameterScope.Parameter);
                                    AddParameter(param);
                                }
                            }
                            else
                            {
                                offset += 2; // + Leerzeile
                                if (item.Item2 - offset - 1 >= 0)
                                {
                                    var bytes = new byte[item.Item2 - offset - 2];
                                    Buffer.BlockCopy(Content, item.Item1 + offset, bytes, 0, item.Item2 - offset - 2);

                                    var param = new ParameterFile(name, filename, ParameterScope.Parameter) { ContentType = contenttype, Data = bytes };
                                    AddParameter(param);
                                }
                                else
                                {
                                    var param = new Parameter(name, filename, ParameterScope.Parameter);
                                    AddParameter(param);
                                }
                            }
                        }

                        break;
                    }
                case TypeEnctype.Text:
                    {
                        var lines = new List<string>();
                        var offset = 0;

                        for (var i = 0; i < Content.Length; i++)
                        {
                            if (Content[i] == '\r')
                            {
                                lines.Add(Encoding.UTF8.GetString(Content, offset, i - offset));
                            }
                            else if (Content[i] == '\n')
                            {
                                offset = i + 1;
                            }
                        }

                        // wenn noch nicht alle Bytes gelesen wurden
                        if (offset < Content.Length)
                        {
                            lines.Add(Encoding.UTF8.GetString(Content, offset, Content.Length - offset));
                        }

                        var last = null as Parameter;

                        foreach (var v in lines)
                        {
                            var match = Regex.Match(v, @"([\w-]*)=(.*)", RegexOptions.Compiled);
                            if (match.Groups[1].Success && match.Groups[2].Success)
                            {
                                last = new Parameter(match.Groups[1].ToString().Trim(), match.Groups[2].ToString().Trim(), ParameterScope.Parameter);
                                AddParameter(last);
                            }
                            else if (last != null)
                            {
                                last.Value += "\r\n" + v;

                            }
                        }

                        if (last != null)
                        {
                            last.Value = last.Value.TrimEnd();
                        }

                        break;
                    }
                case TypeEnctype.UrLEncoded:
                    {
                        var str = Encoding.UTF8.GetString(Content, 0, Content.Length);
                        var param = str.Replace('+', ' ');

                        foreach (var v in param.Split('&'))
                        {
                            var s = v.Split('=');
                            AddParameter(new Parameter
                            (
                                s[0],
                                s.Length > 1 ? s[1]?.TrimEnd() : string.Empty,
                                ParameterScope.Parameter
                            ));
                        }

                        break;
                    }
                default:
                    {

                        break;
                    }
            }
        }

        /// <summary>
        /// Ermittelt den die Parameter aus der Session
        /// </summary>
        private void ParseSessionParams()
        {
            Session = SessionManager.GetSession(this);

            var property = Session.GetProperty<SessionPropertyParameter>();
            if (property != null && property.Params != null)
            {
                foreach (var param in property.Params)
                {
                    AddParameter(new Parameter(param.Key?.ToLower(), param.Value.Value, ParameterScope.Session));
                }
            }
        }

        /// <summary>
        /// Fügt mehrere Parameter hinzu
        /// </summary>
        /// <param name="param">Die Parameter</param>
        public void AddParameter(IEnumerable<Parameter> param)
        {
            foreach (var p in param)
            {
                AddParameter(p);
            }
        }

        /// <summary>
        /// Fügt ein Parameter hinzu
        /// </summary>
        /// <param name="param">Der Parameter</param>
        public void AddParameter(Parameter param)
        {
            if (!Param.ContainsKey(param.Key.ToLower()))
            {
                Param.Add(param.Key.ToLower(), param);
            }
            else
            {
                Param[param.Key.ToLower()] = param;
            }
        }

        /// <summary>
        /// Liefert ein Parameter anhand seines Namens
        /// </summary>
        /// <param name="name">Der Name des Parameters</param>
        /// <returns>Der Wert</returns>
        public Parameter GetParameter(string name)
        {
            if (!string.IsNullOrWhiteSpace(name) && HasParameter(name))
            {
                return Param[name.ToLower()];
            }

            return null;
        }

        /// <summary>
        /// Prüft, ob ein Parameter vorhanden ist
        /// </summary>
        /// <param name="name">Der Name des Parameters</param>
        /// <returns>true wenn Parameter vorhanden ist, false sonst</returns>
        public bool HasParameter(string name)
        {
            if (name == null)
            {
                return false;
            }

            return Param.ContainsKey(name.ToLower());
        }
    }
}
