﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DnsCitySelectorTests.Helpers
{
    public static class WaitUntil
    {

        public static void WaitSomeInterval(int msec)
        {
            Task.Delay(TimeSpan.FromMilliseconds(msec)).Wait();
        }

        public static void WaitElement(IWebDriver driver, By locator, int seconds = 5)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.ElementIsVisible(locator));
            new WebDriverWait(driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        public static void WaitElementAbsence(IWebDriver driver, By locator, int seconds = 5)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
        }
    }
}
