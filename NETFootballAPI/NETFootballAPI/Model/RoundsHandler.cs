using System.Collections.Generic;
using System.Threading.Tasks;

namespace NETFootballAPI
{
    public class RoundsHandler : ApiHandler, IRoundsHandler
    {
        private const string Endpoint = "fixtures";
        public async Task<List<string>> GetRoundsAvailableByLeagueIdAsync(int leagueId)
        {
            CheckIfIdIsLessThanOrEqualToZero(leagueId);

            return await GetListFromEndpoint<string>(ApiUrl + Endpoint + $"/rounds/{leagueId}", Endpoint);
        }

        public async Task<string> GetCurrentRoundsAvailableByLeagueIdAsync(int leagueId)
        {
            CheckIfIdIsLessThanOrEqualToZero(leagueId);

            return await GetItemFromEndpoint<string>(ApiUrl + Endpoint + $"/rounds/{leagueId}/current", Endpoint);
        }
    }
}