using System;
using System.Security;
using NETFootballAPI;
using NUnit.Framework;

namespace UnitTest_NETFootballAPI
{
    public class ApiHandlerUnitTest
    {
        private ApiHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _handler = new ApiHandler();
        }

        [Test]
        public void SetApiKey_IsNotNullOrWhiteSpace()
        {
            Assert.That(() => _handler.SetApiKey(""), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void SetApiUrl_IsNotNullOrWhiteSpace()
        {
            Assert.That(() => _handler.SetApiUrl(""), Throws.TypeOf<ArgumentNullException>());
        }

        [TestCase("..", TestName = "Just dots")]
        [TestCase("www.google", TestName = "Missing top level domain")]
        [TestCase("Google", TestName = "Name of site, not address")]
        public void SetApiUrl_IsValidUrl(string url)
        {
            Assert.That( () => _handler.SetApiUrl(url), Throws.TypeOf<UriFormatException>());
        }
    }
}