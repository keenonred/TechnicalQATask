using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace TechnicalQATask
{
    [TestFixture(BrowserType.Chrome)]
    [TestFixture(BrowserType.Firefox)]

    public class AutomatedTests : WebDriverFactory
    {
        public AutomatedTests(BrowserType browser) : base(browser) 
        {
            HelperMethods.Init(_driver);
        }

        [SetUp]
        public void SetupTest()
        {
            string url = "https://keenonred.github.io/";
            _driver.Navigate().GoToUrl(url);
        }

        [Test]
        public void SelectPerimeterViaUrl()
        {
            for (var width = 3; width <= 9; width++)
            {
                for (var height = 3; height <= 9; height++)
                {
                    var expectedAlertText = "Done! Ready for the next try? Give me a size [3-9]:";
                    var url = $"https://keenonred.github.io/?width=" + width + "&height=" + height;
                    _driver.Navigate().GoToUrl(url);

                    List<IWebElement> rows = new List<IWebElement>(_driver.FindElements(By.XPath("//div[contains(@class,'mainGrid')]/div[contains(@class,'row')]")));
                    Assert.AreEqual(height, rows.Count, $"Grid does not contain entered number of rows ({height})");
                    var icons = new List<IWebElement>(rows.First().FindElements(By.ClassName("icon")));
                    Assert.AreEqual(width, icons.Count, $"Grid does not contain entered number of columns ({width})");

                    HelperMethods.SelectPerimeter();
                    var alert = _driver.SwitchTo().Alert();
                    Assert.AreEqual(expectedAlertText, alert.Text, "Check prompt message");
                    alert.Dismiss();
                }
            }
        }

        [Test]
        public void SelectPerimeterViaPrompt()
        {
            HelperMethods.SelectPerimeter();

            for (int i = 3; i <= 9; i++)
            {
                var alert = _driver.SwitchTo().Alert();
                var expectedAlertText = "Done! Ready for the next try? Give me a size [3-9]:";
                Assert.AreEqual(expectedAlertText, alert.Text, "Check prompt message");

                alert.SendKeys(i.ToString());
                alert.Accept();

                List<IWebElement> rows = new List<IWebElement>(_driver.FindElements(By.XPath("//div[contains(@class,'mainGrid')]/div[contains(@class,'row')]")));
                Assert.AreEqual(i, rows.Count, $"Grid does not contain entered number of rows ({i})");
                var icons = new List<IWebElement>(rows.First().FindElements(By.ClassName("icon")));
                Assert.AreEqual(i, icons.Count, $"Grid does not contain entered number of columns ({i})");

                HelperMethods.SelectPerimeter();

                if (i == 9)
                {
                    alert.Dismiss();
                }
            }

        }

        [Test]
        [TestCase(-1)]
        [TestCase(2)]
        [TestCase(10)]
        public void UrlInvalidIntBoundaries(int invalidBoundary)
        {
            var url = $"https://keenonred.github.io/?width=" + invalidBoundary + "&height=" + invalidBoundary;
            _driver.Navigate().GoToUrl(url);

            List<IWebElement> rows = new List<IWebElement>(_driver.FindElements(By.XPath("//div[contains(@class,'mainGrid')]/div[contains(@class,'row')]")));
            Assert.AreEqual(4, rows.Count, "Grid does not contain the default number of rows (4)");

            for (var i = 0; i < rows.Count; i++)
            {
                var icons = new List<IWebElement>(rows[i].FindElements(By.ClassName("icon")));
                Assert.AreEqual(4, icons.Count, "Grid does not contain the default number of columns (4)");
            }
        }

        [Test]
        [TestCase(-1)]
        [TestCase(2)]
        [TestCase(10)]
        public void PromptInvalidIntBoundaries(int invalidBoundary)
        {
            HelperMethods.SelectPerimeter();

            var alert = _driver.SwitchTo().Alert();
            alert.SendKeys(invalidBoundary.ToString());
            alert.Accept();

            var expectedAlertText = "Not a good size!";
            Assert.AreEqual(expectedAlertText, alert.Text, "Check prompt message");
            alert.Accept();
        }

        [Test]
        [TestCase("a")]
        [TestCase("$")]
        [TestCase("1.5")]
        public void PromptInvalidStringBoundaries(string invalidBoundary)
        {
            HelperMethods.SelectPerimeter();
            var alert = _driver.SwitchTo().Alert();
            alert.SendKeys(invalidBoundary);
            alert.Accept();

            var expectedAlertText = "Not a good size!";

            Assert.IsTrue(HelperMethods.IsAlertPresent(), "Prompt message is not present");
            Assert.AreEqual(expectedAlertText, alert.Text, "Check prompt message");
            alert.Accept();
        }

        [Test]
        public void SelectAllNoPrompt()
        {
            for (var width = 3; width <= 9; width++)
            {
                for (var height = 3; height <= 9; height++)
                {
                    var url = $"https://keenonred.github.io/?width=" + width + "&height=" + height;
                    _driver.Navigate().GoToUrl(url);
                    HelperMethods.SelectAll();
                    Assert.IsFalse(HelperMethods.IsAlertPresent(), "Prompt message is shown");
                }
            }
        }


        [OneTimeTearDown]
        public void Close()
        {
            _driver.Quit();
        }


    }
}
