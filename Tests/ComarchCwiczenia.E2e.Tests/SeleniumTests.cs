using NUnit.Framework.Internal;
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

    [Test]
    public void HandleJavaScriptAlerts()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");

        var alertButton = driver.FindElement(By.XPath("//*[@id=\"content\"]/div/ul/li[1]/button"));
        alertButton.Click();

        var alert = driver.SwitchTo().Alert();
        Assert.That(alert.Text, Is.EqualTo("I am a JS Alert"));
        alert.Accept();

        var resultText = driver.FindElement(By.Id("result"));
        Assert.That(resultText.Text, Is.EqualTo("You successfully clicked an alert"));

        // Test dla JS Confirm
        var confirmButton = driver.FindElement(By.XPath("//button[text()='Click for JS Confirm']"));
        confirmButton.Click();

        // Przechwytujemy alert confirm
        alert = driver.SwitchTo().Alert();
        Assert.That(alert.Text, Is.EqualTo("I am a JS Confirm"), "Tekst confirm jest nieprawidłowy!");

        // Odrzucamy confirm
        alert.Dismiss();

        // Sprawdzamy, czy wyświetlił się komunikat o odrzuceniu
        resultText = driver.FindElement(By.Id("result"));
        Assert.That(resultText.Text, Is.EqualTo("You clicked: Cancel"), "Komunikat po odrzuceniu confirm jest nieprawidłowy!");



        // Test dla JS Prompt
        var promptButton = driver.FindElement(By.XPath("//button[text()='Click for JS Prompt']"));
        promptButton.Click();

        // Przechwytujemy prompt
        alert = driver.SwitchTo().Alert();
        Assert.That(alert.Text, Is.EqualTo("I am a JS prompt"), "Tekst prompt jest nieprawidłowy!");
        
        // Wprowadzamy tekst do prompta
        alert.SendKeys("Test Selenium");

        // Akceptujemy prompt
        alert.Accept();

        // Sprawdzamy, czy wyświetlił się komunikat z wpisanym tekstem
        resultText = driver.FindElement(By.Id("result"));
        Assert.That(resultText.Text, Is.EqualTo("You entered: Test Selenium"), "Komunikat po akceptacji prompta jest nieprawidłowy!");
    }



}
