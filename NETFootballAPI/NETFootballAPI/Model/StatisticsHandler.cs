using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NETFootballAPI
{
    public partial class ApiHandler
    {
        public async Task<List<Statistics>> GetStatisticsByTeamIdAndLeagueIdAsync(int teamId, int leagueId)
        {
            CheckIfIdIsLessThanOrEqualToZero(teamId);
            CheckIfIdIsLessThanOrEqualToZero(leagueId);
            var endpoint = "statistics";

            return await GetListFromEndpoint<Statistics>(_apiUrl + endpoint + $"/{leagueId}/{teamId}", endpoint);
        }

        public async Task<List<Statistics>> GetStatisticsByTeamIdAndLeagueIdAndEndDateAsync(int teamId, int leagueId,
            DateTime date)
        {
            CheckIfIdIsLessThanOrEqualToZero(teamId);
            CheckIfIdIsLessThanOrEqualToZero(leagueId);
            CheckIfYearIsInValidRange(date.Year);
            var endpoint = "statistics";

            return await GetListFromEndpoint<Statistics>(
                _apiUrl + endpoint + $"/{leagueId}/{teamId}/{date:yyyy-MM-dd}", endpoint);
        }
    }
}