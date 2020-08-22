using System;
using System.Threading.Tasks;
using NETFootballAPI;
using NUnit.Framework;

namespace UnitTest_NETFootballAPI
{
    public class RoundsHandlerUnitTest
    {
        private readonly IRoundsHandler _handler = new RoundsHandler();
        [SetUp]
        public void Setup()
        {
            var key = "thisIsATestString";
            var url = "https://www.api-football.com/demo/v2/";
            _handler.SetApiKey(key);
            _handler.SetApiUrl(url);
        }
        
        [TestCase(-25, TestName = "Negative number")]
        [TestCase(0, TestName = "Zero")]
        public void GetRoundsAvailableByLeagueId_IdShouldBeHigherThan0(int testId)
        {
            Assert.That(async () => await _handler.GetRoundsAvailableByLeagueIdAsync(testId), 
                Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetRoundsAvailableByLeagueId_InvalidIdShouldReturnEmptyList()
        {
            var item = await _handler.GetRoundsAvailableByLeagueIdAsync(int.MaxValue);
            Assert.That(item.Count == 0);
        }

        [Test]
        public async Task GetRoundsAvailableByLeagueId_ShouldReturnValidRounds()
        {
            // TeamId 15 is a Serie A Team in Brazil that is available on the demo api
            var item = await _handler.GetRoundsAvailableByLeagueIdAsync(357);
            Assert.That(item.Count > 0);
        }
        
        [TestCase(-25, TestName = "Negative number")]
        [TestCase(0, TestName = "Zero")]
        public void GetCurrentRoundsAvailableByLeagueId_IdShouldBeHigherThan0(int testId)
        {
            Assert.That(async () => await _handler.GetCurrentRoundsAvailableByLeagueIdAsync(testId), 
                Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetCurrentRoundsAvailableByLeagueId_InvalidIdShouldReturnEmptyString()
        {
            var item = await _handler.GetCurrentRoundsAvailableByLeagueIdAsync(int.MaxValue);
            Assert.That(string.IsNullOrWhiteSpace(item));
        }

        [Test]
        public async Task GetCurrentRoundsAvailableByLeagueId_ShouldReturnValidRound()
        {
            // TeamId 15 is a Serie A Team in Brazil that is available on the demo api
            var item = await _handler.GetCurrentRoundsAvailableByLeagueIdAsync(357);
            Assert.That(!string.IsNullOrWhiteSpace(item));
        }
    }
}