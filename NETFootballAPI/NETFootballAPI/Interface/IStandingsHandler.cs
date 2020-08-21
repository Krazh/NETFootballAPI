using System.Collections.Generic;
using System.Threading.Tasks;

namespace NETFootballAPI
{
    public interface IStandingsHandler : IBaseApi
    {
        Task<List<Standings>> GetStandingsFromLeagueAsync(int leagueId);
    }
}