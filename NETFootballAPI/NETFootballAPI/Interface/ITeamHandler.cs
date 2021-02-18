using System.Collections.Generic;
using System.Threading.Tasks;

namespace NETFootballAPI
{
    public interface ITeamHandler : IBaseApi
    {
        Task<Team> GetTeamByIdAsync(int teamId);
        Task<List<Team>> GetTeamsByLeagueIdAndSeasonAsync(int leagueId, int season);
        /// <param name="search">Should not contain accented or special characters. IE: ê should be replace with e</param>
        Task<Team> GetTeamByStringSearchAsync(string search);
    }
}