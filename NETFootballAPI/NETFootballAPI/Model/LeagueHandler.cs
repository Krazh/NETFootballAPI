using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NETFootballAPI
{
    public partial class ApiHandler
    {
        public async Task<List<League>> GetAllLeaguesAsync()
        {
            var endpoint = "leagues";
            return await GetListFromEndpoint<League>(_apiUrl + endpoint, endpoint);
        }

        public async Task<League> GetLeagueByIdAsync(int id)
        {
            CheckIfIdIsLessThanOrEqualToZero(id);
            var endpoint = "leagues";
            return await GetItemFromEndpoint<League>(_apiUrl + endpoint + $"/league/{id}", endpoint);
        }

        public async Task<List<League>> GetLeaguesByTeamIdAsync(int teamId)
        {
            CheckIfIdIsLessThanOrEqualToZero(teamId);
            var endpoint = "leagues";
            return await GetListFromEndpoint<League>(_apiUrl + endpoint + $"/team/{teamId}", endpoint);
        }

        public async Task<List<League>> GetLeaguesByTeamIdAndSeasonAsync(int teamId, int season)
        {
            CheckIfIdIsLessThanOrEqualToZero(teamId);
            CheckIfYearIsInValidRange(season);
            var endpoint = "leagues";
            return await GetListFromEndpoint<League>(_apiUrl + endpoint + $"/team/{teamId}/{season}", endpoint);
        }

        public async Task<League> GetLeagueByStringSearchAsync(string search)
        {
            if (string.IsNullOrWhiteSpace(search) || search.Length <3) throw new ArgumentException();
            CheckIfStringContainsSymbols(search);
            search = search.Replace(' ', '_');
            var endpoint = "leagues";
            return await GetItemFromEndpoint<League>(_apiUrl + endpoint + $"/search/{search}", endpoint);
        }

        public async Task<List<League>> GetLeaguesByCountryAsync(string country)
        {
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException();
            CheckIfStringContainsSymbols(country);
            var endpoint = "leagues";
            return await GetListFromEndpoint<League>(_apiUrl + endpoint + $"/country/{country}", endpoint);
        }

        public async Task<List<League>> GetLeaguesByCountryAndSeasonAsync(string country, int season)
        {
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException();
            CheckIfStringContainsSymbols(country);
            CheckIfYearIsInValidRange(season);
            var endpoint = "leagues";
            return await GetListFromEndpoint<League>(_apiUrl + endpoint + $"/country/{country}/{season}", endpoint);
        }

        public async Task<List<League>> GetLeaguesByCountryCodeAsync(string code)
        {
            if(string.IsNullOrWhiteSpace(code) || code.Length != 2) throw new ArgumentException();
            var endpoint = "leagues";
            return await GetListFromEndpoint<League>(_apiUrl + endpoint + $"/country/{code}", endpoint);
        }

        public async Task<List<League>> GetLeaguesByCountryCodeAndSeasonAsync(string code, int season)
        {
            if(string.IsNullOrWhiteSpace(code) || code.Length != 2) throw new ArgumentException();
            CheckIfStringContainsSymbols(code);
            CheckIfYearIsInValidRange(season);
            var endpoint = "leagues";
            return await GetListFromEndpoint<League>(_apiUrl + endpoint + $"/country/{code}/{season}", endpoint);
        }

        public async Task<List<League>> GetLeaguesBySeasonAsync(int year)
        {
            CheckIfYearIsInValidRange(year);
            var endpoint = "leagues";
            return await GetListFromEndpoint<League>(_apiUrl + endpoint + $"/season/{year}", endpoint);
        }

        public async Task<List<League>> GetSeasonsAvailableForLeagueAsync(int leagueId)
        {
            if (leagueId <= 0) throw new ArgumentException();
            var endpoint = "leagues";
            return await GetListFromEndpoint<League>(_apiUrl + endpoint + $"/seasonsavailable/{leagueId}", endpoint);
        }

        public async Task<List<League>> GetSeasonsAvailableForLeagueAsync(int leagueId, int season)
        {
            if (leagueId <= 0) throw new ArgumentException();
            CheckIfYearIsInValidRange(season);
            var endpoint = "leagues";
            return await GetListFromEndpoint<League>(_apiUrl + endpoint + $"/seasonsavailable/{leagueId}/{season}",
                endpoint);
        }

        public async Task<List<League>> GetCurrentLeaguesAsync()
        {
            var endpoint = "leagues";
            return await GetListFromEndpoint<League>(_apiUrl + endpoint + "/current/", endpoint);
        }

        public async Task<List<League>> GetCurrentLeaguesByCountryAsync(string country)
        {
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException();
            CheckIfStringContainsSymbols(country);
            var endpoint = "leagues";

            return await GetListFromEndpoint<League>(_apiUrl + endpoint + $"/current/{country}", endpoint);
        }

        public async Task<List<League>> GetLeaguesByTypeAsync(string type)
        {
            if (string.IsNullOrWhiteSpace(type)) throw new ArgumentException();
            CheckIfStringContainsSymbols(type);
            var endpoint = "leagues";

            return await GetListFromEndpoint<League>(_apiUrl + endpoint + $"/type/{type}", endpoint);
        }

        public async Task<List<League>> GetLeaguesByTypeAndCountryAsync(string type, string country)
        {
            if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(country)) throw new ArgumentException();
            CheckIfStringContainsSymbols(type);
            CheckIfStringContainsSymbols(country);
            var endpoint = "leagues";

            return await GetListFromEndpoint<League>(_apiUrl + endpoint + $"/type/{type}/{country}", endpoint);
        }

        public async Task<List<League>> GetLeaguesByTypeAndCountryAndSeasonAsync(string type, string country,
            int season)
        {
            if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(country)) throw new ArgumentException();
            CheckIfStringContainsSymbols(type);
            CheckIfStringContainsSymbols(country);
            CheckIfYearIsInValidRange(season);
            var endpoint = "leagues";

            return await GetListFromEndpoint<League>(_apiUrl + endpoint + $"/type/{type}/{country}/{season}", endpoint);
        }

        public async Task<List<League>> GetLeaguesByTypeAndSeasonAsync(string type, int season)
        {
            if (string.IsNullOrWhiteSpace(type)) throw new ArgumentException();
            CheckIfStringContainsSymbols(type);
            CheckIfYearIsInValidRange(season);
            var endpoint = "leagues";

            return await GetListFromEndpoint<League>(_apiUrl + endpoint + $"/type/{type}/{season}", endpoint);
        }
        #region Private Methods
        private static void CheckIfIdIsLessThanOrEqualToZero(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than or equal to 0");
        }

        private static void CheckIfStringContainsSymbols(string item)
        {
            if (Regex.IsMatch(item, "[!,@,#,$,%,^,&,*,?,~,£,(,)]")) throw new ArgumentException("String contains invalid symbols");
        }

        private static void CheckIfYearIsInValidRange(int year)
        {
            if (year <= 1900 || year >= (DateTime.Today.Year + 5))
                throw new ArgumentOutOfRangeException();
        }
        #endregion
    }
}