using System;
using System.Collections.Generic;
using System.Linq;
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

        public static T GetFirstObjectFromJArray<T>(JArray array)
        {
            if (array.First == null) throw new NullReferenceException();
            var item = array.First.First.ToString()!;
            return JsonConvert.DeserializeObject<T>(item);
        }
        
        public static List<T> GetListFromJArray<T>(JArray array)
        {
            if (array.First == null) throw new NullReferenceException();
            var temp = from object ob in array.First select JsonConvert.DeserializeObject<T>(ob.ToString());
            return temp.ToList();
        }
    }
}