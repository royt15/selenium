using Newtonsoft.Json.Bson;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace KaljappSelenium
{
    [TestFixture(Description = "Map tests")]
    internal class TestMap
    {

        protected IWebDriver driver;

        private IWebElement mapEl => driver.FindElement(By.Id("map"));


        [OneTimeSetUp]
        public void SetUp()
        {
            driver = new ChromeDriver(TestUtils.GetChromeOptions());
            driver.Manage().Timeouts().ImplicitWait = new System.TimeSpan(1);

            // Initial navigation
            driver.Navigate().GoToUrl(TestUtils.testUrl);
        }


        [Test]
        public void TestMapCanBeMoved()
        {
            Actions actions = new Actions(driver);
            Random r = new Random();

            var windowSize = driver.Manage().Window.Size;
            string urlAtBegin = driver.Url;

            // Move to element and click
            var dims = mapEl.Location;
            Tuple<int, int> center = Tuple.Create(dims.X + mapEl.Size.Width / 2, dims.Y + mapEl.Size.Height / 2);
            Tuple<int, int> dragOffset = Tuple.Create(center.Item1 + r.Next(1, 50), center.Item2 + 1 + r.Next(1, 50));

            actions
                .MoveToLocation(center.Item1, center.Item2)
                .ClickAndHold()
                .MoveToLocation(dragOffset.Item1, dragOffset.Item2) 
                .Release()
                .Perform();

            // Assert
            Assert.That(!driver.Url.Equals(urlAtBegin));
            Assert.That(driver.Url.Contains("lat="));
            Assert.That(driver.Url.Contains("long="));

        }

        [Test]
        public void TestMarkerClick()
        {
            
            Actions actions = new Actions(driver);

            try
            {
                driver.Navigate().GoToUrl(TestUtils.testUrl + "/map?lat=60.1645023&long=24.94434267282");

                // Wait markers to load, Pick any marker
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(5000));
                var marker = wait.Until((driver) => driver.FindElement(By.CssSelector("img.leaflet-marker-icon")));

                actions.Click(marker).Perform();

                IWebElement infoPopup = driver.FindElement(By.ClassName("bar-info-popup"));
                
                Assert.That(infoPopup.Displayed);
                Assert.That(infoPopup.Size.Width >= 480); // px
                Assert.That(infoPopup.Size.Height > 0); // px

            } catch (NoSuchElementException e)
            {
                Assert.Fail(e.ToString());
            }

        }

        [OneTimeTearDown]
        public void TearDown() {
            driver.Quit();
            driver.Dispose();
        }



    }
}
