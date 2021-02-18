#nullable enable
using Newtonsoft.Json;

namespace NETFootballAPI
{
    public class Team
    {
        [JsonProperty("team_id")]
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Code { get; set; }
        public string Logo { get; set; }
        [JsonProperty("is_national")]
        public bool IsNational { get; set; }
        public string Country { get; set; }
        public int Founded { get; set; }
        public Venue Venue { get; set; }
        [JsonProperty("statistics")]
        public Matches MatchStatistics { get; set; }
    }
}