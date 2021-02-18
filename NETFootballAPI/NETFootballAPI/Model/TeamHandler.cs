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
            
            if(!string.IsNullOrWhiteSpace(jsonString)) return GetTeamFromJson(jsonString);
 
            throw new ArgumentException();
        }

        public async Task<List<Team>> GetTeamsByLeagueIdAsync(int leagueId)
        {
            CheckIfIntegerIsLessThanOrEqualToZero(leagueId);

            return await GetListFromEndpoint<Team>(ApiUrl + Endpoint + $"/league/{leagueId}", Endpoint);
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
            var teamString = JsonDocument.Parse(json).RootElement.GetProperty("team").GetRawText();
            var team = JsonConvert.DeserializeObject<Team>(teamString);
            var venueString = JsonDocument.Parse(json).RootElement.GetProperty("venue").GetRawText();
            team.Venue = JsonConvert.DeserializeObject<Venue>(venueString);
            return team;
        }
    }
}