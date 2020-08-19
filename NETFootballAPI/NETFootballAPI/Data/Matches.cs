using Newtonsoft.Json;

namespace NETFootballAPI
{
    public class Matches
    {
        [JsonProperty("matchsPlayed")]
        public Match MatchesPlayer { get; set; }
        public Match Wins { get; set; }
        public Match Draws { get; set; }
        public Match Loses { get; set; }
    }
}