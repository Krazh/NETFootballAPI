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
            CheckIfIdIsLessThanOrEqualToZero(fixtureId);
            var url = ApiUrl + Endpoint + $"/id/{fixtureId}";

            return await GetItemFromEndpoint<Fixture>(url, Endpoint);
        }

        public Task<List<Fixture>> GetAllLiveFixturesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Fixture>> GetAllLiveFixturesBySeveralLeaguesAsync(List<int> leagueId, string timeZone = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<Fixture>> GetAllFixturesByDateAsync(DateTime date, string timeZone = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<Fixture>> GetAllFixturesByLeagueAsync(int leagueId, string timeZone = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<Fixture>> GetAllFixturesByLeagueAndDateAsync(int leagueId, DateTime date, string timeZone = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<Fixture>> GetAllFixturesByLeagueAndRoundAsync(int leagueId, string round, string timeZone = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<Fixture>> GetNextNumberOfFixturesByLeagueAsync(int leagueId, int numberOfFixtures, string timeZone = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<Fixture>> GetLastNumberOfFixturesByLeagueAsync(int leagueId, int numberOfFixtures, string timeZone = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<Fixture>> GetAllFixturesByTeamAsync(int teamId, string timeZone = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<Fixture>> GetAllFixturesByTeamAndLeagueAsync(int teamId, int leagueId, string timeZone = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<Fixture>> GetNextNumberOfFixturesByTeamAsync(int teamId, int numberOfFixtures, string timeZone = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<Fixture>> GetLastNumberOfFixturesByTeamAsync(int teamId, int numberOfFixtures, string timeZone = null)
        {
            throw new NotImplementedException();
        }
    }
}