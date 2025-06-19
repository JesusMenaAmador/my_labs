using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;

namespace UIAutomationTests
{
    [TestFixture]
    public class Selenium
    {
        IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            _driver = new FirefoxDriver();
        }

        [Test]
        public void Enter_To_List_Of_Countries_Test()
        {
            var URL = "http://localhost:8080/";

            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(URL);

            Assert.That(_driver.Title, Is.Not.Empty, "The page title should not be empty after navigating to the URL.");
            }
    }
}
