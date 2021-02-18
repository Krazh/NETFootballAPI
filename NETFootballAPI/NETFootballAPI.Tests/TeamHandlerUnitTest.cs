using System;
using System.Threading.Tasks;
using NETFootballAPI;
using NUnit.Framework;

namespace UnitTest_NETFootballAPI
{
    public class TeamHandlerUnitTest
    {
        private readonly ITeamHandler _handler = new TeamHandler();
        [SetUp]
        public void Setup()
        {
            var url = "https://v3.football.api-sports.io/";
            var key = "5b0cecdeb8fb3c8ff17b33efa6923cbc";
            _handler.SetApiUrl(url);
            _handler.SetApiKey(key);
        }

        #region GetTeamById

        [TestCase(-25, TestName = "Negative number")]
        [TestCase(0, TestName = "Zero")]
        public void GetTeamById_IdShouldBeHigherThan0(int testId)
        {
            Assert.That(async () => await _handler.GetTeamByIdAsync(testId), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void GetTeamById_InvalidIdShouldReturnNullObject()
        {
            Assert.That(async () => await _handler.GetTeamByIdAsync(int.MaxValue), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetTeamById_ShouldReturnValidTeam()
        {
            // TeamId 15 is a Serie A Team in Brazil that is available on the demo api
            var item = await _handler.GetTeamByIdAsync(15);
            Assert.That(item != null && item.Id == "15");
        }

        #endregion
        #region GetTeamsByLeagueId

        [TestCase(-25, TestName = "Negative number")]
        [TestCase(0, TestName = "Zero")]
        public void GetTeamsByLeagueId_IdShouldBeHigherThan0(int testId)
        {
            Assert.That(async () => await _handler.GetTeamsByLeagueIdAsync(testId), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetTeamsByLeagueId_InvalidIdShouldReturnEmptyList()
        {
            var item = await _handler.GetTeamsByLeagueIdAsync(int.MaxValue);
            Assert.That(item.Count == 0);
        }

        [Test]
        public async Task GetTeamsByLeagueId_ShouldReturnValidLeague()
        {
            // LeagueID 357 is Serie in Brazil that is available on the demo api
            var item = await _handler.GetTeamsByLeagueIdAsync(357);
            Assert.That(item != null && item[0].Country == "Brazil");
        }

        #endregion
        #region GetTeamByStringSearch

        [Test]
        public void GetTeamByStringSearch_StringParameterMustNotBeNullOrWhiteSpace()
        {
            Assert.That(async () => await _handler.GetTeamByStringSearchAsync(""), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void GetTeamByStringSearch_StringShouldBeAtLeastThreeCharacters()
        {
            Assert.That(async () => await _handler.GetTeamByStringSearchAsync("ab"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetTeamByStringSearch_ShouldReturnValidTeam()
        {
            // Test API has the team Grêmio available from Brazil.
            // This is also a special case because you cannot search for accented characters like ê.
            var teamName = "Gremio";
            var item = await _handler.GetTeamByStringSearchAsync(teamName);
            Assert.That(item.Name == teamName);
        }

        [Test]
        public void GetTeamByStringSearch_StringShouldNotContainSymbols()
        {
            Assert.That(async () => await _handler.GetTeamByStringSearchAsync("Braz!l"), Throws.TypeOf<ArgumentException>());
        }

        #endregion
    }
}