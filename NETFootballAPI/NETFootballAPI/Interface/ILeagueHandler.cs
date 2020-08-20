using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NETFootballAPI
{
    public interface ILeagueHandler
    {
        void SetApiKey(string apiKey);
        void SetApiUrl(string apiUrl);
        Task<List<League>> GetAllLeaguesAsync();
        Task<League> GetLeagueByIdAsync(int id);
        Task<List<League>> GetLeaguesByTeamIdAsync(int teamId);
        Task<List<League>> GetLeaguesByTeamIdAndSeasonAsync(int teamId, int season);
        Task<League> GetLeagueByStringSearchAsync(string search);
        Task<List<League>> GetLeaguesByCountryAsync(string country);
        Task<List<League>> GetLeaguesByCountryAndSeasonAsync(string country, int season);
        Task<List<League>> GetLeaguesByCountryCodeAsync(string code);
        Task<List<League>> GetLeaguesByCountryCodeAndSeasonAsync(string code, int season);
        Task<List<League>> GetLeaguesBySeasonAsync(int year);
        Task<List<League>> GetSeasonsAvailableForLeagueAsync(int leagueId);
        Task<List<League>> GetSeasonsAvailableForLeagueAsync(int leagueId, int season);
        Task<List<League>> GetCurrentLeaguesAsync();
        Task<List<League>> GetCurrentLeaguesByCountryAsync(string country);
        Task<List<League>> GetLeaguesByTypeAsync(string type);
        Task<List<League>> GetLeaguesByTypeAndCountryAsync(string type, string country);

        Task<List<League>> GetLeaguesByTypeAndCountryAndSeasonAsync(string type, string country,
            int season);

        Task<List<League>> GetLeaguesByTypeAndSeasonAsync(string type, int season);
    }
}