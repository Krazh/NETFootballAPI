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
        private const string Endpoint = "leagues";
        public async Task<List<League>> GetAllLeaguesAsync()
        {
            return await GetListFromEndpoint<League>(ApiUrl + Endpoint, Endpoint);
        }

        public async Task<League> GetLeagueByIdAsync(int id)
        {
            CheckIfIntegerIsLessThanOrEqualToZero(id);
            return await GetItemFromEndpoint<League>(ApiUrl + Endpoint + $"/league/{id}", Endpoint);
        }

        public async Task<List<League>> GetLeaguesByTeamIdAsync(int teamId)
        {
            CheckIfIntegerIsLessThanOrEqualToZero(teamId);
            return await GetListFromEndpoint<League>(ApiUrl + Endpoint + $"/team/{teamId}", Endpoint);
        }

        public async Task<List<League>> GetLeaguesByTeamIdAndSeasonAsync(int teamId, int season)
        {
            CheckIfIntegerIsLessThanOrEqualToZero(teamId);
            CheckIfYearIsInValidRange(season);
            return await GetListFromEndpoint<League>(ApiUrl + Endpoint + $"/team/{teamId}/{season}", Endpoint);
        }

        public async Task<League> GetLeagueByStringSearchAsync(string search)
        {
            if (string.IsNullOrWhiteSpace(search) || search.Length <3) throw new ArgumentException();
            CheckIfStringContainsSymbols(search);
            search = search.Replace(' ', '_');
            return await GetItemFromEndpoint<League>(ApiUrl + Endpoint + $"/search/{search}", Endpoint);
        }

        public async Task<List<League>> GetLeaguesByCountryAsync(string country)
        {
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException();
            CheckIfStringContainsSymbols(country);
            return await GetListFromEndpoint<League>(ApiUrl + Endpoint + $"/country/{country}", Endpoint);
        }

        public async Task<List<League>> GetLeaguesByCountryAndSeasonAsync(string country, int season)
        {
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException();
            CheckIfStringContainsSymbols(country);
            CheckIfYearIsInValidRange(season);
            return await GetListFromEndpoint<League>(ApiUrl + Endpoint + $"/country/{country}/{season}", Endpoint);
        }

        public async Task<List<League>> GetLeaguesByCountryCodeAsync(string code)
        {
            if(string.IsNullOrWhiteSpace(code) || code.Length != 2) throw new ArgumentException();
            return await GetListFromEndpoint<League>(ApiUrl + Endpoint + $"/country/{code}", Endpoint);
        }

        public async Task<List<League>> GetLeaguesByCountryCodeAndSeasonAsync(string code, int season)
        {
            if(string.IsNullOrWhiteSpace(code) || code.Length != 2) throw new ArgumentException();
            CheckIfStringContainsSymbols(code);
            CheckIfYearIsInValidRange(season);
            return await GetListFromEndpoint<League>(ApiUrl + Endpoint + $"/country/{code}/{season}", Endpoint);
        }

        public async Task<List<League>> GetLeaguesBySeasonAsync(int year)
        {
            CheckIfYearIsInValidRange(year);
            return await GetListFromEndpoint<League>(ApiUrl + Endpoint + $"/season/{year}", Endpoint);
        }

        public async Task<List<League>> GetSeasonsAvailableForLeagueAsync(int leagueId)
        {
            if (leagueId <= 0) throw new ArgumentException();
            return await GetListFromEndpoint<League>(ApiUrl + Endpoint + $"/seasonsAvailable/{leagueId}", Endpoint);
        }

        public async Task<List<League>> GetSeasonsAvailableForLeagueAsync(int leagueId, int season)
        {
            if (leagueId <= 0) throw new ArgumentException();
            CheckIfYearIsInValidRange(season);
            return await GetListFromEndpoint<League>(ApiUrl + Endpoint + $"/seasonsAvailable/{leagueId}/{season}",
                Endpoint);
        }

        public async Task<List<League>> GetCurrentLeaguesAsync()
        {
            return await GetListFromEndpoint<League>(ApiUrl + Endpoint + "/current/", Endpoint);
        }

        public async Task<List<League>> GetCurrentLeaguesByCountryAsync(string country)
        {
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException();
            CheckIfStringContainsSymbols(country);

            return await GetListFromEndpoint<League>(ApiUrl + Endpoint + $"/current/{country}", Endpoint);
        }

        public async Task<List<League>> GetLeaguesByTypeAsync(string type)
        {
            if (string.IsNullOrWhiteSpace(type)) throw new ArgumentException();
            CheckIfStringContainsSymbols(type);

            return await GetListFromEndpoint<League>(ApiUrl + Endpoint + $"/type/{type}", Endpoint);
        }

        public async Task<List<League>> GetLeaguesByTypeAndCountryAsync(string type, string country)
        {
            if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(country)) throw new ArgumentException();
            CheckIfStringContainsSymbols(type);
            CheckIfStringContainsSymbols(country);

            return await GetListFromEndpoint<League>(ApiUrl + Endpoint + $"/type/{type}/{country}", Endpoint);
        }

        public async Task<List<League>> GetLeaguesByTypeAndCountryAndSeasonAsync(string type, string country,
            int season)
        {
            if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(country)) throw new ArgumentException();
            CheckIfStringContainsSymbols(type);
            CheckIfStringContainsSymbols(country);
            CheckIfYearIsInValidRange(season);

            return await GetListFromEndpoint<League>(ApiUrl + Endpoint + $"/type/{type}/{country}/{season}", Endpoint);
        }

        public async Task<List<League>> GetLeaguesByTypeAndSeasonAsync(string type, int season)
        {
            if (string.IsNullOrWhiteSpace(type)) throw new ArgumentException();
            CheckIfStringContainsSymbols(type);
            CheckIfYearIsInValidRange(season);

            return await GetListFromEndpoint<League>(ApiUrl + Endpoint + $"/type/{type}/{season}", Endpoint);
        }
        
    }
}