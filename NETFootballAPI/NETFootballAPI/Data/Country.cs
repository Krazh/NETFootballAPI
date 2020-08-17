using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace NETFootballAPI
{
    public class Country
    {
        [JsonProperty("Country")]
        public string Name { get; set; }
        
        public string Code { get; set; }
        public string Flag { get; set; }
    }
}