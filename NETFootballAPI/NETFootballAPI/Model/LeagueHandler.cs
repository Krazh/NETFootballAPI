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

    public class LeagueHandler : ApiHandler, ILeagueHandler
    {
        public async Task<List<League>> GetAllLeaguesAsync()
        {
            var endpoint = "leagues";
            return await GetListFromEndpoint<League>(ApiUrl + endpoint, endpoint);
        }

        public async Task<League> GetLeagueByIdAsync(int id)
        {
            CheckIfIdIsLessThanOrEqualToZero(id);
            var endpoint = "leagues";
            return await GetItemFromEndpoint<League>(ApiUrl + endpoint + $"/league/{id}", endpoint);
        }

        public async Task<List<League>> GetLeaguesByTeamIdAsync(int teamId)
        {
            CheckIfIdIsLessThanOrEqualToZero(teamId);
            var endpoint = "leagues";
            return await GetListFromEndpoint<League>(ApiUrl + endpoint + $"/team/{teamId}", endpoint);
        }

        public async Task<List<League>> GetLeaguesByTeamIdAndSeasonAsync(int teamId, int season)
        {
            CheckIfIdIsLessThanOrEqualToZero(teamId);
            CheckIfYearIsInValidRange(season);
            var endpoint = "leagues";
            return await GetListFromEndpoint<League>(ApiUrl + endpoint + $"/team/{teamId}/{season}", endpoint);
        }

        public async Task<League> GetLeagueByStringSearchAsync(string search)
        {
            if (string.IsNullOrWhiteSpace(search) || search.Length <3) throw new ArgumentException();
            CheckIfStringContainsSymbols(search);
            search = search.Replace(' ', '_');
            var endpoint = "leagues";
            return await GetItemFromEndpoint<League>(ApiUrl + endpoint + $"/search/{search}", endpoint);
        }

        public async Task<List<League>> GetLeaguesByCountryAsync(string country)
        {
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException();
            CheckIfStringContainsSymbols(country);
            var endpoint = "leagues";
            return await GetListFromEndpoint<League>(ApiUrl + endpoint + $"/country/{country}", endpoint);
        }

        public async Task<List<League>> GetLeaguesByCountryAndSeasonAsync(string country, int season)
        {
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException();
            CheckIfStringContainsSymbols(country);
            CheckIfYearIsInValidRange(season);
            var endpoint = "leagues";
            return await GetListFromEndpoint<League>(ApiUrl + endpoint + $"/country/{country}/{season}", endpoint);
        }

        public async Task<List<League>> GetLeaguesByCountryCodeAsync(string code)
        {
            if(string.IsNullOrWhiteSpace(code) || code.Length != 2) throw new ArgumentException();
            var endpoint = "leagues";
            return await GetListFromEndpoint<League>(ApiUrl + endpoint + $"/country/{code}", endpoint);
        }

        public async Task<List<League>> GetLeaguesByCountryCodeAndSeasonAsync(string code, int season)
        {
            if(string.IsNullOrWhiteSpace(code) || code.Length != 2) throw new ArgumentException();
            CheckIfStringContainsSymbols(code);
            CheckIfYearIsInValidRange(season);
            var endpoint = "leagues";
            return await GetListFromEndpoint<League>(ApiUrl + endpoint + $"/country/{code}/{season}", endpoint);
        }

        public async Task<List<League>> GetLeaguesBySeasonAsync(int year)
        {
            CheckIfYearIsInValidRange(year);
            var endpoint = "leagues";
            return await GetListFromEndpoint<League>(ApiUrl + endpoint + $"/season/{year}", endpoint);
        }

        public async Task<List<League>> GetSeasonsAvailableForLeagueAsync(int leagueId)
        {
            if (leagueId <= 0) throw new ArgumentException();
            var endpoint = "leagues";
            return await GetListFromEndpoint<League>(ApiUrl + endpoint + $"/seasonsAvailable/{leagueId}", endpoint);
        }

        public async Task<List<League>> GetSeasonsAvailableForLeagueAsync(int leagueId, int season)
        {
            if (leagueId <= 0) throw new ArgumentException();
            CheckIfYearIsInValidRange(season);
            var endpoint = "leagues"; 
            return await GetListFromEndpoint<League>(ApiUrl + endpoint + $"/seasonsAvailable/{leagueId}/{season}",
                endpoint);
        }

        public async Task<List<League>> GetCurrentLeaguesAsync()
        {
            var endpoint = "leagues";
            return await GetListFromEndpoint<League>(ApiUrl + endpoint + "/current/", endpoint);
        }

        public async Task<List<League>> GetCurrentLeaguesByCountryAsync(string country)
        {
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException();
            CheckIfStringContainsSymbols(country);
            var endpoint = "leagues";

            return await GetListFromEndpoint<League>(ApiUrl + endpoint + $"/current/{country}", endpoint);
        }

        public async Task<List<League>> GetLeaguesByTypeAsync(string type)
        {
            if (string.IsNullOrWhiteSpace(type)) throw new ArgumentException();
            CheckIfStringContainsSymbols(type);
            var endpoint = "leagues";

            return await GetListFromEndpoint<League>(ApiUrl + endpoint + $"/type/{type}", endpoint);
        }

        public async Task<List<League>> GetLeaguesByTypeAndCountryAsync(string type, string country)
        {
            if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(country)) throw new ArgumentException();
            CheckIfStringContainsSymbols(type);
            CheckIfStringContainsSymbols(country);
            var endpoint = "leagues";

            return await GetListFromEndpoint<League>(ApiUrl + endpoint + $"/type/{type}/{country}", endpoint);
        }

        public async Task<List<League>> GetLeaguesByTypeAndCountryAndSeasonAsync(string type, string country,
            int season)
        {
            if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(country)) throw new ArgumentException();
            CheckIfStringContainsSymbols(type);
            CheckIfStringContainsSymbols(country);
            CheckIfYearIsInValidRange(season);
            var endpoint = "leagues";

            return await GetListFromEndpoint<League>(ApiUrl + endpoint + $"/type/{type}/{country}/{season}", endpoint);
        }

        public async Task<List<League>> GetLeaguesByTypeAndSeasonAsync(string type, int season)
        {
            if (string.IsNullOrWhiteSpace(type)) throw new ArgumentException();
            CheckIfStringContainsSymbols(type);
            CheckIfYearIsInValidRange(season);
            var endpoint = "leagues";

            return await GetListFromEndpoint<League>(ApiUrl + endpoint + $"/type/{type}/{season}", endpoint);
        }
        
    }
}