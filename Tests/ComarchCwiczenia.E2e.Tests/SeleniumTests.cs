using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;

namespace ComarchCwiczenia.E2e.Tests;

[TestFixture]
public class SeleniumTests
{
    private IWebDriver driver;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
    }

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
    }

    [Test]
    public void CheckPageTitle()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com");

        Assert.That(driver.Title, Is.EqualTo("The Internet"),
            "Tytuł strony jest nieprawidłowy");
    }
}
