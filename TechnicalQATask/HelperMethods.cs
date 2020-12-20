
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace TechnicalQATask
{
    public class HelperMethods
    {
        public static IWebDriver _driver;

        public static void Init(IWebDriver driver)
        {
            _driver = driver;
        }

        public static void SelectPerimeter()
        {
            List<IWebElement> rows = new List<IWebElement>(_driver.FindElements(By.XPath("//div[contains(@class,'mainGrid')]/div[contains(@class,'row')]")));

            for (var i = 0; i < rows.Count; i++)
            {
                var icons = new List<IWebElement>(rows[i].FindElements(By.ClassName("icon")));

                if (i == 0 || i == rows.Count - 1)
                {
                    foreach (var icon in icons)
                    {
                        icon.Click();
                    }
                }
                else
                {
                    icons.First().Click();
                    icons.Last().Click();
                }
            }
        }

        public static void SelectAll()
        {
            List<IWebElement> rows = new List<IWebElement>(_driver.FindElements(By.XPath("//div[contains(@class,'mainGrid')]/div[contains(@class,'row')]")));

            for (var i = 0; i < rows.Count; i++)
            {
                var icons = new List<IWebElement>(rows[i].FindElements(By.ClassName("icon")));

                foreach (var icon in icons)
                {
                    icon.Click();
                }

            }
        }

        public static bool IsAlertPresent()
        {
            try
            {
                _driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
    }
}
