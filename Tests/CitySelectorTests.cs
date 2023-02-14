using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using DnsCitySelectorTests.PageObjects;
using OpenQA.Selenium.Firefox;
using DnsCitySelectorTests.Helpers;
using System.Drawing;

namespace DnsCitySelectorTests.Tests
{
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(FirefoxDriver))]
    //[TestFixture(typeof(OperaDriver))] -> �������� ������������ Selenium, Opera driver ������ �� ��������������
    [Parallelizable]
    public class CitySelectorTests <Browser> where Browser : IWebDriver, new() 
    {

        protected IWebDriver _webDriver;
        private CitySelectorPageObject _citySelectorModal;


        [SetUp]
        public void DoBeforeEachTest()
        {
            _webDriver = new Browser();
            _webDriver.Manage().Cookies.DeleteAllCookies();
            _webDriver.Navigate().GoToUrl(TestSettings.HostPrefix);
            _webDriver.Manage().Window.Size = new Size(1920, 1080);

            _citySelectorModal = new MainMenuPageObject(_webDriver).OpenCitySelectorWindow();

        }

        [Test, Description("����� ������ �� ������ ������ ��� ����� ��� ������")]
        [Category("Bubble buttons")]
        public void ShouldPickCityFromButtonList()
        {
            string expectedCity = TestGenerateData.getRandomBigCity();

            var mainMenu = _citySelectorModal.PickCityFromBubbleButtonsList(expectedCity);
            string actualCity = mainMenu.GetCurrentCityName();

            Assert.AreEqual(expectedCity, actualCity);
        }



        [Test, Description("����� ������ �� ����������� ��������")]
        [Category("Search")]
        public void ShouldFindCityBySearchWithFullCorrectName()
        {
            string expectedCity = TestGenerateData.getRandomBigCity();

            var mainMenu = _citySelectorModal.SearchCityByFullName(expectedCity);
            string actualCity = mainMenu.GetCurrentCityName();

            Assert.AreEqual(expectedCity, actualCity);
        }

        [Test, Description("����� ������ �� ����������� �������� � �������� �� � ����� ������")]
        [Category("Search")]
        public void ShouldFindCityBySearchWithCorrectNameAndSpaces()
        {
            string query = TestGenerateData.GenerateBigCityWithSpacesBeforeAndAfter();
            string expectedCity = query.Trim();

            var mainMenu = _citySelectorModal.SearchCityByFullName(query);
            string actualCity = mainMenu.GetCurrentCityName();

            Assert.AreEqual(expectedCity, actualCity);
        }

        [Test, Description("���� ���� � ���� ��� ������ ������")]
        [Category("Search")]
        public void ShouldNotFindCityByNumbersValue()
        {
            _citySelectorModal.FillSearchInputWithValue(TestGenerateData.GenerateRandomNumber());

            Assert.That(_citySelectorModal.IsNotFoundResultVisible(), Is.True);
        }

        [Test, Description("���� ����. �������� � ���� ��� ������ ������")]
        [Category("Search")]
        public void ShouldNotFindCityBySpecSymbols()
        {
            _citySelectorModal.FillSearchInputWithValue(TestGenerateData.GetSpecialCharacters());

            Assert.That(_citySelectorModal.IsNotFoundResultVisible(), Is.True);
        }

        [Test, Description("���� ������ �������� � ���� ��� ������ ������")]
        [Category("Search")]
        public void ShouldNotFindCityBySpacesOnly()
        {
            _citySelectorModal.FillSearchInputWithValue("        ");

            Assert.That(_citySelectorModal.IsNotFoundResultVisible(), Is.True);
        }

        [Test, Description("����� ������ ����� ������ ������� � ��������")]
        [Category("List of territories")]
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