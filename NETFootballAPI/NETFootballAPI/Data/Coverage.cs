namespace NETFootballAPI
{
    public class Coverage
    {
        public bool Standings { get; set; }
        public Fixture Fixtures { get; set; }
        public bool Players { get; set; }
        public bool TopScorers { get; set; }
        public bool Predictions { get; set; }
        public bool Odds { get; set; }
    }
}