using System;
using System.Security;
using Microsoft.VisualBasic;
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

        [Test]
        public void GetListFromEndPoint_StringMustNotBeNullOrWhiteSpaceTest1()
        {
            Assert.That(async () => await _handler.GetListFromEndpoint<League>("https://www.api-football.com/demo/v2/", ""), Throws.TypeOf<ArgumentNullException>());
        }
        
        [Test]
        public void GetListFromEndPoint_StringMustNotBeNullOrWhiteSpaceTest2()
        {
            Assert.That(async () => await _handler.GetListFromEndpoint<League>("", "leagues"), Throws.TypeOf<ArgumentNullException>());
        }
        
        [TestCase("..", TestName = "Just dots")]
        [TestCase("www.google", TestName = "Missing top level domain")]
        [TestCase("Google", TestName = "Name of site, not address")]
        public void GetListFromEndPoint_IsValidUrl(string url)
        {
            Assert.That( () => _handler.GetListFromEndpoint<League>(url, "leagues"), Throws.TypeOf<UriFormatException>());
        }
        
        [Test]
        public void GetItemFromEndpoint_StringMustNotBeNullOrWhiteSpaceTest1()
        {
            Assert.That(async () => await _handler.GetItemFromEndpoint<League>("https://www.api-football.com/demo/v2/", ""), Throws.TypeOf<ArgumentNullException>());
        }
        
        [Test]
        public void GetItemFromEndpoint_StringMustNotBeNullOrWhiteSpaceTest2()
        {
            Assert.That(async () => await _handler.GetItemFromEndpoint<League>("", "leagues"), Throws.TypeOf<ArgumentNullException>());
        }
        
        [TestCase("..", TestName = "Just dots")]
        [TestCase("www.google", TestName = "Missing top level domain")]
        [TestCase("Google", TestName = "Name of site, not address")]
        public void GetItemFromEndpoint_IsValidUrl(string url)
        {
            Assert.That( () => _handler.GetItemFromEndpoint<League>(url, "leagues"), Throws.TypeOf<UriFormatException>());
        }
    }
}