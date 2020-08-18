using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NETFootballAPI
{
    public partial class ApiHandler
    {
        public async Task<List<League>> GetAllLeagues()
        {
            var endpoint = "leagues";

            try
            {
                var content = await _client.GetStringAsync(_apiUrl + endpoint);

                var jObj = DeserializeJson(content, endpoint);
            
                var leagues = new List<League>();

                if (jObj == null) return leagues;
                leagues.AddRange(from object? ob in jObj select JsonConvert.DeserializeObject<League>(ob.ToString()!));

                return leagues;
            }
            catch (Exception e)
            {
                // TODO Implement error logging
                Console.WriteLine(e);
                return new List<League>();
            }
        }

        public async Task<League> GetLeagueById(int id)
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

        private static void CheckIfIdIsLessThanOrEqualToZero(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than or equal to 0");
        }

        public async Task<League> GetLeagueByTeamId(int id)
        {
            CheckIfIdIsLessThanOrEqualToZero(id);

            var endpoint = "leagues";

            try
            {
                var content = await _client.GetStringAsync(_apiUrl + endpoint + $"/team/{id}");
                var jObj = DeserializeJson(content, endpoint);

                return GetFirstObjectFromJArray<League>(jObj);
            }
            catch (Exception e)
            {
                // TODO Implement error logging
                Console.WriteLine(e);
                return null;
            }

            return new League();
        }
    }
}