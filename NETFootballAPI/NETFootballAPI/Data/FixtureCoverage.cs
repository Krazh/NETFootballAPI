using Newtonsoft.Json;

namespace NETFootballAPI
{
    public class FixtureCoverage
    {
        public bool Events { get; set; }
        public bool Lineups { get; set; }
        public bool Statistics { get; set; }
        [JsonProperty("player_statistics")]
        public bool PlayerStatistics { get; set; }
    }
}