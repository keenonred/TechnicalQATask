using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.IO;
using System.Text;

namespace TechnicalQATask
{
    public class WebDriverFactory
    {
        public IWebDriver _driver;

        protected WebDriverFactory(BrowserType type)
        {
            _driver = WebDriver(type);
        }

        public enum BrowserType
        {
            Firefox,
            Chrome
        }

        public static IWebDriver WebDriver(BrowserType type)
        {
            IWebDriver driver = null;

            switch (type)
            {
                case BrowserType.Chrome:
                    driver = ChromeDriver();
                    break;

                case BrowserType.Firefox:
                    driver = FirefoxDriver();
                    break;
            }

            return driver;
        }

        public static IWebDriver FirefoxDriver()
        {
            string projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            string geckoDriverDirectory = projectFolder + "\\netcoreapp3.1\\";
            FirefoxDriverService geckoService =
            FirefoxDriverService.CreateDefaultService(geckoDriverDirectory);
            geckoService.Host = "::1";
            var firefoxOptions = new FirefoxOptions();
            firefoxOptions.AcceptInsecureCertificates = true;
            return new FirefoxDriver(geckoService, firefoxOptions);
        }

        public static IWebDriver ChromeDriver()
        {
            ChromeOptions options = new ChromeOptions();
            IWebDriver driver = new ChromeDriver(options);
            return driver;
        }
    }
}
