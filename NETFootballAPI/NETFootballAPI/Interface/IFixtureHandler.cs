using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NETFootballAPI
{
    public interface IFixtureHandler : IBaseApi
    {
        Task<Fixture> GetFixtureByIdAsync(int fixtureId, string timeZone = null);
        Task<List<Fixture>> GetAllLiveFixturesAsync();
        Task<List<Fixture>> GetAllLiveFixturesBySeveralLeaguesAsync(List<int> leagueId, string timeZone = null);
        Task<List<Fixture>> GetAllFixturesByDateAsync(DateTime date, string timeZone = null);
        Task<List<Fixture>> GetAllFixturesByLeagueAsync(int leagueId, string timeZone = null);
        Task<List<Fixture>> GetAllFixturesByLeagueAndDateAsync(int leagueId, DateTime date, string timeZone = null);
        Task<List<Fixture>> GetAllFixturesByLeagueAndRoundAsync(int leagueId, string round, string timeZone = null);
        Task<List<Fixture>> GetNextNumberOfFixturesByLeagueAsync(int leagueId, int numberOfFixtures, string timeZone = null);
        Task<List<Fixture>> GetLastNumberOfFixturesByLeagueAsync(int leagueId, int numberOfFixtures, string timeZone = null);
        Task<List<Fixture>> GetAllFixturesByTeamAsync(int teamId, string timeZone = null);
        Task<List<Fixture>> GetAllFixturesByTeamAndLeagueAsync(int teamId, int leagueId, string timeZone = null);
        Task<List<Fixture>>
            GetNextNumberOfFixturesByTeamAsync(int teamId, int numberOfFixtures, string timeZone = null);
        Task<List<Fixture>>
            GetLastNumberOfFixturesByTeamAsync(int teamId, int numberOfFixtures, string timeZone = null);
    }
}