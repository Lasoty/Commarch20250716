using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComarchCwiczenia.E2e.Tests.PageObjects;
using WebDriverManager.DriverConfigs.Impl;

namespace ComarchCwiczenia.E2e.Tests.Scenarios;

[TestFixture]
internal class LoginTests : TestBase
{
    [Test]
    public void LoginWithInvalidCredentials_ShowsErrorMessage()
    {
        var page = new LoginPage(driver);
        page.EnterUserName("tomsmith");
        page.EnterPassword("wrongPassword");
        page.ClickLoginButton();
        Assert.That(page.IsErrorMessageDisplayed(), Is.True);
    }
}