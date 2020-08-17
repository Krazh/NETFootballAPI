#nullable enable
using System.Net.Http;
using System.Text.Json;
using Newtonsoft.Json;

namespace NETFootballAPI
{
    public partial class ApiHandler
    {
        private string _apiKey = "";
        private string _apiUrl = "";
        private readonly HttpClient _client;

        public ApiHandler()
        {
            _client = new HttpClient();
        }

        public void SetApiKey(string key)
        {
            _apiKey = key;
            _client.DefaultRequestHeaders.Add("X-RapidAPI-Key", _apiKey);
        }

        public void SetApiUrl(string url)
        {
            _apiUrl = url;
        }

        private object? DeserializeJson(string content, string endpoint)
        {
            var jDoc = JsonDocument.Parse(content);
            var jObj = JsonConvert.DeserializeObject(jDoc.RootElement.GetProperty("api").GetProperty(endpoint)
                .ToString());

            return jObj;
        }
    }
} 