using System;
using System.Threading.Tasks;
using NETFootballAPI;
using NUnit.Framework;

namespace UnitTest_NETFootballAPI
{
    public class LeagueHandlerUnitTest
    {
        private readonly ApiHandler _handler = new ApiHandler();
        [SetUp]
        public void Setup()
        {
            var key = "thisIsATestString";
            var url = "https://www.api-football.com/demo/v2/";
            _handler.SetApiKey(key);
            _handler.SetApiUrl(url);
        }

        #region GetAllLeagues

        [Test]
        public async Task GetAllLeagues_ReturnsSomething()
        {
            var items = await _handler.GetAllLeaguesAsync();
            
            Assert.That(items.Count > 0);
        }

        #endregion
        #region GetLeaguesById

        [TestCase(-25, TestName = "Negative number")]
        [TestCase(0, TestName = "Zero")]
        public void GetLeagueById_IdShouldBeHigherThan0(int testId)
        {
            Assert.That(async () => await _handler.GetLeagueByIdAsync(testId), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetLeagueById_InvalidIdShouldReturnNullObject()
        {
            var item = await _handler.GetLeagueByIdAsync(int.MaxValue);
            Assert.That(item == null);
        }


        #endregion
        #region GetLeaguesByTeamId

        [TestCase(-25, TestName = "Negative number")]
        [TestCase(0, TestName = "Zero")]
        public void GetLeaguesByTeamId_IdShouldBeHigherThan0(int testId)
        {
            Assert.That(async () => await _handler.GetLeaguesByTeamIdAsync(testId), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetLeaguesByTeamId_InvalidIdShouldReturnNullObject()
        {
            var item = await _handler.GetLeaguesByTeamIdAsync(int.MaxValue);
            Assert.That(item == null);
        }

        [Test]
        public async Task GetLeaguesByTeamId_ShouldReturnValidListOfLeagues()
        {
            // Assert. Test api has 4 leagues available, LeagueId 357 is Brazil and TeamId 15 is a team from Brazil.
            var item = await _handler.GetLeaguesByTeamIdAsync(15);
            Assert.That(item.Count > 0);
        }

        #endregion
        #region GetLeaguesByTeamIdAndSeason
        [TestCase(-25, 2018, TestName = "Negative number")]
        [TestCase(0, 2018, TestName = "Zero")]
        public void GetLeaguesByTeamIdAndSeason_IdShouldBeHigherThan0(int testId, int season)
        {
            Assert.That(async () => await _handler.GetLeaguesByTeamIdAndSeasonAsync(testId, season), Throws.TypeOf<ArgumentException>());
        }
        
        [Test]
        public async Task GetLeaguesByTeamIdAndSeason_InvalidIdShouldReturnNullObject()
        {
            var item = await _handler.GetLeaguesByTeamIdAndSeasonAsync(int.MaxValue, 2018);
            Assert.That(item == null);
        }
        
        [Test]
        public async Task GetLeaguesByTeamIdAndSeason_ShouldReturnValidListOfLeagues()
        {
            // Assert. Test api has 4 leagues available, LeagueId 357 is Brazil and TeamId 15 is a team from Brazil. 
            // Due to limitations of the demo url only the season 2019 has data for the league. 
            var item = await _handler.GetLeaguesByTeamIdAndSeasonAsync(15,2019);
            Assert.That(item.Count > 0);
        }
        #endregion
        #region GetLeagueByStringSearch

        [Test]
        public void GetLeagueByStringSearch_StringParameterMustNotBeNullOrWhiteSpace()
        {
            Assert.That(async () => await _handler.GetLeagueByStringSearchAsync(""), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetLeagueByStringSearch_ShouldReturnValidLeague()
        {
            // Test API has the league Serie A available from Brazil
            var validLeagueName = "Serie A";
            var item = await _handler.GetLeagueByStringSearchAsync(validLeagueName);
            Assert.That(item.Name == validLeagueName);
        }

        [Test]
        public void GetLeagueByStringSearch_StringShouldNotContainSymbols()
        {
            Assert.That(async () => await _handler.GetLeagueByStringSearchAsync("Braz!l"), Throws.TypeOf<ArgumentException>());
        }

        #endregion
        #region GetLeaguesByCountry

        [Test]
        public void GetLeaguesByCountry_StringShouldNotBeNullOrWhitespace()
        {
            Assert.That(async () => await _handler.GetLeaguesByCountryAsync(""), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void GetLeaguesByCountry_StringShouldNotContainSymbols()
        {
            Assert.That(async () => await _handler.GetLeaguesByCountryAsync("Braz!l"),
                Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetLeaguesByCountry_ShouldReturnListOfLeagues()
        {
            var country = "Brazil";
            var item = await _handler.GetLeaguesByCountryAsync(country);
            Assert.That(item.Count > 0 && item[0].Country == country);
        }

        #endregion
    }
}