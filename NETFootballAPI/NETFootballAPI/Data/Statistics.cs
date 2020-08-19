using Newtonsoft.Json;

namespace NETFootballAPI
{
    public class Statistics
    {
        [JsonProperty("matchs")]
        public Matches Matches { get; set; }
        public TotalGoals Goals { get; set; }
        [JsonProperty("goalsAvg")]
        public AverageGoals AverageGoals { get; set; }
    }
}