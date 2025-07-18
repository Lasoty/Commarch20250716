using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace ComarchCwiczenia.E2e.Tests.PageObjects;

public class LoginPage
{
    private readonly IWebDriver driver;

    public LoginPage(IWebDriver driver)
    {
        this.driver = driver;
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");
    }

    public IWebElement UsernameField => driver.FindElement(By.Id("username"));
    public IWebElement PasswordField => driver.FindElement(By.Id("password"));
    public IWebElement LoginButton => driver.FindElement(By.XPath("//*[@id=\"login\"]/button"));
    public IWebElement ErrorMessage => driver.FindElement(By.CssSelector(".flash.error"));

    public void EnterUserName(string username)
    {
        UsernameField.SendKeys(username);
    }

    public void EnterPassword(string password)
    {
        PasswordField.SendKeys(password);
    }

    public void ClickLoginButton()
    {
        LoginButton.Click();
    }

    public bool IsErrorMessageDisplayed()
    {
        return ErrorMessage.Displayed;
    }
}
