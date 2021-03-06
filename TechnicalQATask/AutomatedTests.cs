using NUnit.Framework;
using System.Threading;

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
        public void EnterValidIntViaUrl()
        {
            for (int width = 3; width <= 9; width++)
            {
                for (int height = 3; height <= 9; height++)
                {
                    string expectedAlertText = "Done! Ready for the next try? Give me a size [3-9]:";
                    string url = $"https://keenonred.github.io/?width={width}&height={height}";
                    _driver.Navigate().GoToUrl(url);
                    HelperMethods.AssertCustomGrid(height, width);
                    HelperMethods.SelectPerimeter();
                    var alert = _driver.SwitchTo().Alert();
                    Assert.AreEqual(expectedAlertText, alert.Text, "Check prompt message");
                    alert.Dismiss();
                }
            }
        }

        [Test]
        public void EnterValidIntViaPrompt()
        {
            HelperMethods.SelectPerimeter();

            for (int i = 3; i <= 9; i++)
            {
                var alert = _driver.SwitchTo().Alert();
                string expectedAlertText = "Done! Ready for the next try? Give me a size [3-9]:";
                Assert.AreEqual(expectedAlertText, alert.Text, "Check prompt message");
                alert.SendKeys(i.ToString());
                alert.Accept();
                Thread.Sleep(100);
                HelperMethods.AssertCustomGrid(i, i);
                HelperMethods.SelectPerimeter();

                if (i == 9)
                {
                    alert.Dismiss();
                }
            }
        }

        [Test]
        [TestCase(2, 3)]
        [TestCase(2, 10)]
        [TestCase(3, 10)]
        public void EnterInvalidIntViaUrl(int width, int height)
        {
            string url = $"https://keenonred.github.io/?width={width}&height={height}";
            _driver.Navigate().GoToUrl(url);
            HelperMethods.AssertDefaultGrid();
        }

        [Test]
        [TestCase(-1)]
        [TestCase(2)]
        [TestCase(350)]
        public void EnterInvalidIntViaPrompt(int invalidBoundary)
        {
            HelperMethods.SelectPerimeter();
            var alert = _driver.SwitchTo().Alert();
            alert.SendKeys(invalidBoundary.ToString());
            alert.Accept();
            string expectedAlertText = "Not a good size!";
            Assert.IsTrue(HelperMethods.IsAlertPresent(), "Prompt message is not present");
            Assert.AreEqual(expectedAlertText, alert.Text, "Check prompt message");
            alert.Accept();
            Thread.Sleep(100);
            HelperMethods.AssertDefaultGrid();
        }

        [Test]
        [TestCase("a")]
        [TestCase("$")]
        [TestCase("1.5")]
        public void EnterInvalidStringViaPrompt(string invalidBoundary)
        {
            HelperMethods.SelectPerimeter();
            var alert = _driver.SwitchTo().Alert();
            alert.SendKeys(invalidBoundary);
            alert.Accept();
            string expectedAlertText = "Not a good size!";
            Assert.IsTrue(HelperMethods.IsAlertPresent(), "Prompt message is not present");
            Assert.AreEqual(expectedAlertText, alert.Text, "Check prompt message");
            alert.Accept();
            HelperMethods.AssertDefaultGrid();
        }

        [Test]
        public void SelectAllNoPrompt()
        {
            for (int width = 3; width <= 9; width++)
            {
                for (int height = 3; height <= 9; height++)
                {
                    string url = $"https://keenonred.github.io/?width={width}&height={height}";
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
