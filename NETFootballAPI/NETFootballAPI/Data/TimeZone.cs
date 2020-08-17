namespace NETFootballAPI
{
    public class TimeZone
    {
        public string Region { get; set; }
        public string Country { get; set; }

        public TimeZone(string compiledString)
        {
            var strings = compiledString.Split('/');
            Region = strings[0];
            Country = strings[1];
        }

        public override string ToString()
        {
            return $"This timezone is in the country {Country} in the region {Region}";
        }
    }
}