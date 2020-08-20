using System.Collections.Generic;
using System.Threading.Tasks;

namespace NETFootballAPI
{
    public interface ITeamHandler
    {
        Task<Team> GetTeamByIdAsync(int teamId);
        Task<List<Team>> GetTeamsByLeagueIdAsync(int leagueId);
        /// <param name="search">Should not contain accented or special characters. IE: ê should be replace with e</param>
        Task<Team> GetTeamByStringSearchAsync(string search);
    }
}