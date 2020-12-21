using NUnit.Framework;
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
            List<IWebElement> rows = ReturnAllRows();

            for (int i = 0; i < rows.Count; i++)
            {
                List<IWebElement> icons = ReturnIconsInARow(rows[i]);

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
            List<IWebElement> rows = ReturnAllRows();

            for (int i = 0; i < rows.Count; i++)
            {
                List<IWebElement> icons = ReturnIconsInARow(rows[i]);

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

        public static void AssertDefaultGrid()
        {
            int defaultGridSize = 4;
            List<IWebElement> rows = ReturnAllRows();
            Assert.AreEqual(defaultGridSize, rows.Count, "Grid does not contain the default number of rows");

            for (int i = 0; i < rows.Count; i++)
            {
                List<IWebElement> icons = ReturnIconsInARow(rows[i]);
                Assert.AreEqual(defaultGridSize, icons.Count, "Grid does not contain the default number of columns");
            }
        }

        public static void AssertCustomGrid(int height, int width)
        {
            List<IWebElement> rows = ReturnAllRows();
            Assert.AreEqual(height, rows.Count, $"Grid does not contain entered number of rows ({height})");
            List<IWebElement> icons = ReturnIconsInARow(rows.First());
            Assert.AreEqual(width, icons.Count, $"Grid does not contain entered number of columns ({width})");
        }

        private static List<IWebElement> ReturnAllRows()
        {
            List<IWebElement> rows = new List<IWebElement>(_driver.FindElements(By.XPath("//div[contains(@class,'mainGrid')]/div[contains(@class,'row')]")));
            return rows;
        }
        
        private static List<IWebElement> ReturnIconsInARow(IWebElement row)
        {
            List<IWebElement> icons = new List<IWebElement>(row.FindElements(By.ClassName("icon")));
            return icons;
        }
    }
}
