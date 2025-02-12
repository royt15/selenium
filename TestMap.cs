using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace KaljappSelenium
{
    [TestFixture(Description = "Map tests")]
    internal class TestMap
    {

        protected IWebDriver? driver;

        private IWebElement mapEl => driver.FindElement(By.Id("map"));

        // TODO: get from some file or something
        private const string url = "http://localhost:4200";

        [OneTimeSetUp]
        public void SetUp()
        {
            driver = new ChromeDriver(TestUtils.GetChromeOptions());
            driver.Manage().Timeouts().ImplicitWait = new System.TimeSpan(1);

            // Initial navigation
            driver.Navigate().GoToUrl(url);
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

        [OneTimeTearDown]
        public void TearDown() {
            TestUtils.TearDownDriver(ref driver!);
        }



    }
}
