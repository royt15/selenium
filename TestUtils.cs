using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace KaljappSelenium
{
    internal static class TestUtils
    {

            

        public static ChromeOptions GetChromeOptions()
        {
            var options = new ChromeOptions();
            options.AddArgument("--window-size=860,1024");
            options.AddArgument("--start-maximized");
            //options.AddArgument("--headless");
            return options;
        }


        public static void TearDownDriver(ref IWebDriver d)
        {
            d.Quit();
            d.Dispose();
        }

    }
}
