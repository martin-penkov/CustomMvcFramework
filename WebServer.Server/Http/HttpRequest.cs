using System;

namespace WebServer.Server.Http
{
    public class HttpRequest
    {
        public HttpMethod Method { get; init; }

        public string Path { get; init; }

        public HttpHeaderCollection HeaderCollection { get; } = new HttpHeaderCollection();

        public string Body { get; init; }
    }
}
