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
        public void GetLeagueByTeamId_IdShouldBeHigherThan0(int testId)
        {
            Assert.That(async () => await _handler.GetLeagueByTeamId(testId), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetLeagueByTeamId_InvalidIdShouldReturnNullObject()
        {
            var item = await _handler.GetLeagueByTeamId(int.MaxValue);
            Assert.That(item == null);
        }

        [Test]
        public async Task GetLeagueByTeamId_ShouldReturnValidTeam()
        {
            // Assert. Test api has 4 leagues available, Id 357 is Brazil and Team Id 15 is a team from Brazil.
            var item = await _handler.GetLeagueByTeamId(15);
            Assert.That(item.Id == "357");
        }
    }
}