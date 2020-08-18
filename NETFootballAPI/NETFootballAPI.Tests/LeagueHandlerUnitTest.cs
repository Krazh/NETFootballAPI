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

        [Test]
        public async Task GetAllLeagues_ReturnsSomething()
        {
            var items = await _handler.GetAllLeagues();
            
            Assert.That(items.Count > 0);
        }

        [TestCase(-25, TestName = "Negative number")]
        [TestCase(0, TestName = "Zero")]
        public void GetLeagueById_IdShouldBeHigherThan0(int testId)
        {
            Assert.That(async () => await _handler.GetLeagueById(testId), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetLeagueById_InvalidIdShouldReturnNullObject()
        {
            var item = await _handler.GetLeagueById(int.MaxValue);
            Assert.That(item == null);
        }

        [TestCase(-25, TestName = "Negative number")]
        [TestCase(0, TestName = "Zero")]
        public void GetLeaguesByTeamId_IdShouldBeHigherThan0(int testId)
        {
            Assert.That(async () => await _handler.GetLeaguesByTeamId(testId), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetLeaguesByTeamId_InvalidIdShouldReturnNullObject()
        {
            var item = await _handler.GetLeaguesByTeamId(int.MaxValue);
            Assert.That(item == null);
        }

        [Test]
        public async Task GetLeaguesByTeamId_ShouldReturnValidListOfLeagues()
        {
            // Assert. Test api has 4 leagues available, LeagueId 357 is Brazil and TeamId 15 is a team from Brazil.
            var item = await _handler.GetLeaguesByTeamId(15);
            Assert.That(item.Count > 0);
        }
        
        [TestCase(-25, 2018, TestName = "Negative number")]
        [TestCase(0, 2018, TestName = "Zero")]
        public void GetLeaguesByTeamIdAndSeason_IdShouldBeHigherThan0(int testId, int season)
        {
            Assert.That(async () => await _handler.GetLeaguesByTeamIdAndSeason(testId, season), Throws.TypeOf<ArgumentException>());
        }
        
        [Test]
        public async Task GetLeaguesByTeamIdAndSeason_InvalidIdShouldReturnNullObject()
        {
            var item = await _handler.GetLeaguesByTeamIdAndSeason(int.MaxValue, 2018);
            Assert.That(item == null);
        }
        
        [Test]
        public async Task GetLeaguesByTeamIdAndSeason_ShouldReturnValidListOfLeagues()
        {
            // Assert. Test api has 4 leagues available, LeagueId 357 is Brazil and TeamId 15 is a team from Brazil. 
            // Due to limitations of the demo url only the season 2019 has data for the league. 
            var item = await _handler.GetLeaguesByTeamIdAndSeason(15,2019);
            Assert.That(item.Count > 0);
        }
    }
}