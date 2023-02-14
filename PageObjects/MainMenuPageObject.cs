using DnsCitySelectorTests.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnsCitySelectorTests.PageObjects
{
    public class MainMenuPageObject
    {
        private IWebDriver _webDriver;
        private readonly By _citySelectorButton = By.CssSelector("[class^=city-select__text]");

        public MainMenuPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public CitySelectorPageObject OpenCitySelectorWindow()
        {
            WaitForCitySelectorButton();
            _webDriver.FindElement(_citySelectorButton).Click();
            return new CitySelectorPageObject(_webDriver);
        }

        public string GetCurrentCityName()
        {
            WaitForCitySelectorButton();
            return _webDriver.FindElement(_citySelectorButton).Text;
        }

        public void WaitForCitySelectorButton()
        {
            WaitUntil.WaitElement(_webDriver, _citySelectorButton);
        }
    }
}
