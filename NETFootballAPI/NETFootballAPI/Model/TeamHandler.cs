using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NETFootballAPI
{
    public partial class ApiHandler
    {
        public async Task<Team> GetTeamByIdAsync(int teamId)
        {
            CheckIfIdIsLessThanOrEqualToZero(teamId);
            var endpoint = "teams";

            return await GetItemFromEndpoint<Team>(_apiUrl + endpoint + $"/team/{teamId}", endpoint);
        }

        public async Task<List<Team>> GetTeamsByLeagueIdAsync(int leagueId)
        {
            CheckIfIdIsLessThanOrEqualToZero(leagueId);
            var endpoint = "teams";

            return await GetListFromEndpoint<Team>(_apiUrl + endpoint + $"/league/{leagueId}", endpoint);
        }
        
        /// <param name="search">Should not contain accented or special characters. IE: ê should be replace with e</param>
        public async Task<Team> GetTeamByStringSearchAsync(string search)
        {
            if(string.IsNullOrWhiteSpace(search) || search.Length <3) throw new ArgumentException();
            CheckIfStringContainsSymbols(search);
            var endpoint = "teams";
            search = search.Replace(' ', '_');

            return await GetItemFromEndpoint<Team>(_apiUrl + endpoint + $"/search/{search}", endpoint);
        }
    }
}