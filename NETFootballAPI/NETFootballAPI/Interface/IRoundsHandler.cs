using System.Collections.Generic;
using System.Threading.Tasks;

namespace NETFootballAPI
{
    public interface IRoundsHandler : IBaseApi
    {
        Task<List<string>> GetRoundsAvailableByLeagueIdAsync(int leagueId);
        Task<string> GetCurrentRoundsAvailableByLeagueIdAsync(int leagueId);
    }
}