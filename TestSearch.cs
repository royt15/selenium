using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace KaljappSelenium
{
    [TestFixture]
    internal class TestSearch
    {

        protected IWebDriver? driver;
        private const string url = "http://localhost:4200";

        // Locators
        IWebElement SearchInput => driver.FindElement(By.XPath("//input[@name='q']"));
        IWebElement SearchResults => driver.FindElement(By.XPath("//div[contains(@class, 'results')]"));


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
            Assert.That(SearchInput.Displayed);
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
            SearchInput.SendKeys(q);

            // Legacy code would be ClassicAssert.AreEqual(...)
            // https://docs.nunit.org/articles/nunit/writing-tests/assertions/assertions.html#two-models
            Assert.That(SearchInput.GetAttribute("value"), Is.EqualTo(q));

        }

        [Test]
        public void TestThatResultsAppear()
        {
            const string q = "Bar";
            const int maxTimeMs = 2500; 

            SearchInput.Clear();
            SearchInput.SendKeys(q);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(maxTimeMs));
            
            wait.Until((driver) => SearchResults);
            
            if(SearchResults.Displayed) { 
                // Get results
                List<IWebElement> res = SearchResults.FindElements(By.ClassName("result")).ToList();
                Assert.That(res.Count() >= 3);
            } 
            else
            {
                Assert.Fail();
            }

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
