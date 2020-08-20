#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NETFootballAPI
{
    public class ApiHandler : IBaseApi
    {
        private string _apiKey = "";
        internal string ApiUrl = "";
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
            
            ApiUrl = url;
        }

        public async Task<List<T>> GetListFromEndpoint<T>(string url, string endpoint)
        {
            if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(endpoint)) throw new ArgumentNullException();
            var unusedVar = new Uri(url); // Only used to test if string is a valid url

            try
            {
                var content = await _client.GetStringAsync(url.ToLower());
                var jsonElement = JsonDocument.Parse(content).RootElement.GetProperty("api").GetProperty(endpoint).GetRawText();
                return JsonConvert.DeserializeObject<List<T>>(jsonElement);
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
                var jsonElement = JsonDocument.Parse(content).RootElement.GetProperty("api").GetProperty(endpoint)
                    .GetRawText();
                jsonElement = jsonElement.TrimStart('[');
                jsonElement = jsonElement.TrimEnd(']');
                return JsonConvert.DeserializeObject<T>(jsonElement);
            }
            catch (Exception e)
            {
                // TODO Implement error logging
                return default(T)!;
            }
        }
        
        #region Internal Methods

        internal static void CheckIfIdIsLessThanOrEqualToZero(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than or equal to 0");
        }

        internal static void CheckIfStringContainsSymbols(string item)
        {
            if (Regex.IsMatch(item, "[!,@,#,$,%,^,&,*,?,~,£,(,)]")) throw new ArgumentException("String contains invalid symbols");
        }

        internal static void CheckIfYearIsInValidRange(int year)
        {
            if (year <= 1900 || year >= (DateTime.Today.Year + 5))
                throw new ArgumentOutOfRangeException();
        }
        #endregion
    }
} 