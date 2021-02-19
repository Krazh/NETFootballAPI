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

            var venueTeam = await GetItemFromEndpoint<VenueTeam>(ApiUrl + Endpoint + $"?id={teamId}", Endpoint);

            if (venueTeam == null) return null;
            
            venueTeam.Team.Venue = venueTeam.Venue;
            return venueTeam.Team;
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

            var listFromEndpoint = await GetListFromEndpoint<VenueTeam>(ApiUrl + Endpoint + $"?league={leagueId}&season={season}", Endpoint);
            var returnList = new List<Team>();

            foreach (VenueTeam v in listFromEndpoint)
            {
                v.Team.Venue = v.Venue;
                returnList.Add(v.Team);
            }

            return returnList;
        }
        
        /// <param name="search">Should not contain accented or special characters. IE: ê should be replaced with e</param>
        public async Task<Team> GetTeamByStringSearchAsync(string search)
        {
            if(string.IsNullOrWhiteSpace(search) || search.Length <3) throw new ArgumentException();
            CheckIfStringContainsSymbols(search);

            var venueTeam = await GetItemFromEndpoint<VenueTeam>(ApiUrl + Endpoint + $"?name={search}", Endpoint );

            if (venueTeam == null) return null;

            venueTeam.Team.Venue = venueTeam.Venue;
            return venueTeam.Team;
        }
    }
}