using System;
using System.Threading.Tasks;
using NETFootballAPI;
using NUnit.Framework;

namespace UnitTest_NETFootballAPI
{
    public class StatisticsUnitTest
    {
        private readonly IStatisticsHandler _handler = new StatisticsHandler();
        [SetUp]
        public void Setup()
        {
            var key = "thisIsATestString";
            var url = "https://www.api-football.com/demo/v2/";
            _handler.SetApiKey(key);
            _handler.SetApiUrl(url);
        }

        #region GetStatisticsByTeamIdAndLeagueId

        [TestCase(-25, 357, TestName = "Negative number")]
        [TestCase(0, 357, TestName = "Zero")]
        [TestCase(15, -25, TestName = "Negative number")]
        [TestCase(15, 0, TestName = "Zero")]
        public void GetStatisticsByTeamIdAndLeagueId_IdShouldBeHigherThan0(int teamId, int leagueId)
        {
            Assert.That(async () => await _handler.GetStatisticsByTeamIdAndLeagueIdAsync(teamId, leagueId), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetStatisticsByTeamIdAndLeagueId_ShouldReturnValidStatistics()
        {
            // LeagueID 357 is Serie A and teamId 15 is a team in that leaguie in Brazil that is available on the demo api
            var item = await _handler.GetStatisticsByTeamIdAndLeagueIdAsync(15,357);
            Assert.That(item != null);
        }

        #endregion

        #region GetStatisticsByTeamIdAndLeagueIdAndEndDate

        [TestCase(-25, 357, TestName = "Negative number")]
        [TestCase(0, 357, TestName = "Zero")]
        [TestCase(15, -25, TestName = "Negative number")]
        [TestCase(15, 0, TestName = "Zero")]
        public void GetStatisticsByTeamIdAndLeagueIdAndEndDate_IdShouldBeHigherThan0(int teamId, int leagueId)
        {
            Assert.That(async () => await _handler.GetStatisticsByTeamIdAndLeagueIdAndEndDateAsync(teamId, leagueId, DateTime.Now), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void GetStatisticsByTeamIdAndLeagueIdAndEndDate_YearShouldBeValidRange()
        {
            var date = DateTime.Now;
            date = date.AddYears(50);
            Assert.That(async () => await _handler.GetStatisticsByTeamIdAndLeagueIdAndEndDateAsync(15,357, date), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public async Task GetStatisticsByTeamIdAndLeagueIdAndEndDate_ShouldReturnValidStatistics()
        {
            // LeagueID 357 is Serie A and teamId 15 is a team in that leaguie in Brazil that is available on the demo api
            var item = await _handler.GetStatisticsByTeamIdAndLeagueIdAndEndDateAsync(15,357,DateTime.Now);
            Assert.That(item != null);
        }

        #endregion
    }
}