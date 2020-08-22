using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NETFootballAPI;
using NUnit.Framework;

namespace UnitTest_NETFootballAPI
{
    public class FixtureHandlerUnitTest
    {
        private readonly IFixtureHandler _handler = new FixtureHandler();
        [SetUp]
        public void Setup()
        {
            var url = "https://www.api-football.com/demo/v2/";
            _handler.SetApiUrl(url);
        }

        #region GetFixturesById
        [TestCase(-25, TestName = "Negative number")]
        [TestCase(0, TestName = "Zero")]
        public void GetFixtureById_IdShouldBeHigherThan0(int id)
        {
            Assert.That(async () => await _handler.GetFixtureByIdAsync(id), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetFixtureById_InvalidIdShouldReturnNullObject()
        {
            var item = await _handler.GetFixtureByIdAsync(int.MaxValue);
            Assert.That(item == null);
        }

        [Test]
        public async Task GetFixtureById_ValidIdShouldReturnValidFixture()
        {
            // FixtureId 380 is a valid fixture from the demo API in the league 357 (Brazil)
            var item = await _handler.GetFixtureByIdAsync(380);
            Assert.That(item.Id == 380 && item.LeagueId == 357);
        }
        #endregion
        #region GetAllLiveFixtures

        

        #endregion
        #region GetAllLiveFixturesBySeveralLeagues

        [TestCase(-25, 15, 20, TestName = "Negative number")]
        [TestCase(0, -20, 5, TestName = "Zero")]
        [TestCase(5, 20, -25, TestName = "Negative number")]
        public void GetAllLiveFixturesBySeveralLeagues(int id1, int id2, int id3)
        {
            Assert.That(async () => await _handler.GetAllLiveFixturesBySeveralLeaguesAsync(new List<int>{id1, id2, id3}),Throws.TypeOf<ArgumentException>());
        }

        #endregion
    }
}