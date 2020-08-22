using Newtonsoft.Json;

namespace NETFootballAPI
{
    public class Coverage
    {
        public bool Standings { get; set; }
        public FixtureCoverage Fixtures { get; set; }
        public bool Players { get; set; }
        public bool TopScorers { get; set; }
        public bool Predictions { get; set; }
        public bool Odds { get; set; }
    }
}