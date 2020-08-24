using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NETFootballAPI
{
    public interface IStatisticsHandler : IBaseApi
    {
        Task<GoalStatistics> GetStatisticsByTeamIdAndLeagueIdAsync(int teamId, int leagueId);
        Task<GoalStatistics> GetStatisticsByTeamIdAndLeagueIdAndEndDateAsync(int teamId, int leagueId,
            DateTime date);
    }
}