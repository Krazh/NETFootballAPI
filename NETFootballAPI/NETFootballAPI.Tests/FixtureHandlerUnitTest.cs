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
        #region GetNextNumberOfFixturesByLeague

        [TestCase(-25, TestName = "Test with negative number")]
        [TestCase(0, TestName = "Test with 0")]
        public void GetNextNumberOfFixturesByLeague_IdShouldNotBeLessThanOrEqualToZero(int id)
        {
            Assert.That(async () => await _handler.GetNextNumberOfFixturesByLeagueAsync(id,5), Throws.TypeOf<ArgumentException>());
        }

        [TestCase(-25, TestName = "Test with negative number")]
        [TestCase(0, TestName = "Test with 0")]
        public void GetNextNumberOfFixturesByLeague_NumberOfMatchesShouldNotBeLessThanOrEqualToZero(int number)
        {
            Assert.That(async () => await _handler.GetNextNumberOfFixturesByLeagueAsync(357,number), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetNextNumberOfFixturesByLeague_InvalidIdShouldReturnEmptyList()
        {
            var item = await _handler.GetNextNumberOfFixturesByLeagueAsync(int.MaxValue, 5);
            Assert.That(item.Count == 0);
        }

        #endregion
        #region GetLastNumberOfFixturesByLeague

        [TestCase(-25, TestName = "Test with negative number")]
        [TestCase(0, TestName = "Test with 0")]
        public void GetLastNumberOfFixturesByLeague_IdShouldNotBeLessThanOrEqualToZero(int id)
        {
            Assert.That(async () => await _handler.GetLastNumberOfFixturesByLeagueAsync(id,5), Throws.TypeOf<ArgumentException>());
        }

        [TestCase(-25, TestName = "Test with negative number")]
        [TestCase(0, TestName = "Test with 0")]
        public void GetLastNumberOfFixturesByLeague_NumberOfFixturesShouldNotBeLessThanOrEqualToZero(int number)
        {
            Assert.That(async () => await _handler.GetLastNumberOfFixturesByLeagueAsync(357, number), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetLastNumberOfFixturesByLeague_InvalidIdShouldReturnEmptyList()
        {
            var item = await _handler.GetLastNumberOfFixturesByLeagueAsync(int.MaxValue,5);
            Assert.That(item.Count == 0);
        }

        [Test]
        public async Task GetLastNumberOfFixturesByLeague_ShouldReturnPopulatedList()
        {
            var item = await _handler.GetLastNumberOfFixturesByLeagueAsync(357, 5);
            Assert.That(item.Count > 0);
        }
        
        #endregion
        #region GetAllFixturesByTeam

        [TestCase(-25, TestName = "Test with negative number")]
        [TestCase(0, TestName = "Test with 0")]
        public void GetAllFixturesByTeam_IdShouldNotBeLessThanOrEqualToZero(int id)
        {
            Assert.That(async () => await _handler.GetAllFixturesByTeamAsync(id), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetAllFixturesByTeam_InvalidIdShouldReturnEmptyList()
        {
            var item = await _handler.GetAllFixturesByTeamAsync(int.MaxValue);
            Assert.That(item.Count == 0);
        }

        [Test]
        public async Task GetAllFixturesByTeam_ShouldReturnPopulatedList()
        {
            var item = await _handler.GetAllFixturesByTeamAsync(15);
            Assert.That(item.Count > 0);
        }

        #endregion
        #region GetAllFixturesByTeamAndLeague

        [TestCase(-25, 357, TestName = "Test with negative number")]
        [TestCase(0, 357, TestName = "Test with 0")]
        [TestCase(15, -25, TestName = "Test with negative number")]
        [TestCase(15, 0, TestName = "Test with 0")]
        public void GetAllFixturesByTeamAndLeague_IdShouldNotBeLessThanOrEqualToZero(int id, int id2)
        {
            Assert.That(async () => await _handler.GetAllFixturesByTeamAndLeagueAsync(id, id2), Throws.TypeOf<ArgumentException>());
        }

        [TestCase(int.MaxValue, 357, TestName = "Invalid team_id")]
        [TestCase(15, int.MaxValue, TestName = "Invalid league_id")]
        public async Task GetAllFixturesByTeamAndLeague_InvalidIdShouldReturnEmptyList(int id, int id2)
        {
            var item = await _handler.GetAllFixturesByTeamAndLeagueAsync(id, id2);
            Assert.That(item.Count == 0);
        }

        [Test]
        public async Task GetAllFixturesByTeamAndLeague_ShouldReturnPopulatedList()
        {
            var item = await _handler.GetAllFixturesByTeamAndLeagueAsync(15,357);
            Assert.That(item.Count > 0);
        }

        #endregion
        #region GetNextNumberOfFixturesByTeam

        [TestCase(-25, 5, TestName = "Test with negative number")]
        [TestCase(0, 5, TestName = "Test with 0")]
        [TestCase(15, -25, TestName = "Test with negative number")]
        [TestCase(15, 0, TestName = "Test with 0")]
        public void GetNextNumberOfFixturesByTeam_IdShouldNotBeLessThanOrEqualToZero(int id, int number)
        {
            Assert.That(async () => await _handler.GetNextNumberOfFixturesByTeamAsync(id, number), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetNextNumberOfFixturesByTeam_InvalidIdShouldReturnEmptyList()
        {
            var item = await _handler.GetNextNumberOfFixturesByTeamAsync(int.MaxValue,5);
            Assert.That(item.Count == 0);
        }

        #endregion
        #region GetLastNumberOfFixturesByTeam

        [TestCase(-25, 5, TestName = "Test with negative number")]
        [TestCase(0, 5, TestName = "Test with 0")]
        [TestCase(15, -25, TestName = "Test with negative number")]
        [TestCase(15, 0, TestName = "Test with 0")]
        public void GetLastNumberOfFixturesByTeam_IdShouldNotBeLessThanOrEqualToZero(int id, int id2)
        {
            Assert.That(async () => await _handler.GetLastNumberOfFixturesByTeamAsync(id, id2), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetLastNumberOfFixturesByTeam_InvalidIdShouldReturnEmptyList()
        {
            var item = await _handler.GetLastNumberOfFixturesByTeamAsync(int.MaxValue, 5);
            Assert.That(item.Count == 0);
        }

        [Test]
        public async Task GetLastNumberOfFixturesByTeam_ShouldReturnPopulatedList()
        {
            var item = await _handler.GetLastNumberOfFixturesByTeamAsync(15, 5);
            Assert.That(item.Count > 0);
        }

        #endregion
    }
}