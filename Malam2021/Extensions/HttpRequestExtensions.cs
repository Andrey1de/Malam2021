using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Malam2021.Extensions
{
    public static class HttpClientHelpers
    {
        public static HttpClient  PrepareClient(this HttpClient client)
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json,text/json,*/*");
            client.DefaultRequestHeaders.Add("User-Agent", "aed-task-spooler");
            client.DefaultRequestHeaders.Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/html"));
            client.DefaultRequestHeaders.Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/text"));
            return client;

        }

        public static async Task<string> GetRemoteAsync(this HttpClient client, string url)
        {
            string jsonBody = "";
            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    jsonBody = await response.Content.ReadAsStringAsync();

                }

            }
            return jsonBody;


        }

        public static async Task<string> PostRemoteAsync(this HttpClient client, string url, string jsonIn)
        {
            string jsonBody = "";

            using (StringContent content = new StringContent(jsonIn, Encoding.UTF8, "application/json"))
            {

                using (HttpResponseMessage response = await client.PostAsync(url, content))
                {
                    if (response.IsSuccessStatusCode)
                    {

                        response.EnsureSuccessStatusCode();
                        jsonBody = await response.Content.ReadAsStringAsync();
                    }


                }
            }
            return jsonBody;
        }
 
        public static async Task<string> PutRemoteAsync(this HttpClient client, string url, string jsonIn)
        {
            string jsonBody = "";

            using (StringContent content = new StringContent(jsonIn, Encoding.UTF8, "application/json"))
            {

                using (HttpResponseMessage response = await client.PutAsync(url, content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        response.EnsureSuccessStatusCode();
                        jsonBody = await response.Content.ReadAsStringAsync();
                    }


                }
            }
            return jsonBody;
        }

        public static async Task<string> DeleteRemoteAsync(this HttpClient client, string url)
        {
            string jsonBody = "";

            using (HttpResponseMessage response = await client.DeleteAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    jsonBody = await response.Content.ReadAsStringAsync();

                }
                return jsonBody;
            }
        }

        public class BodyStatus
        {
            public HttpStatusCode StatusCode { get; init; }
            public string Body { get; init; }

        }

        public static async Task<BodyStatus> GetRemoteAsync2(this HttpClient client, string url)
        {
            string jsonBody = "";
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;
            using (HttpResponseMessage response = await client.GetAsync(url))
            {

                response.EnsureSuccessStatusCode();
                jsonBody = await response.Content.ReadAsStringAsync();
                statusCode = (jsonBody != "[]") ? response.StatusCode : HttpStatusCode.NotFound;

            }
            return new BodyStatus() { Body = jsonBody, StatusCode = statusCode };


        }

        public static async Task<BodyStatus> PostRemoteAsync2<T>(this HttpClient client, string url, T objIn) where T : class, new()
        {
            string jsonBody = "";
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;
            string jsonIn = objIn.ToJson();

            using (StringContent content = new StringContent(jsonIn, Encoding.UTF8, "application/json"))
            {

                using (HttpResponseMessage response = await client.PostAsync(url, content))
                {
                    response.EnsureSuccessStatusCode();
                    jsonBody = await response.Content.ReadAsStringAsync();
                    statusCode = response.StatusCode;

                }
            }
            return new BodyStatus() { Body = jsonBody, StatusCode = statusCode };
        }
        public static async Task<BodyStatus> PutRemoteAsync2<T>(this HttpClient client, string url, T objIn)  where T: class,new()
        {
            string jsonBody = "";
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;
            string jsonIn = objIn.ToJson();

            using (StringContent content = new StringContent(jsonIn, Encoding.UTF8, "application/json"))
            {

                using (HttpResponseMessage response = await client.PutAsync(url, content))
                {
                    response.EnsureSuccessStatusCode();
                    jsonBody = await response.Content.ReadAsStringAsync();
                    statusCode = response.StatusCode;

                }
            }
            return new BodyStatus() { Body = jsonBody, StatusCode = statusCode };
        }

        public static async Task<BodyStatus> DeleteRemoteAsync2(this HttpClient client, string url)
        {
            string jsonBody = "";
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;

            using (HttpResponseMessage response = await client.DeleteAsync(url))
            {
                response.EnsureSuccessStatusCode();
                jsonBody = await response.Content.ReadAsStringAsync();
                statusCode = response.StatusCode;

            }

            return new BodyStatus() { Body = jsonBody, StatusCode = statusCode };
        }

    }
    public static class RequestTranscriptHelpers
    {
        public static HttpRequestMessage ToHttpRequestMessage(this HttpRequest req)
            => new HttpRequestMessage()
                .SetMethod(req)
                .SetAbsoluteUri(req)
                .SetHeaders(req)
                .SetContent(req)
                .SetContentType(req);

        private static HttpRequestMessage SetAbsoluteUri(this HttpRequestMessage msg, HttpRequest req)
            => msg.Set(m => m.RequestUri = new UriBuilder
            {
                Scheme = req.Scheme,
                Host = req.Host.Host,
                Port = req.Host.Port.Value,
                Path = req.PathBase.Add(req.Path),
                Query = req.QueryString.ToString()
            }.Uri);

        private static HttpRequestMessage SetMethod(this HttpRequestMessage msg, HttpRequest req)
            => msg.Set(m => m.Method = new HttpMethod(req.Method));

        private static HttpRequestMessage SetHeaders(this HttpRequestMessage msg, HttpRequest req)
            => req.Headers.Aggregate(msg, (acc, h) => acc.Set(m => m.Headers.TryAddWithoutValidation(h.Key, h.Value.AsEnumerable())));

        private static HttpRequestMessage SetContent(this HttpRequestMessage msg, HttpRequest req)
            => msg.Set(m => m.Content = new StreamContent(req.Body));

        private static HttpRequestMessage SetContentType(this HttpRequestMessage msg, HttpRequest req)
            => msg.Set(m => m.Content.Headers.Add("Content-Type", req.ContentType), applyIf: req.Headers.ContainsKey("Content-Type"));

        private static HttpRequestMessage Set(this HttpRequestMessage msg, Action<HttpRequestMessage> config, bool applyIf = true)
        {
            if (applyIf)
            {
                config.Invoke(msg);
            }

            return msg;
        }
    }
    public static class ResponseTranscriptHelpers
    {
        public static async Task FromHttpResponseMessage(this HttpResponse resp, HttpResponseMessage msg)
        {
            resp.SetStatusCode(msg)
                .SetHeaders(msg)
                .SetContentType(msg);

            await resp.SetBodyAsync(msg);
        }

        private static HttpResponse SetStatusCode(this HttpResponse resp, HttpResponseMessage msg)
            => resp.Set(r => r.StatusCode = (int)msg.StatusCode);

        private static HttpResponse SetHeaders(this HttpResponse resp, HttpResponseMessage msg)
            => msg.Headers.Aggregate(resp, (acc, h) => acc.Set(r => r.Headers[h.Key] = new StringValues(h.Value.ToArray())));

        private static async Task<HttpResponse> SetBodyAsync(this HttpResponse resp, HttpResponseMessage msg)
        {
            using (var stream = await msg.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            {
                var content = await reader.ReadToEndAsync();

                return resp.Set(async r => await r.WriteAsync(content));
            }
        }

        private static HttpResponse SetContentType(this HttpResponse resp, HttpResponseMessage msg)
            => resp.Set(r => r.ContentType = msg.Content.Headers.GetValues("Content-Type").Single(), applyIf: msg.Content.Headers.Contains("Content-Type"));

        private static HttpResponse Set(this HttpResponse msg, Action<HttpResponse> config, bool applyIf = true)
        {
            if (applyIf)
            {
                config.Invoke(msg);
            }

            return msg;
        }
    }
}
