using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NETFootballAPI
{
    public class StandingsHandler : ApiHandler, IStandingsHandler
    {
        public async Task<List<Standings>> GetStandingsFromLeagueAsync(int leagueId)
        {
            CheckIfIdIsLessThanOrEqualToZero(leagueId);
            var endpoint = "leagueTable";

            try
            {
                var content = await Client.GetStringAsync(ApiUrl + endpoint + $"/{leagueId}");
                var jsonElement = JsonDocument.Parse(content).RootElement.GetProperty("api").GetProperty("standings")[0].GetRawText();
                return JsonConvert.DeserializeObject<List<Standings>>(jsonElement);
            }
            catch (Exception e)
            {
                // TODO Implement error logging
                return null!;
            }
        }
    }
}