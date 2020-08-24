using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NETFootballAPI
{
    public class TeamHandler : ApiHandler, ITeamHandler
    {
        private const string Endpoint = "teams";
        public async Task<Team> GetTeamByIdAsync(int teamId)
        {
            CheckIfIntegerIsLessThanOrEqualToZero(teamId);

            return await GetItemFromEndpoint<Team>(ApiUrl + Endpoint + $"/team/{teamId}", Endpoint);
        }

        public async Task<List<Team>> GetTeamsByLeagueIdAsync(int leagueId)
        {
            CheckIfIntegerIsLessThanOrEqualToZero(leagueId);

            return await GetListFromEndpoint<Team>(ApiUrl + Endpoint + $"/league/{leagueId}", Endpoint);
        }
        
        /// <param name="search">Should not contain accented or special characters. IE: ê should be replace with e</param>
        public async Task<Team> GetTeamByStringSearchAsync(string search)
        {
            if(string.IsNullOrWhiteSpace(search) || search.Length <3) throw new ArgumentException();
            CheckIfStringContainsSymbols(search);
            search = search.Replace(' ', '_');

            return await GetItemFromEndpoint<Team>(ApiUrl + Endpoint + $"/search/{search}", Endpoint);
        }
    }
}