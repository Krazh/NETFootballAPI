using System;
using Newtonsoft.Json;

namespace NETFootballAPI
{
    #nullable enable
    public class League
    {
        [JsonProperty("league_id")]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Country { get; set; }
        [JsonProperty("country_code")]
        public string? CountryCode { get; set; }
        public int Season { get; set; }
        [JsonProperty("season_start")]
        public DateTime? SeasonStart { get; set; }
        [JsonProperty("season_end")] 
        public DateTime? SeasonEnd { get; set; }
        public string Logo { get; set; }
        public string? Flag { get; set; }
        public string Standings { get; set; }
        [JsonProperty("is_current")]
        public bool IsCurrent { get; set; }
        public Coverage Coverage { get; set; }
    }
}