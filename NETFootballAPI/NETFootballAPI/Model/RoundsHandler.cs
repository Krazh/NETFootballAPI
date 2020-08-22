using System.Collections.Generic;
using System.Threading.Tasks;

namespace NETFootballAPI
{
    public class RoundsHandler : ApiHandler, IRoundsHandler
    {
        public async Task<List<string>> GetRoundsAvailableByLeagueIdAsync(int leagueId)
        {
            CheckIfIdIsLessThanOrEqualToZero(leagueId);
            var endpoint = "fixtures";

            return await GetListFromEndpoint<string>(ApiUrl + endpoint + $"/rounds/{leagueId}", endpoint);
        }

        public async Task<string> GetCurrentRoundsAvailableByLeagueIdAsync(int leagueId)
        {
            CheckIfIdIsLessThanOrEqualToZero(leagueId);
            var endpoint = "fixtures";

            return await GetItemFromEndpoint<string>(ApiUrl + endpoint + $"/rounds/{leagueId}/current", endpoint);
        }
    }
}