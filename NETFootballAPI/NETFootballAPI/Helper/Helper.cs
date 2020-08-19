using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NETFootballAPI
{
    public static class Helper
    {
        public static JArray DeserializeJson(string content, string endpoint)
        {
            var jDoc = JsonDocument.Parse(content);
            var jObj = JsonConvert.DeserializeObject(jDoc.RootElement.GetProperty("api").GetProperty(endpoint).ToString());

            return new JArray(jObj);
        }
    }
}