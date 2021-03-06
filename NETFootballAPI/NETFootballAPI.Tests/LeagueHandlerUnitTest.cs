using System;
using System.Threading.Tasks;
using NETFootballAPI;
using NUnit.Framework;

namespace UnitTest_NETFootballAPI
{
    public class LeagueHandlerUnitTest
    {
        private readonly ILeagueHandler _handler = new LeagueHandler();
        [SetUp]
        public void Setup()
        {
            var url = "https://www.api-football.com/demo/v2/";
            _handler.SetApiUrl(url);
        }

        #region GetAllLeagues

        [Test]
        public async Task GetAllLeagues_ReturnsSomething()
        {
            var items = await _handler.GetAllLeaguesAsync();
            
            Assert.That(items.Count > 0);
        }

        #endregion
        #region GetLeaguesById

        [TestCase(-25, TestName = "Negative number")]
        [TestCase(0, TestName = "Zero")]
        public void GetLeagueById_IdShouldBeHigherThan0(int testId)
        {
            Assert.That(async () => await _handler.GetLeagueByIdAsync(testId), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetLeagueById_InvalidIdShouldReturnNullObject()
        {
            var item = await _handler.GetLeagueByIdAsync(int.MaxValue);
            Assert.That(item == null);
        }

        [Test]
        public async Task GetLeagueById_ShouldReturnValidLeague()
        {
            // League Id 357 is Serie A in Brazil that is available on the demo api
            var item = await _handler.GetLeagueByIdAsync(357);
            Assert.That(item != null && item.Id == "357");
        }

        #endregion
        #region GetLeaguesByTeamId

        [TestCase(-25, TestName = "Negative number")]
        [TestCase(0, TestName = "Zero")]
        public void GetLeaguesByTeamId_IdShouldBeHigherThan0(int testId)
        {
            Assert.That(async () => await _handler.GetLeaguesByTeamIdAsync(testId), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetLeaguesByTeamId_InvalidIdShouldReturnNullObject()
        {
            var item = await _handler.GetLeaguesByTeamIdAsync(int.MaxValue);
            Assert.That(item == null);
        }

        [Test]
        public async Task GetLeaguesByTeamId_ShouldReturnValidListOfLeagues()
        {
            // Assert. Test api has 4 leagues available, LeagueId 357 is Brazil and TeamId 15 is a team from Brazil.
            var item = await _handler.GetLeaguesByTeamIdAsync(15);
            Assert.That(item.Count > 0);
        }

        #endregion
        #region GetLeaguesByTeamIdAndSeason
        [TestCase(-25, 2018, TestName = "Negative number")]
        [TestCase(0, 2018, TestName = "Zero")]
        public void GetLeaguesByTeamIdAndSeason_IdShouldBeHigherThan0(int testId, int season)
        {
            Assert.That(async () => await _handler.GetLeaguesByTeamIdAndSeasonAsync(testId, season), Throws.TypeOf<ArgumentException>());
        }

        [TestCase(15, 10000, TestName = "TeamId = 15 in year 10000")]
        [TestCase(15, 1900, TestName = "TeamId = 15 in year 1900")]
        public void GetLeaguesByTeamIdAndSeason_SeasonShouldBeValidYear(int testId, int year)
        {
            Assert.That(async () => await _handler.GetLeaguesByTeamIdAndSeasonAsync(testId, year), Throws.TypeOf<ArgumentOutOfRangeException>());
        }
        
        [Test]
        public async Task GetLeaguesByTeamIdAndSeason_InvalidIdShouldReturnNullObject()
        {
            var item = await _handler.GetLeaguesByTeamIdAndSeasonAsync(int.MaxValue, 2018);
            Assert.That(item == null);
        }
        
        [Test]
        public async Task GetLeaguesByTeamIdAndSeason_ShouldReturnValidListOfLeagues()
        {
            // Assert. Test api has 4 leagues available, LeagueId 357 is Brazil and TeamId 15 is a team from Brazil. 
            // Due to limitations of the demo url only the season 2019 has data for the league. 
            var item = await _handler.GetLeaguesByTeamIdAndSeasonAsync(15,2019);
            Assert.That(item.Count > 0);
        }
        #endregion
        #region GetLeagueByStringSearch

        [Test]
        public void GetLeagueByStringSearch_StringParameterMustNotBeNullOrWhiteSpace()
        {
            Assert.That(async () => await _handler.GetLeagueByStringSearchAsync(""), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void GetLeagueByStringSearch_StringShouldBeAtLeastThreeCharacters()
        {
            Assert.That(async () => await _handler.GetLeagueByStringSearchAsync("ab"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetLeagueByStringSearch_ShouldReturnValidLeague()
        {
            // Test API has the league Serie A available from Brazil
            var validLeagueName = "Serie A";
            var item = await _handler.GetLeagueByStringSearchAsync(validLeagueName);
            Assert.That(item.Name == validLeagueName);
        }

        [Test]
        public void GetLeagueByStringSearch_StringShouldNotContainSymbols()
        {
            Assert.That(async () => await _handler.GetLeagueByStringSearchAsync("Braz!l"), Throws.TypeOf<ArgumentException>());
        }

        #endregion
        #region GetLeaguesByCountry

        [Test]
        public void GetLeaguesByCountry_StringShouldNotBeNullOrWhitespace()
        {
            Assert.That(async () => await _handler.GetLeaguesByCountryAsync(""), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void GetLeaguesByCountry_StringShouldNotContainSymbols()
        {
            Assert.That(async () => await _handler.GetLeaguesByCountryAsync("Braz!l"),
                Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetLeaguesByCountry_ShouldReturnListOfLeagues()
        {
            var country = "Brazil";
            var item = await _handler.GetLeaguesByCountryAsync(country);
            Assert.That(item.Count > 0 && item[0].Country == country);
        }

        #endregion
        #region GetLeaguesByCountryAndSeason

        [Test]
        public void GetLeaguesByCountryAndSeason_StringShouldNotBeNullOrWhitespace()
        {
            Assert.That(async () => await _handler.GetLeaguesByCountryAndSeasonAsync("",2019), Throws.TypeOf<ArgumentException>());
        }
        
        [Test]
        public void GetLeaguesByCountryAndSeason_StringShouldNotContainSymbols()
        {
            Assert.That(async () => await _handler.GetLeaguesByCountryAndSeasonAsync("Braz!l",2019),
                Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetLeaguesByCountryAndSeason_ShouldReturnListOfLeagues()
        {
            var country = "Brazil";
            var item = await _handler.GetLeaguesByCountryAndSeasonAsync(country, 2019);
            Assert.That(item.Count > 0 && item[0].Country == country);
        }


        [TestCase(1900, TestName = "The year is 1900")]
        [TestCase(10000, TestName = "The year is 10000")]
        public void GetLeaguesByCountryAndSeason_SeasonShouldBeValidYear ( int year)
        {
            Assert.That(async () => await _handler.GetLeaguesByCountryAndSeasonAsync("Brazil", year), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        #endregion
        #region GetLeaguesByCountryCode
        [Test]
        public void GetLeaguesByCountryCode_StringShouldNotBeNullOrWhiteSpace()
        {
            Assert.That(async () => await _handler.GetLeaguesByCountryCodeAsync(""), Throws.TypeOf<ArgumentException>());
        }

        [TestCase("a", TestName = "1 character length")]
        [TestCase("abc", TestName = "3 character length")]
        public void GetLeaguesByCountryCode_StringShouldBeExactlyTwoCharacters(string code)
        {
            Assert.That(async () => await _handler.GetLeaguesByCountryCodeAsync(code), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetLeaguesByCountryCode_ShouldReturnValidListOfLeagues()
        {
            // The demo allows access to the Brazilian League which has the country code BR
            var items = await _handler.GetLeaguesByCountryCodeAsync("BR");
            Assert.That(items.Count > 0 && items[0].CountryCode == "BR");
        }
        #endregion
        #region GetLeaguesByCountryCodeAndSeason

        [Test]
        public void GetLeaguesByCountryCodeAndSeason_StringShouldNotBeNullOrWhiteSpace()
        {
            Assert.That(async () => await _handler.GetLeaguesByCountryCodeAndSeasonAsync("",2019), Throws.TypeOf<ArgumentException>());
        }

        [TestCase("a", TestName = "1 character length")]
        [TestCase("abc", TestName = "3 character length")]
        public void GetLeaguesByCountryCodeAndSeason_StringShouldBeExactlyTwoCharacters(string code)
        {
            Assert.That(async () => await _handler.GetLeaguesByCountryCodeAndSeasonAsync(code,2019), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetLeaguesByCountryCodeAndSeason_ShouldReturnValidListOfLeagues()
        {
            // The demo allows access to the Brazilian League which has the country code BR
            var items = await _handler.GetLeaguesByCountryCodeAndSeasonAsync("BR",2019);
            Assert.That(items.Count > 0 && items[0].CountryCode == "BR");
        }
        
        [Test]
        public void GetLeaguesByCountryCodeAndSeason_StringShouldNotContainSymbols()
        {
            Assert.That(async () => await _handler.GetLeaguesByCountryCodeAndSeasonAsync("!l",2019),
                Throws.TypeOf<ArgumentException>());
        }
        
        [TestCase(1900, TestName = "The year is 1900")]
        [TestCase(10000, TestName = "The year is 10000")]
        public void GetLeaguesByCountryCodeAndSeason_SeasonShouldBeValidYear ( int year)
        {
            Assert.That(async () => await _handler.GetLeaguesByCountryCodeAndSeasonAsync("BR", year), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        #endregion
        #region GetLeaguesBySeason

        [TestCase(1900, TestName = "The year is 1900")]
        [TestCase(10000, TestName = "The year is 10000")]
        public void GetLeaguesBySeason_SeasonShouldBeValidYear(int year)
        {
            Assert.That(async () => await _handler.GetLeaguesBySeasonAsync(year), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public async Task GetLeaguesBySeason_ShouldReturnList()
        {
            var items = await _handler.GetLeaguesBySeasonAsync(2019);
            Assert.That(items.Count > 0 && items[0].Season == 2019);
        }

        #endregion
        #region GetSeasonsAvailableForLeagueByLeagueId

        [TestCase(-25, TestName = "Negative number")]
        [TestCase(0, TestName = "Zero")]
        public void GetSeasonsAvailableForLeague_IdShouldBeHigherThan0(int testId)
        {
            Assert.That(async () => await _handler.GetSeasonsAvailableForLeagueAsync(testId), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetSeasonsAvailableForLeague_InvalidIdShouldReturnEmptyList()
        {
            var item = await _handler.GetSeasonsAvailableForLeagueAsync(int.MaxValue);
            Assert.That(item.Count == 0);
        }

        [Test]
        public async Task GetSeasonsAvailableForLeague_ShouldReturnValidSeason()
        {
            var item = await _handler.GetSeasonsAvailableForLeagueAsync(357);
            Assert.That(item != null && item[0].Id == "357");
        }

        #endregion
        #region GetSeasonsAvailableForLeagueByLeagueIdAndSeason

        [TestCase(-25, TestName = "Negative number")]
        [TestCase(0, TestName = "Zero")]
        public void GetSeasonsAvailableForLeagueByLeagueIdAndSeason_IdShouldBeHigherThan0(int testId)
        {
            Assert.That(async () => await _handler.GetSeasonsAvailableForLeagueAsync(testId, 2019), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetSeasonsAvailableForLeagueByLeagueIdAndSeason_InvalidIdShouldReturnEmptyList()
        {
            var item = await _handler.GetSeasonsAvailableForLeagueAsync(int.MaxValue,2019);
            Assert.That(item.Count == 0);
        }

        [Test]
        public async Task GetSeasonsAvailableForLeagueByLeagueIdAndSeason_ShouldReturnValidLeague()
        {
            // League Id 357 is Serie A in Brazil that is available on the demo api
            var item = await _handler.GetSeasonsAvailableForLeagueAsync(357,2019);
            Assert.That(item != null && item[0].Id == "357");
        }
        
        [TestCase(1900, TestName = "The year is 1900")]
        [TestCase(10000, TestName = "The year is 10000")]
        public void GetSeasonsAvailableForLeagueByLeagueIdAndSeason_SeasonShouldBeValidYear ( int year)
        {
            Assert.That(async () => await _handler.GetSeasonsAvailableForLeagueAsync(357, year), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        #endregion
        #region GetCurrentLeagues

        [Test]
        public async Task GetCurrentLeagues_ReturnsSomething()
        {
            var items = await _handler.GetCurrentLeaguesAsync();
            
            Assert.That(items.Count > 0);
        }

        #endregion
        #region GetCurrentLeaguesByCountry

        [Test]
        public void GetCurrentLeaguesByCountry_StringShouldNotBeNullOrWhitespace()
        {
            Assert.That(async () => await _handler.GetCurrentLeaguesByCountryAsync(""), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void GetCurrentLeaguesByCountry_StringShouldNotContainSymbols()
        {
            Assert.That(async () => await _handler.GetCurrentLeaguesByCountryAsync("Braz!l"),
                Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetCurrentLeaguesByCountry_ShouldReturnListOfLeagues()
        {
            var country = "Brazil";
            var item = await _handler.GetCurrentLeaguesByCountryAsync(country);
            Assert.That(item.Count > 0 && item[0].Country == country);
        }

        #endregion
        #region GetLeaguesByType

        [Test]
        public void GetLeaguesByType_StringShouldNotBeNullOrWhitespace()
        {
            Assert.That(async () => await _handler.GetLeaguesByTypeAsync(""), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void GetLeaguesByType_StringShouldNotContainSymbols()
        {
            Assert.That(async () => await _handler.GetLeaguesByTypeAsync("Braz!l"),
                Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetLeaguesByType_ShouldReturnListOfLeagues()
        {
            var type = "League";
            var item = await _handler.GetLeaguesByTypeAsync(type);
            Assert.That(item.Count > 0 && item[0].Type == type);
        }

        #endregion
        #region GetLeaguesByTypeAndCountry

        [TestCase("", "Brazil", TestName = "1st param empty, 2nd correct")]
        [TestCase("League", "", TestName = "1st param correct, 2nd empty")]
        public void GetLeaguesByTypeAndCountry_StringShouldNotBeNullOrWhitespace(string type, string country)
        {
            Assert.That(async () => await _handler.GetLeaguesByTypeAndCountryAsync(type, country), Throws.TypeOf<ArgumentException>());
        }

        [TestCase("Leag!es", "Brazil", TestName = "1st param empty, 2nd correct")]
        [TestCase("Leagues", "Braz!l", TestName = "1st param correct, 2nd empty")]
        public void GetLeaguesByTypeAndCountry_StringShouldNotContainSymbols(string type, string country)
        {
            Assert.That(async () => await _handler.GetLeaguesByTypeAndCountryAsync(type, country),
                Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetLeaguesByTypeAndCountry_ShouldReturnListOfLeagues()
        {
            var type = "League";
            var country = "Brazil";
            var item = await _handler.GetLeaguesByTypeAndCountryAsync(type, country);
            Assert.That(item.Count > 0 && item[0].Type == type && item[0].Country == country);
        }

        #endregion
        #region GetLeaguesByTypeAndCountryAndSeason

        [TestCase("", "Brazil", TestName = "1st param empty, 2nd correct")]
        [TestCase("League", "", TestName = "1st param correct, 2nd empty")]
        public void GetLeaguesByTypeAndCountryAndSeason_StringShouldNotBeNullOrWhitespace(string type, string country)
        {
            Assert.That(async () => await _handler.GetLeaguesByTypeAndCountryAndSeasonAsync(type, country,2019), Throws.TypeOf<ArgumentException>());
        }

        [TestCase("Leag!es", "Brazil", TestName = "1st param empty, 2nd correct")]
        [TestCase("Leagues", "Braz!l", TestName = "1st param correct, 2nd empty")]
        public void GetLeaguesByTypeAndCountryAndSeason_StringShouldNotContainSymbols(string type, string country)
        {
            Assert.That(async () => await _handler.GetLeaguesByTypeAndCountryAndSeasonAsync(type, country,2019),
                Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task GetLeaguesByTypeAndCountryAndSeason_ShouldReturnListOfLeagues()
        {
            var type = "League";
            var country = "Brazil";
            var season = 2019;
            var item = await _handler.GetLeaguesByTypeAndCountryAndSeasonAsync(type, country, season);
            Assert.That(item.Count > 0 && item[0].Type == type && item[0].Country == country && item[0].Season == season);
        }
        
        [TestCase(1900, TestName = "The year is 1900")]
        [TestCase(10000, TestName = "The year is 10000")]
        public void GetLeaguesByTypeAndCountryAndSeason_SeasonShouldBeValidYear ( int year)
        {
            Assert.That(async () => await _handler.GetLeaguesByTypeAndCountryAndSeasonAsync("League", "Brazil", year), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        #endregion
        #region GetLeaguesByTypeAndSeason

        [Test]
        public void GetLeaguesByTypeAndSeason_StringShouldNotBeNullOrWhitespace()
        {
            Assert.That(async () => await _handler.GetLeaguesByTypeAndSeasonAsync("",2019), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void GetLeaguesByTypeAndSeason_StringShouldNotContainSymbols()
        {
            Assert.That(async () => await _handler.GetLeaguesByTypeAndSeasonAsync("Braz!l",2019),
                Throws.TypeOf<ArgumentException>());
        }
        
        [TestCase(1900, TestName = "The year is 1900")]
        [TestCase(10000, TestName = "The year is 10000")]
        public void GetLeaguesByTypeAndSeason_SeasonShouldBeValidYear ( int year)
        {
            Assert.That(async () => await _handler.GetLeaguesByTypeAndSeasonAsync("League", year), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public async Task GetLeaguesByTypeAndSeason_ShouldReturnListOfLeagues()
        {
            var type = "League";
            var season = 2019;
            var item = await _handler.GetLeaguesByTypeAndSeasonAsync(type,season);
            Assert.That(item.Count > 0 && item[0].Type == type && item[0].Season == season);
        }

        #endregion
    }
}