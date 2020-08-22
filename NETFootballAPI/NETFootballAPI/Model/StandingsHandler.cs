using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NETFootballAPI
{
    public class StandingsHandler : ApiHandler, IStandingsHandler
    {
        private const string Endpoint = "leagueTable";
        public async Task<List<Standings>> GetStandingsFromLeagueAsync(int leagueId)
        {
            CheckIfIdIsLessThanOrEqualToZero(leagueId);
            

            try
            {
                var content = await Client.GetStringAsync(ApiUrl + Endpoint + $"/{leagueId}");
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