using System;
using Newtonsoft.Json;

namespace NETFootballAPI
{
    public class Standings
    {
        public int Rank { get; set; }
        [JsonProperty("team_id")]
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string Logo { get; set; }
        public string Group { get; set; }
        [JsonProperty("forme")]
        public string Form { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public LeagueStats All { get; set; }
        public LeagueStats Home { get; set; }
        public LeagueStats Away { get; set; }
        [JsonProperty("goalsDiff")]
        public int GoalDifference { get; set; }
        public int Points { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}