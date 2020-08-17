using System;
using System.Threading.Tasks;
using NETFootballAPI;
using NUnit.Framework;

namespace UnitTest_NETFootballAPI
{
    public class LeagueHandlerUnitTest
    {
        private ApiHandler _handler = new ApiHandler();
        [SetUp]
        public void Setup()
        {
            var key = "5b0cecdeb8fb3c8ff17b33efa6923cbc";
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

        [Test]
        public async Task GetLeagueById_IdShouldBeHigherThan0_Test1()
        {
            Assert.That(async () => await _handler.GetLeagueById(-25), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetLeagueById_IdShouldBeHigherThan0_Test2()
        {
            Assert.That(async () => await _handler.GetLeagueById(0), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetLeagueById_InvalidIdShouldReturnNullObject()
        {
            var item = await _handler.GetLeagueById(999999999);
            Assert.That(item == null);
        }
    }
}