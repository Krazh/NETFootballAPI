using Newtonsoft.Json;

namespace NETFootballAPI
{
    public class TotalGoals
    {
        [JsonProperty("goalsFor")]
        public Goal GoalsFor { get; set; }
        [JsonProperty("goalsAgainst")]
        public Goal GoalsAgainst { get; set; }
    }
}