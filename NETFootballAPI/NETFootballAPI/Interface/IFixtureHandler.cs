using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NETFootballAPI
{
    public interface IFixtureHandler : IBaseApi
    {
        Task<Fixture> GetFixtureByIdAsync(int fixtureId);
        Task<List<Fixture>> GetAllLiveFixturesAsync();
        Task<List<Fixture>> GetAllLiveFixturesBySeveralLeaguesAsync(List<int> leagueId);
        Task<List<Fixture>> GetAllFixturesByDateAsync(DateTime date);
        Task<List<Fixture>> GetAllFixturesByLeagueAsync(int leagueId);
        Task<List<Fixture>> GetAllFixturesByLeagueAndDateAsync(int leagueId, DateTime date);
        Task<List<Fixture>> GetAllFixturesByLeagueAndRoundAsync(int leagueId, string round);
        Task<List<Fixture>> GetNextNumberOfFixturesByLeagueAsync(int leagueId, int numberOfFixtures);
        Task<List<Fixture>> GetLastNumberOfFixturesByLeagueAsync(int leagueId, int numberOfFixtures);
        Task<List<Fixture>> GetAllFixturesByTeamAsync(int teamId);
        Task<List<Fixture>> GetAllFixturesByTeamAndLeagueAsync(int teamId, int leagueId);
        Task<List<Fixture>> GetNextNumberOfFixturesByTeamAsync(int teamId, int numberOfFixtures);
        Task<List<Fixture>> GetLastNumberOfFixturesByTeamAsync(int teamId, int numberOfFixtures);
    }
}