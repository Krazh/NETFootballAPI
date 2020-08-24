using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NETFootballAPI
{
    public class FixtureHandler : ApiHandler, IFixtureHandler
    {
        private const string Endpoint = "fixtures";
        public async Task<Fixture> GetFixtureByIdAsync(int fixtureId)
        {
            CheckIfIntegerIsLessThanOrEqualToZero(fixtureId);
            var url = ApiUrl + Endpoint + $"/id/{fixtureId}";

            return await GetItemFromEndpoint<Fixture>(url, Endpoint);
        }

        /// <summary>
        /// Unsure how to test this tbh as this only returns values when matches are happening. 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Fixture>> GetAllLiveFixturesAsync()
        {
            var url = ApiUrl + Endpoint + "/live";

            return await GetListFromEndpoint<Fixture>(url, Endpoint);
        }

        /// <summary>
        /// Can't test if the method returns a valid response since it only does it while there are matches playing
        /// </summary>
        /// <param name="leagueId"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public async Task<List<Fixture>> GetAllLiveFixturesBySeveralLeaguesAsync(List<int> leagueId)
        {
            var leagueIds = "";
            foreach (int i in leagueId)
            {
                leagueIds += string.IsNullOrWhiteSpace(leagueIds) ? "" : "-";
                CheckIfIntegerIsLessThanOrEqualToZero(i);
                leagueIds += i.ToString();
            }
            var url = ApiUrl + Endpoint + $"/live/{leagueIds}";

            return await GetListFromEndpoint<Fixture>(url, Endpoint);
        }

        public Task<List<Fixture>> GetAllFixturesByDateAsync(DateTime date)
        {
            CheckIfDateTimeIsValid(date);
            var url = ApiUrl + Endpoint + $"/date/{FormatDateTime(date)}";
            return GetListFromEndpoint<Fixture>(url, Endpoint);
        }

        public Task<List<Fixture>> GetAllFixturesByLeagueAsync(int leagueId)
        {
            CheckIfIntegerIsLessThanOrEqualToZero(leagueId);
            var url = ApiUrl + Endpoint + $"/league/{leagueId}";
            return GetListFromEndpoint<Fixture>(url, Endpoint);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="leagueId"></param>
        /// <param name="date"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public Task<List<Fixture>> GetAllFixturesByLeagueAndDateAsync(int leagueId, DateTime date)
        {
            CheckIfIntegerIsLessThanOrEqualToZero(leagueId);
            var url = ApiUrl + Endpoint + $"/league/{leagueId}/{FormatDateTime(date)}";
            return GetListFromEndpoint<Fixture>(url, Endpoint);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="leagueId">Must be a valid integer higher than zero.</param>
        /// <param name="round">Valid returns from /fixtures/rounds endpoint only. Spaces should be replaced with underscores</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Task<List<Fixture>> GetAllFixturesByLeagueAndRoundAsync(int leagueId, string round)
        {
            CheckIfIntegerIsLessThanOrEqualToZero(leagueId);
            CheckIfStringContainsSymbols(round);
            if (round.Contains(' ')) throw new ArgumentException();
            var url = ApiUrl + Endpoint + $"/league/{leagueId}/{round}";
            return GetListFromEndpoint<Fixture>(url, Endpoint);
        }

        /// <summary>
        /// Unable to test a valid return at current time as demo API doesn't contain any future fixtures
        /// </summary>
        /// <param name="leagueId">Must be a valid integer higher than zero.</param>
        /// <param name="numberOfFixtures">Must be a valid integer higher than zero.</param>
        /// <returns></returns>
        public Task<List<Fixture>> GetNextNumberOfFixturesByLeagueAsync(int leagueId, int numberOfFixtures)
        {
            CheckIfIntegerIsLessThanOrEqualToZero(leagueId);
            CheckIfIntegerIsLessThanOrEqualToZero(numberOfFixtures);
            var url = ApiUrl + Endpoint + $"/league/{leagueId}/next/{numberOfFixtures}";
            return GetListFromEndpoint<Fixture>(url, Endpoint);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="leagueId">Must be a valid integer higher than zero.</param>
        /// <param name="numberOfFixtures">Must be a valid integer higher than zero.</param>
        /// <returns></returns>
        public Task<List<Fixture>> GetLastNumberOfFixturesByLeagueAsync(int leagueId, int numberOfFixtures)
        {
            CheckIfIntegerIsLessThanOrEqualToZero(leagueId);
            CheckIfIntegerIsLessThanOrEqualToZero(numberOfFixtures);
            var url = ApiUrl + Endpoint + $"/league/{leagueId}/last/{numberOfFixtures}";
            return GetListFromEndpoint<Fixture>(url, Endpoint);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="teamId">Must be a valid integer higher than zero.</param>
        /// <returns></returns>
        public Task<List<Fixture>> GetAllFixturesByTeamAsync(int teamId)
        {
            CheckIfIntegerIsLessThanOrEqualToZero(teamId);
            var url = ApiUrl + Endpoint + $"/team/{teamId}";
            return GetListFromEndpoint<Fixture>(url, Endpoint);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="teamId">Must be a valid integer higher than zero.</param>
        /// <param name="leagueId">Must be a valid integer higher than zero.</param>
        /// <returns></returns>
        public Task<List<Fixture>> GetAllFixturesByTeamAndLeagueAsync(int teamId, int leagueId)
        {
            CheckIfIntegerIsLessThanOrEqualToZero(teamId);
            CheckIfIntegerIsLessThanOrEqualToZero(leagueId);
            var url = ApiUrl + Endpoint + $"/team/{teamId}/{leagueId}";
            return GetListFromEndpoint<Fixture>(url, Endpoint);
        }

        public Task<List<Fixture>> GetNextNumberOfFixturesByTeamAsync(int teamId, int numberOfFixtures)
        {
            throw new NotImplementedException();
        }

        public Task<List<Fixture>> GetLastNumberOfFixturesByTeamAsync(int teamId, int numberOfFixtures)
        {
            throw new NotImplementedException();
        }
    }
}