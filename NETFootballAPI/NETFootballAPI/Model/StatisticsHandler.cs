using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NETFootballAPI
{
    public class StatisticsHandler : ApiHandler, IStatisticsHandler
    {
        private const string Endpoint = "statistics";
        public async Task<GoalStatistics> GetStatisticsByTeamIdAndLeagueIdAsync(int teamId, int leagueId)
        {
            CheckIfIdIsLessThanOrEqualToZero(teamId);
            CheckIfIdIsLessThanOrEqualToZero(leagueId);

            return await GetItemFromEndpoint<GoalStatistics>(ApiUrl + Endpoint + $"/{leagueId}/{teamId}", Endpoint);
        }

        public async Task<GoalStatistics> GetStatisticsByTeamIdAndLeagueIdAndEndDateAsync(int teamId, int leagueId,
            DateTime date)
        {
            CheckIfIdIsLessThanOrEqualToZero(teamId);
            CheckIfIdIsLessThanOrEqualToZero(leagueId);
            CheckIfYearIsInValidRange(date.Year);

            return await GetItemFromEndpoint<GoalStatistics>(
                ApiUrl + Endpoint + $"/{leagueId}/{teamId}/{FormatDateTime(date)}", Endpoint);
        }
    }
}