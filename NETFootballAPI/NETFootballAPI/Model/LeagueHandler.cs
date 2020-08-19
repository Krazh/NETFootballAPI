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

            try
            {
                var content = await _client.GetStringAsync(_apiUrl + endpoint);

                var jObj = DeserializeJson(content, endpoint);
                
                return GetListFromJArray<League>(jObj);
            }
            catch (Exception e)
            {
                // TODO Implement error logging
                return new List<League>();
            }
        }

        public async Task<League> GetLeagueByIdAsync(int id)
        {
            CheckIfIdIsLessThanOrEqualToZero(id);
            
            var endpoint = "leagues";

            try
            {
                var content = await _client.GetStringAsync(_apiUrl + endpoint + $"/league/{id}");
                var jObj = DeserializeJson(content, endpoint);

                if (jObj?.First == null) throw new NullReferenceException();
                
                return GetFirstObjectFromJArray<League>(jObj);
            }
            
            catch (Exception e)
            {
                // TODO Implement error logging
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<List<League>> GetLeaguesByTeamIdAsync(int teamId)
        {
            CheckIfIdIsLessThanOrEqualToZero(teamId);

            var endpoint = "leagues";

            try
            {
                var content = await _client.GetStringAsync(_apiUrl + endpoint + $"/team/{teamId}");
                var jObj = DeserializeJson(content, endpoint);
                
                return GetListFromJArray<League>(jObj);
            }
            catch (Exception e)
            {
                // TODO Implement error logging
                return null;
            }
        }

        public async Task<List<League>> GetLeaguesByTeamIdAndSeasonAsync(int teamId, int season)
        {
            CheckIfIdIsLessThanOrEqualToZero(teamId);
            CheckIfYearIsInValidRange(season);

            var endpoint = "leagues";

            try
            {
                var content = await _client.GetStringAsync(_apiUrl + endpoint + $"/team/{teamId}/{season}");
                var jObj = DeserializeJson(content, endpoint);

                return GetListFromJArray<League>(jObj);
            }
            catch (Exception e)
            {
                // TODO Implement error logging
                return null;
            }
        }

        public async Task<League> GetLeagueByStringSearchAsync(string search)
        {
            if (string.IsNullOrWhiteSpace(search) || search.Length <3) throw new ArgumentException();
            CheckIfStringContainsSymbols(search);
            search = search.Replace(' ', '_');
            var endpoint = "leagues";

            try
            {
                var content = await _client.GetStringAsync(_apiUrl + endpoint + $"/search/{search}");
                var jObj = DeserializeJson(content, endpoint);

                return GetFirstObjectFromJArray<League>(jObj);
            }
            catch (Exception e)
            {
                // TODO Implement error logging
                return null;
            }
        }

        public async Task<List<League>> GetLeaguesByCountryAsync(string country)
        {
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException();
            CheckIfStringContainsSymbols(country);
            var endpoint = "leagues";

            try
            {
                var content = await _client.GetStringAsync(_apiUrl + endpoint + $"/country/{country}");
                var jObj = DeserializeJson(content, endpoint);

                return GetListFromJArray<League>(jObj);
            }
            catch (Exception e)
            {
                // TODO Implement error logging
                return null;
            }
        }

        public async Task<List<League>> GetLeaguesByCountryAndSeasonAsync(string country, int season)
        {
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException();
            CheckIfStringContainsSymbols(country);
            CheckIfYearIsInValidRange(season);
            var endpoint = "leagues";

            try
            {
                var content = await _client.GetStringAsync(_apiUrl + endpoint + $"/country/{country}/{season}");
                var jObj = DeserializeJson(content, endpoint);

                return GetListFromJArray<League>(jObj);
            }
            catch (Exception e)
            {
                // TODO Implement error logging
                return null;
            }
        }

        public async Task<List<League>> GetLeaguesByCountryCodeAsync(string code)
        {
            if(string.IsNullOrWhiteSpace(code) || code.Length != 2) throw new ArgumentException();
            var endpoint = "leagues";

            try
            {
                var content = await _client.GetStringAsync(_apiUrl + endpoint + $"/country/{code}");
                var array = DeserializeJson(content, endpoint);

                return GetListFromJArray<League>(array);
            }
            catch (Exception e)
            {
                // TODO Implement error logging
                return null;
            }
        }

        public async Task<List<League>> GetLeaguesByCountryCodeAndSeasonAsync(string code, int season)
        {
            if(string.IsNullOrWhiteSpace(code) || code.Length != 2) throw new ArgumentException();
            CheckIfStringContainsSymbols(code);
            CheckIfYearIsInValidRange(season);
            var endpoint = "leagues";

            try
            {
                var content = await _client.GetStringAsync(_apiUrl + endpoint + $"/country/{code}/{season}");
                var array = DeserializeJson(content, endpoint);

                return GetListFromJArray<League>(array);
            }
            catch (Exception e)
            {
                // TODO Implement error logging
                return null;
            }
        }

        public async Task<List<League>> GetLeaguesBySeasonAsync(int year)
        {
            CheckIfYearIsInValidRange(year);
            var endpoint = "leagues";

            try
            {
                var content = await _client.GetStringAsync(_apiUrl + endpoint + $"/season/{year}");
                var array = DeserializeJson(content, endpoint);

                return GetListFromJArray<League>(array);
            }
            catch (Exception e)
            {
                // TODO Implement error logging
                return null;
            }
        }

        public async Task<List<League>> GetSeasonsAvailableForLeagueAsync(int leagueId)
        {
            if (leagueId <= 0) throw new ArgumentException();
            var endpoint = "leagues";
            
            try
            {
                var content = await _client.GetStringAsync(_apiUrl + endpoint + $"/seasonsavailable/{leagueId}");
                var array = DeserializeJson(content, endpoint);

                return GetListFromJArray<League>(array);
            }
            catch (Exception e)
            {
                // TODO Implement error logging
                return null;
            }
        }

        public async Task<List<League>> GetSeasonsAvailableForLeagueAsync(int leagueId, int season)
        {
            if (leagueId <= 0) throw new ArgumentException();
            CheckIfYearIsInValidRange(season);
            var endpoint = "leagues";
            
            try
            {
                var content = await _client.GetStringAsync(_apiUrl + endpoint + $"/seasonsavailable/{leagueId}/{season}");
                var array = DeserializeJson(content, endpoint);

                return GetListFromJArray<League>(array);
            }
            catch (Exception e)
            {
                // TODO Implement error logging
                return null;
            }
        }

        public async Task<List<League>> GetCurrentLeaguesAsync()
        {
            var endpoint = "leagues";
            
            try
            {
                var content = await _client.GetStringAsync(_apiUrl + endpoint + "/current/");
                var array = DeserializeJson(content, endpoint);

                return GetListFromJArray<League>(array);
            }
            catch (Exception e)
            {
                // TODO Implement error logging
                return null;
            }
        }

        public async Task<List<League>> GetCurrentLeaguesByCountryAsync(string country)
        {
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException();
            CheckIfStringContainsSymbols(country);
            var endpoint = "leagues";
            
            try
            {
                var content = await _client.GetStringAsync(_apiUrl + endpoint + $"/current/{country}");
                var array = DeserializeJson(content, endpoint);

                return GetListFromJArray<League>(array);
            }
            catch (Exception e)
            {
                // TODO Implement error logging
                return null;
            }
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