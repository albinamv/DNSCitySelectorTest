using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using DnsCitySelectorTests.PageObjects;
using OpenQA.Selenium.Firefox;
using DnsCitySelectorTests.Helpers;
using System.Drawing;

namespace DnsCitySelectorTests.Tests
{
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(FirefoxDriver))]
    //[TestFixture(typeof(OperaDriver))]
    [Parallelizable]
    public class CitySelectorTests <Browser> where Browser : IWebDriver, new() 
    {

        protected IWebDriver _webDriver;
        private CitySelectorPageObject _citySelectorModal;


        [OneTimeSetUp]
        protected void DoBeforeAllTests()
        {
            /*
            new DriverManager().SetUpDriver(new ChromeConfig());
            new DriverManager().SetUpDriver(new FirefoxConfig());
            new DriverManager().SetUpDriver(new OperaConfig());
            */

            //_webDriver = new OperaDriver();
            //_webDriver = new ChromeDriver();
        }

        [SetUp]
        public void DoBeforeEachTest()
        {
            _webDriver = new Browser();
            _webDriver.Manage().Cookies.DeleteAllCookies();
            _webDriver.Navigate().GoToUrl(TestSettings.HostPrefix);
            _webDriver.Manage().Window.Size = new Size(1920, 1080);

            _citySelectorModal = new MainMenuPageObject(_webDriver).OpenCitySelectorWindow();

        }

        [Test]
        public void ShouldPickCityFromButtonList()
        {
            string expectedCity = TestGenerateData.getRandomBigCity();

            var mainMenu = _citySelectorModal.PickCityFromBubbleButtonsList(expectedCity);
            string actualCity = mainMenu.GetCurrentCityName();

            Assert.AreEqual(expectedCity, actualCity);
        }



        [Test]
        public void ShouldFindCityBySearchWithFullCorrectName()
        {
            string expectedCity = TestGenerateData.getRandomBigCity();

            var mainMenu = _citySelectorModal.SearchCityByFullName(expectedCity);
            string actualCity = mainMenu.GetCurrentCityName();

            Assert.AreEqual(expectedCity, actualCity);
        }

        [Test]
        public void ShouldNotFindCityByNumbersValue()
        {
            _citySelectorModal.FillSearchInputWithValue(TestGenerateData.GenerateRandomNumber());

            Assert.That(_citySelectorModal.IsNotFoundResultVisible(), Is.True);
        }

        [Test]
        public void ShouldPickCityFromTerritoryList()
        {
            _citySelectorModal.ChooseRandomDistrict();
            _citySelectorModal.ChooseRandomRegion();
            string expectedCity = _citySelectorModal.ChooseRandomCity();

            WaitUntil.WaitSomeInterval(1000);
            string actualCity = new MainMenuPageObject(_webDriver).GetCurrentCityName();

            Assert.That(actualCity, Does.Contain(expectedCity));

        }


        [TearDown]
        public void DoAfterEachTest()
        {
            _webDriver.Quit();
        }

    }
}