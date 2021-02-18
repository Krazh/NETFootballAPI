#nullable enable
using Newtonsoft.Json;

namespace NETFootballAPI
{
    public class Team
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public bool National { get; set; }
        public string Country { get; set; }
        public int Founded { get; set; }
        public Venue Venue { get; set; }

    }
}