using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace KaljappSelenium
{
    [TestFixture]
    internal class TestSearch
    {

        protected IWebDriver? driver;
        private const string url = "http://localhost:4200";

        // Elements
        private readonly By searchInput = By.XPath("//input[@name='q']");


        [OneTimeSetUp]
        public void SetUp()
        {
            driver = new ChromeDriver(GetChromeOptions());
            driver.Manage().Timeouts().ImplicitWait = new System.TimeSpan(1);

            // Initial navigation
            driver.Navigate().GoToUrl(url);
        }


        [Test]
        public void TestSearchIsVisible()
        {
            IWebElement el = driver.FindElement(searchInput);
            Assert.That(el.Displayed);
        }

        [Test]
        public void TestSearchIsEnabled()
        {
            IWebElement el = driver.FindElement(By.XPath("//input[@name='q']"));
            Assert.That(el.Enabled);
        }

        [Test]
        public void TestSearchQueryCanBeWritten()
        {
            const string q = "Bar Tentti"; 
            IWebElement el = driver.FindElement(searchInput);
            el.SendKeys(q);

            // Legacy code would be ClassicAssert.AreEqual(...)
            // https://docs.nunit.org/articles/nunit/writing-tests/assertions/assertions.html#two-models
            Assert.That(el.GetAttribute("value"), Is.EqualTo(q));

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }


        private static ChromeOptions GetChromeOptions()
        {
            var options = new ChromeOptions();
            options.AddArgument("--window-size=860,1024");
            options.AddArgument("--start-maximized");
            //options.AddArgument("--headless");
            return options;
        }

    }
}
