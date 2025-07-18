using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;

namespace ComarchCwiczenia.E2e.Tests.Scenarios;

internal abstract class TestBase
{
    protected IWebDriver driver;
    private ChromeOptions options;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());

        // Wyłączenie jawnego okna przeglądarki
        options.AddArgument("headless");
        options.AddArgument("--disable-gpu");

        // Inne przydatne ustawienia
        options.AddArgument("--window-size=1920,1080");
        options.AddArgument("--lang=en-US");
    }

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver(options);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
    }
}