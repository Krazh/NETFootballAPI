using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NETFootballAPI
{
    public class StatisticsHandler : ApiHandler, IStatisticsHandler
    {
        public async Task<Statistics> GetStatisticsByTeamIdAndLeagueIdAsync(int teamId, int leagueId)
        {
            CheckIfIdIsLessThanOrEqualToZero(teamId);
            CheckIfIdIsLessThanOrEqualToZero(leagueId);
            var endpoint = "statistics";

            return await GetItemFromEndpoint<Statistics>(ApiUrl + endpoint + $"/{leagueId}/{teamId}", endpoint);
        }

        public async Task<Statistics> GetStatisticsByTeamIdAndLeagueIdAndEndDateAsync(int teamId, int leagueId,
            DateTime date)
        {
            CheckIfIdIsLessThanOrEqualToZero(teamId);
            CheckIfIdIsLessThanOrEqualToZero(leagueId);
            CheckIfYearIsInValidRange(date.Year);
            var endpoint = "statistics";

            return await GetItemFromEndpoint<Statistics>(
                ApiUrl + endpoint + $"/{leagueId}/{teamId}/{date:yyyy-MM-dd}", endpoint);
        }
    }
}