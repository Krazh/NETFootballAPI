using Newtonsoft.Json;

namespace NETFootballAPI
{
    public class LeagueStats
    {
        [JsonProperty("matchsPlayed")]
        public int MatchesPlayed { get; set; }
        public int Win { get; set; }
        public int Draw { get; set; }
        public int Lose { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
    }
}