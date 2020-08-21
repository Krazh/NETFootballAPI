using System;
using System.Threading.Tasks;
using NETFootballAPI;
using NUnit.Framework;

namespace UnitTest_NETFootballAPI
{
    public class StandingsHandlerUnitTest
    {
        private readonly IStandingsHandler _handler = new StandingsHandler();
        [SetUp]
        public void Setup()
        {
            var key = "thisIsATestString";
            var url = "https://www.api-football.com/demo/v2/";
            _handler.SetApiKey(key);
            _handler.SetApiUrl(url);
        }

        #region GetStandingsByLeague

        [TestCase(-25, TestName = "Negative number")]
        [TestCase(0, TestName = "Zero")]
        public void GetTeamById_IdShouldBeHigherThan0(int testId)
        {
            Assert.That(async () => await _handler.GetStandingsFromLeagueAsync(testId), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetTeamById_InvalidIdShouldReturnNullObject()
        {
            var item = await _handler.GetStandingsFromLeagueAsync(int.MaxValue);
            Assert.That(item == null);
        }

        [Test]
        public async Task GetTeamById_ShouldReturnValidLeague()
        {
            // TeamId 15 is a Serie A Team in Brazil that is available on the demo api
            var item = await _handler.GetStandingsFromLeagueAsync(357);
            Assert.That(item.Count > 0);
        }

        #endregion
    }
}