using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EscolaBiblica.App.Biblioteca.Web
{
    public class WebRequestHelper
    {
        private readonly WebRequestConfig _config;

        public WebRequestHelper(WebRequestConfig config)
        {
            _config = config;
        }

        public async Task Post(object jsonPost)
        {
            await Post(jsonPost, (json, header) => true);
        }

        public async Task<T> Post<T>(object jsonPost, Func<string, WebHeaderCollection, T> fn = null)
        {
            var request = CriarRequisicao();
            request.Method = "POST";

            using (var webStream = await Task.Factory.FromAsync(request.BeginGetRequestStream, request.EndGetRequestStream, request))
            {
                WriteJson(webStream, jsonPost);

                return await Response(request, (webResponse, stream) =>
                {
                    var responseJson = stream.ReadToEnd();

                    if (fn == null)
                        return JsonConvert.DeserializeObject<T>(responseJson);

                    return fn(responseJson, webResponse.Headers);
                });
            }
        }

        public async Task Put(object jsonPut)
        {
            var request = CriarRequisicao();
            request.Method = "PUT";

            using (var webStream = await Task.Factory.FromAsync(request.BeginGetRequestStream, request.EndGetRequestStream, request))
            {
                WriteJson(webStream, jsonPut);

                await Response<object>(request);
            }
        }

        public async Task<T> Get<T>(Func<string, T> fn = null)
        {
            var request = CriarRequisicao();
            request.Method = "GET";

            return await Response(request, (webResponse, stream) =>
            {
                var json = stream.ReadToEnd();

                if (fn == null)
                    return JsonConvert.DeserializeObject<T>(json);

                return fn(json);
            });
        }

        public async Task Delete(object jsonDelete)
        {
            await Delete(jsonDelete, json => true);
        }

        public async Task<T> Delete<T>(object jsonDelete, Func<string, T> fn)
        {
            var request = CriarRequisicao();
            request.Method = "DELETE";

            using (var webStream = await Task.Factory.FromAsync(request.BeginGetRequestStream, request.EndGetRequestStream, request))
            {
                WriteJson(webStream, jsonDelete);

                return await Response(request, (webResponse, stream) => fn(stream.ReadToEnd()));
            }
        }

        private async Task<T> Response<T>(WebRequest request, Func<WebResponse, StreamReader, T> fn = null)
        {
            using (var webResponse = await Task.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, request))
            using (var stream = new StreamReader(webResponse.GetResponseStream()))
            {
                if (fn == null)
                    return default(T);

                return fn(webResponse, stream);
            }
        }

        private void WriteJson(Stream stream, object jsonObject)
        {
            var json = JsonConvert.SerializeObject(jsonObject, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var bytes = Encoding.UTF8.GetBytes(json);
            stream.Write(bytes, 0, bytes.Length);
        }

        private WebRequest CriarRequisicao()
        {
            var request = WebRequest.Create(new Uri(_config.ToString()));
            request.ContentType = "application/json";
            request.Timeout = 30000; // 30 s

            foreach (var header in _config.Headers)
                request.Headers[header.Key] = header.Value.ToString();

            return request;
        }
    }
}
