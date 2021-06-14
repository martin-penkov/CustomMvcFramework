using System;
using System.Collections.Generic;
using System.Linq;

namespace WebServer.Server.Http
{
    public class HttpRequest
    {
        private const string NewLine = "/r/n";

        public HttpMethod Method { get; init; }

        public string Path { get; init; }

        public HttpHeaderCollection HeaderCollection { get; private set; } = new HttpHeaderCollection();

        public string Body { get; init; }

        public static HttpRequest Parse(string request)
        {
            var lines = request.Split(NewLine);

            var startLine = lines.First().Split(" ");

            var method = ParseHttpMethod(startLine[0]);
            var path = startLine[1];

            var headers = ParseHttpHeaderCollection(lines.Skip(1));
            var requestObject = new HttpRequest()
            {

            };
        }

        private static HttpHeaderCollection ParseHttpHeaderCollection(IEnumerable<string> headerLines)
        {
            var headers = new HttpHeaderCollection();
            foreach (var headerLine in headerLines)
            {
                if (headerLine != NewLine)
                {
                    var headerParts = headerLine.Split(":");

                    var header = new HttpHeader()
                    {
                        Name = headerParts[0],
                        Value = headerParts[1].Trim()
                    };
                    headers.Add(header);
                }
            }

            return headers;
        }

        private static HttpMethod ParseHttpMethod(string method)
        {
            return method.ToUpper() switch
            {
                "GET" => HttpMethod.Get,
                "POST" => HttpMethod.Post,
                "PUT" => HttpMethod.Put,
                "DELETE" => HttpMethod.Delete,
                _ => throw new System.InvalidOperationException($"Method '{method}' is not supported")
            };
        }

        //private static string[] getStartLine(string request)
        //{

        //}
    }
}
