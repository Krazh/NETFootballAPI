using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NETFootballAPI
{
    public class TeamHandler : ApiHandler, ITeamHandler
    {
        private const string Endpoint = "teams";
        public async Task<Team> GetTeamByIdAsync(int teamId)
        {
            CheckIfIntegerIsLessThanOrEqualToZero(teamId);

            var jsonString = await GetStringFromEndpoint(ApiUrl + Endpoint + $"?id={teamId}", Endpoint);
            
            if(!CheckIfEmptyString(jsonString)) return GetTeamFromJson(jsonString);
 
            throw new ArgumentException();
        }

        private bool CheckIfEmptyString(string? jsonString)
        {
            if (jsonString != null)
            {
                jsonString = jsonString.TrimStart('[');
                jsonString = jsonString.TrimEnd(']');

            }
            return string.IsNullOrEmpty(jsonString);
        }

        public async Task<List<Team>> GetTeamsByLeagueIdAndSeasonAsync(int leagueId, int season)
        {
            CheckIfIntegerIsLessThanOrEqualToZero(leagueId);
            CheckIfYearIsInValidRange(season);

            var json = await GetStringFromEndpoint(ApiUrl + Endpoint + $"?league={leagueId}&season={season}", Endpoint);

            if (!string.IsNullOrWhiteSpace(json))
            {
                var teams = new List<Team>();
                // Skal hente en liste ud af Teams med Venue property fra et json array hvor hvert item er delt op i to json objects, team og venue. 
            }

            throw new ArgumentException();
        }
        
        /// <param name="search">Should not contain accented or special characters. IE: ê should be replaced with e</param>
        public async Task<Team> GetTeamByStringSearchAsync(string search)
        {
            if(string.IsNullOrWhiteSpace(search) || search.Length <3) throw new ArgumentException();
            CheckIfStringContainsSymbols(search);

            var json = await GetStringFromEndpoint(ApiUrl + Endpoint + $"?name={search}", Endpoint );

            return GetTeamFromJson(json);
        }

        private Team GetTeamFromJson(string json)
        {
            json = json.TrimStart('[');
            json = json.TrimEnd(']');
            var teamString = JsonDocument.Parse(json).RootElement.GetProperty("team").GetRawText();
            var team = JsonConvert.DeserializeObject<Team>(teamString);
            var venueString = JsonDocument.Parse(json).RootElement.GetProperty("venue").GetRawText();
            team.Venue = JsonConvert.DeserializeObject<Venue>(venueString);
            return team;
        }
    }
}