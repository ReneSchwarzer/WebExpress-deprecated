﻿using Microsoft.AspNetCore.Http.Features;
using System.Linq;
using System.Net;
using System.Text;
using WebExpress.WebUri;

namespace WebExpress.Message
{
    public class HttpContext
    {
        /// <summary>
        /// The context of the web server.
        /// </summary>
        public IHttpServerContext ServerContext { get; protected set; }

        /// <summary>
        /// Returns the id.
        /// </summary>
        public string ID { get; protected set; }

        /// <summary>
        /// Returns the request.
        /// </summary>
        public Request Request { get; protected set; }

        /// <summary>
        /// Gets the ip address and port number of the server to which the request is made.
        /// </summary>
        public EndPoint LocalEndPoint { get; protected set; }

        /// <summary>
        /// Gets the ip address and port number of the client from which the request originated.
        /// </summary>
        public EndPoint RemoteEndPoint { get; protected set; }

        /// <summary>
        /// Set of features.
        /// </summary>
        public IFeatureCollection Features { get; protected set; }

        /// <summary>
        /// The encoding.
        /// </summary>
        public Encoding Encoding { get; protected set; } = Encoding.Default;

        /// <summary>
        /// Returns the uri.
        /// </summary>
        public IUri Uri { get; internal set; }

        /// <summary>
        /// Returns the uri.
        /// </summary>
        //public System.Uri RealUri { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        internal HttpContext()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contextFeatures">Initial set of features.</param>
        /// <param name="serverContext">The context of the Web server.</param>
        public HttpContext(IFeatureCollection contextFeatures, IHttpServerContext serverContext)
        {
            var connectionFeature = contextFeatures.Get<IHttpConnectionFeature>();
            var requestFeature = contextFeatures.Get<IHttpRequestFeature>();

            Features = contextFeatures;
            ID = connectionFeature.ConnectionId;
            Uri = new UriRelative(requestFeature.Path);
            //RealUri = new System.Uri(requestFeature.Path + (string.IsNullOrWhiteSpace(requestFeature.QueryString) ? "" : $"?{requestFeature.QueryString}"), UriKind.Relative);
            LocalEndPoint = new IPEndPoint(connectionFeature.LocalIpAddress, connectionFeature.LocalPort);
            RemoteEndPoint = new IPEndPoint(connectionFeature.RemoteIpAddress, connectionFeature.RemotePort);

            Encoding = requestFeature.Headers.ContentEncoding.Any() ? Encoding.GetEncoding(requestFeature.Headers.ContentEncoding) : Encoding.Default;

            Request = new Request(contextFeatures, serverContext);
        }
    }
}
