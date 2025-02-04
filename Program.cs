using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        IWebDriver chromeDriver = new ChromeDriver();
        string url = "https://www.google.fi";
        chromeDriver.Navigate().GoToUrl(url);
        string title = chromeDriver.Title;
        Console.WriteLine($"{url} title is: ${title}");
        Console.In.ReadLine();

    }
}

