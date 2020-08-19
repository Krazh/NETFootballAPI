using Newtonsoft.Json;

namespace NETFootballAPI
{
    public class AverageGoals
    {
        [JsonProperty("goalsFor")]
        public Goal GoalsFor { get; set; }
        [JsonProperty("goalsAgainst")]
        public Goal GoalsAgainst { get; set; }
    }
}