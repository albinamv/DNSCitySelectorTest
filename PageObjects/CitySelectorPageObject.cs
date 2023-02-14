using DnsCitySelectorTests.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace DnsCitySelectorTests.PageObjects
{
    public class CitySelectorPageObject
    {
        private IWebDriver _webDriver;
        private Random random = new();

        private readonly By _loader = By.ClassName("loader-ufo");

        private readonly By _searchInput = By.CssSelector("[class^=search-field] input");
        private readonly By _noSearchResults = By.CssSelector("[class^='city-not-found']");
        private readonly string _exactSearchResultSelector = "//ul[@class='cities-search']/descendant::span[text()='{0}']";
        private readonly By _searchResults = By.CssSelector(".cities-search");

        private readonly By _citiesButtons = By.CssSelector("[class^='city-bubble']");
        
        private readonly By _districtsList = By.CssSelector("ul[class^=districts]");
        private readonly By _regionsList = By.CssSelector("ul[class^=regions]");
        private readonly By _citiesList = By.CssSelector("ul[class^=cities]");
        private readonly By _listElement = By.TagName("li");
        

        public CitySelectorPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            WaitUntil.WaitElementAbsence(_webDriver, _loader);
        }

        public void FillSearchInputWithValue(string value)
        {
            _webDriver.FindElement(_searchInput).SendKeys(value);
        }

        public int ElementsCount(By locator)
        {
            return _webDriver.FindElement(locator).FindElements(_listElement).Count;
        }

        public MainMenuPageObject SearchCityByFullName(string cityName)
        {
            FillSearchInputWithValue(cityName.ToLower());

            if (ElementsCount(_searchResults) == 1)
                _webDriver.FindElement(_searchInput).SendKeys(Keys.Enter);
            else
                _webDriver.FindElement(By.XPath(String.Format(_exactSearchResultSelector, cityName))).Click();

            WaitUntil.WaitSomeInterval(1000);
            return (new MainMenuPageObject(_webDriver));
        }

        public bool IsNotFoundResultVisible()
        {
            return _webDriver.FindElement(_noSearchResults).Displayed;
        }

        public int CityIndexInBubbleButtons(string cityName)
        {
            var citiesNames = _webDriver.FindElements(_citiesButtons).Select(x => x.Text).ToList();
            if (citiesNames.Contains(cityName))
                return citiesNames.IndexOf(cityName);
            else
                return -1;
        }

        public MainMenuPageObject PickCityFromBubbleButtonsList(string cityName)
        {
            WaitUntil.WaitSomeInterval(500);
            int cityIndex = CityIndexInBubbleButtons(cityName);

            _webDriver.FindElements(_citiesButtons).ElementAt(cityIndex).Click();

            WaitUntil.WaitSomeInterval(1000);
            return new MainMenuPageObject(_webDriver);
        }

        public void ChooseRandomDistrict() 
        {
            WaitUntil.WaitSomeInterval(500);
            int districtsCount = ElementsCount(_districtsList);
            int districtIndex = random.Next(districtsCount);
            _webDriver.FindElement(_districtsList).FindElements(_listElement).ElementAt(districtIndex).Click();
        }

        public void ChooseRandomRegion()
        {
            WaitUntil.WaitSomeInterval(500);
            int regionsCount = ElementsCount(_regionsList);
            int regionIndex = random.Next(regionsCount);
            var region = _webDriver.FindElement(_regionsList).FindElements(_listElement).ElementAt(regionIndex);

            ScrollTo(region, _webDriver);

            region.Click();
        }

        public string ChooseRandomCity()
        {
            WaitUntil.WaitSomeInterval(500);
            int citiesCount = ElementsCount(_citiesList);
            int cityIndex = random.Next(citiesCount);
            var city = _webDriver.FindElement(_citiesList).FindElements(_listElement).ElementAt(cityIndex);
            string expectedCity = city.Text;

            ScrollTo(city, _webDriver);

            city.Click();
            return expectedCity;
        }

        public static void ScrollTo(IWebElement element, IWebDriver driver)
        {
            Actions actions = new(driver);
            actions.MoveToElement(element);
            actions.Perform();
        }
    }
}
