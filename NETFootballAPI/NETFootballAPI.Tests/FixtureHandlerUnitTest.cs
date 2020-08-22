using System;
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
    }
}