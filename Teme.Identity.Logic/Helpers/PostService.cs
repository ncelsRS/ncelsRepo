using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Teme.Identity.Logic.Helpers
{
    class PostService
    {
        private static readonly HttpClient _Client = new HttpClient();
        //метод для вызова POST сервиса, возвращает json
        public static async Task<string> getPostService(string requestJson, string url)
        {
            var response = await Request(HttpMethod.Post, url, requestJson, new Dictionary<string, string>());
            string responseText = await response.Content.ReadAsStringAsync();
            return responseText;
        }
        static async Task<HttpResponseMessage> Request(HttpMethod pMethod, string pUrl, string pJsonContent, Dictionary<string, string> pHeaders)
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = pMethod;
            httpRequestMessage.RequestUri = new Uri(pUrl);
            foreach (var head in pHeaders)
            {
                httpRequestMessage.Headers.Add(head.Key, head.Value);
            }
            switch (pMethod.Method)
            {
                case "POST":
                    HttpContent httpContent = new StringContent(pJsonContent, Encoding.UTF8, "application/json");
                    httpRequestMessage.Content = httpContent;
                    break;
            }
            return await _Client.SendAsync(httpRequestMessage);
        }
    }
}
