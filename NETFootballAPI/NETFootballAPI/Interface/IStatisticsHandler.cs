using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NETFootballAPI
{
    public interface IStatisticsHandler
    {
        Task<Statistics> GetStatisticsByTeamIdAndLeagueIdAsync(int teamId, int leagueId);

        Task<Statistics> GetStatisticsByTeamIdAndLeagueIdAndEndDateAsync(int teamId, int leagueId,
            DateTime date);

        void SetApiKey(string key);
        void SetApiUrl(string url);
    }
}