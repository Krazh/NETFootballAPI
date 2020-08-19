#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            _apiKey = key;
            _client.DefaultRequestHeaders.Add("X-RapidAPI-Key", _apiKey);
        }

        public void SetApiUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));
 
            var unusedVar = new Uri(url); // Only used to test if string is a valid url
            
            _apiUrl = url;
        }

        public async Task<List<T>> GetListFromEndpoint<T>(string url, string endpoint)
        {
            if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(endpoint)) throw new ArgumentNullException();
            var unusedVar = new Uri(url); // Only used to test if string is a valid url

            try
            {
                var content = await _client.GetStringAsync(url.ToLower());
                var array = DeserializeJson(content, endpoint);

                return GetListFromJArray<T>(array);
            }
            catch (Exception e)
            {
                // TODO Implement error logging
                return null!;
            }
        }

        public async Task<T> GetItemFromEndpoint<T>(string url, string endpoint)
        {
            if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(endpoint)) throw new ArgumentNullException();
            var unusedVar = new Uri(url); // Only used to test if string is a valid url
            
            try
            {
                var content = await _client.GetStringAsync(url);
                var jObj = DeserializeJson(content, endpoint);

                return GetFirstObjectFromJArray<T>(jObj);
            }
            catch (Exception e)
            {
                // TODO Implement error logging
                return default(T)!;
            }
        }

        private JArray DeserializeJson(string content, string endpoint)
        {
            var jDoc = JsonDocument.Parse(content);
            var jObj = JsonConvert.DeserializeObject(jDoc.RootElement.GetProperty("api").GetProperty(endpoint)
                .ToString());

            return (jObj as JArray)!;
        }

        private T GetFirstObjectFromJArray<T>(JArray array)
        {
            if (array.First == null) throw new NullReferenceException();
            return JsonConvert.DeserializeObject<T>(array.First.ToString()!);
        }

        private List<T> GetListFromJArray<T>(JArray array)
        {
            if (array.First == null) throw new NullReferenceException();
            return (from object ob in array select JsonConvert.DeserializeObject<T>(ob.ToString())).ToList();
        }
    }
} 