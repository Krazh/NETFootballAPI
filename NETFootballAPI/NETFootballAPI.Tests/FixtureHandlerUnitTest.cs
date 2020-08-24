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
        #region GetAllFixturesByDate

        [Test]
        public void GetAllFixturesByDate_DateTimeMustNotBeEmpty()
        {
            Assert.That(async () => await _handler.GetAllFixturesByDateAsync(new DateTime()), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetAllFixturesByDate_ShouldReturnValidList()
        {
            var date = DateTime.Parse("2019-04-27");
            var item = await _handler.GetAllFixturesByDateAsync(date);
            Assert.That(item.Count > 0);
        }

        public async Task GetAllFixturesByDate_InvalidDateShouldReturnEmptyList()
        {
            var item = await _handler.GetAllFixturesByDateAsync(DateTime.Parse("1444-11-11"));
            Assert.That(item.Count == 0);
        }
        
        #endregion
        #region GetAllFixturesByLeague

        [TestCase(-25, TestName = "Test with negative number")]
        [TestCase(0, TestName ="Test with 0")]
        public void GetAllFixturesByLeague_IdShouldNotBeLessThanOrEqualToZero(int id)
        {
            Assert.That(async () => await _handler.GetAllFixturesByLeagueAsync(id),
                Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetAllFixturesByLeague_InvalidIdShouldReturnEmptyList()
        {
            var item = await _handler.GetAllFixturesByLeagueAsync(int.MaxValue);
            Assert.That(item.Count == 0);
        }

        [Test] 
        public async Task GetAllFixturesByLeague_ShouldReturnPopulatedList() 
        { 
            var item = await _handler.GetAllFixturesByLeagueAsync(357);
            Assert.That(item.Count > 0);
        }

        #endregion
        #region GetAllFixturesByLeagueAndDate

        [TestCase(-25, TestName = "Test with negative number")]
        [TestCase(0, TestName = "Test with 0")]
        public void GetAllFixturesByLeagueAndDate_IdShouldNotBeLessThanOrEqualToZero(int id)
        {
            var validDate = DateTime.Parse("2019-04-27");
            Assert.That(async () => await _handler.GetAllFixturesByLeagueAndDateAsync(id, validDate), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetAllFixturesByLeagueAndDate_InvalidIdShouldReturnEmptyList()
        {
            var validDate = DateTime.Parse("2019-04-27");
            var item = await _handler.GetAllFixturesByLeagueAndDateAsync(int.MaxValue, validDate);
            Assert.That(item.Count == 0);
        }

        [Test]
        public async Task GetAllFixturesByLeagueAndDate_InvalidDateShouldReturnEmptyList()
        {
            var invalidDate = DateTime.Parse("1444-11-11");
            var item = await _handler.GetAllFixturesByLeagueAndDateAsync(357, invalidDate);
            Assert.That(item.Count == 0);
        }

        [Test]
        public async Task GetAllFixturesByLeagueAndDate_ShouldReturnPopulatedList()
        {
            var item = await _handler.GetAllFixturesByLeagueAndDateAsync(357, DateTime.Parse("2019-04-27"));
            Assert.That(item.Count > 0);
        }

        #endregion
        #region GetAllFixturesByLeagueAndRound

        [TestCase(-25, TestName = "Test with negative number")]
        [TestCase(0, TestName = "Test with 0")]
        public void GetAllFixturesByLeagueAndRound_IdShouldNotBeLessThanOrEqualToZero(int id)
        {
            var validRound = "Regular_Season_-_1";
            Assert.That(async () => await _handler.GetAllFixturesByLeagueAndRoundAsync(id, validRound), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetAllFixturesByLeagueAndRound_InvalidIdShouldReturnEmptyList()
        {
            var validRound = "Regular_Season_-_1";
            var item = await _handler.GetAllFixturesByLeagueAndRoundAsync(int.MaxValue, validRound);
            Assert.That(item.Count == 0);
        }

        [TestCase("round 1", TestName = "With Space")]
        [TestCase("Round1!", TestName = "With Symbols")]
        public void GetAllFixturesByLeagueAndRound_InvalidStringShouldReturnEmptyList(string test)
        {
            Assert.That(async () => await _handler.GetAllFixturesByLeagueAndRoundAsync(357, test), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetAllFixturesByLeagueAndRound_ShouldReturnPopulatedList()
        {
            var validRound = "Regular_Season_-_1";
            var item = await _handler.GetAllFixturesByLeagueAndRoundAsync(357, validRound);
            Assert.That(item.Count > 0);
        }

        #endregion
    }
}