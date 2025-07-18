using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
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

    [Test]
    public void CorrectLoginTest()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");

        var usernameField = driver.FindElement(By.Id("username"));
        var passwordField = driver.FindElement(By.Id("password"));

        usernameField.SendKeys("tomsmith");
        passwordField.SendKeys("SuperSecretPassword!");

        var loginButton = driver.FindElement(By.XPath("//*[@id=\"login\"]/button"));
        loginButton.Click();

        var successMessage = driver.FindElement(By.CssSelector("#flash"));
        Assert.That(successMessage.Text, Does.Contain("You logged into a secure area!"));
    }

    [Test]
    public void IncorrectLoginTest()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");

        var usernameField = driver.FindElement(By.Id("username"));
        var passwordField = driver.FindElement(By.Id("password"));

        usernameField.SendKeys("wrongUser");
        passwordField.SendKeys("wrongPassword!");

        var loginButton = driver.FindElement(By.CssSelector("button[type='submit']"));
        loginButton.Click();

        var errorMessage = driver.FindElement(By.Id("flash"));
        Assert.That(errorMessage.Text, Does.Contain("Your username is invalid!"));
    }

    [Test]
    public void CheckboxTest()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/checkboxes");
        var checkbox = driver.FindElement(By.XPath("//*[@id=\"checkboxes\"]/input[1]"));
        checkbox.Click();
        bool isChecked = checkbox.Selected;

        Assert.That(isChecked, Is.True);
    }

    [Test]
    public void DropDownTest()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dropdown");
        var dropdown = driver.FindElement(By.Id("dropdown"));
        var selectElement = new SelectElement(dropdown);

        selectElement.SelectByValue("1");

        Assert.That(selectElement.SelectedOption.Text, Is.EqualTo("Option 1"));

        selectElement.SelectByValue("2");
        Assert.That(selectElement.SelectedOption.Text, Is.EqualTo("Option 2"));

    }

}
