using System;
using Newtonsoft.Json;

namespace NETFootballAPI
{
    public class Fixture
    {
        [JsonProperty("fixture_id")]
        public int Id { get; set; }
        [JsonProperty("league_id")]
        public int LeagueId { get; set; }
        public League League { get; set; }
        public DateTime EventDate { get; set; }
        public string EventTimeStamp { get; set; }
        public string FirstHalfStart { get; set; }
        public string SecondHalfStart { get; set; }
        public string Round { get; set; }
        public string Status { get; set; }
        public string StatusShort { get; set; }
        public int Elapsed { get; set; }
        public string Venue { get; set; }
        public string Referee { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public int? GoalsHomeTeam { get; set; }
        public int? GoalsAwayTeam { get; set; }
        public Score Score { get; set; }
    }
}