using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace AuthorizationTests
{
    public class Tests
    {
        private IWebDriver driver;
        private readonly By _pricingLink = By.XPath("//*[@id=\"header\"]/div[2]/div/nav/a[1]");
        private readonly By _loginButton = By.XPath("//*[@id=\"header\"]/div[2]/div/div[2]/a[2]");
        private readonly By _emailInput = By.XPath("//*[@id=\"username\"]");
        private readonly By _passwordInput = By.XPath("//*[@id=\"password\"]");
        private readonly By _loginButton2 = By.XPath("/html/body/div[4]/div/form/button");

        [SetUp]
        public void Setup() 
        {
            driver = new OpenQA.Selenium.Edge.EdgeDriver();
            driver.Navigate().GoToUrl("https://evrasia.spb.ru/");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }

        [Test]
        public void TestPageTitle()
        {
            Assert.That(driver.Title, Is.EqualTo("🍣Рестораны Евразия"));
        }

        [Test]
        public void TestObjectVisibility()
        {
            var element = driver.FindElement(_loginButton);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(element.Displayed);
        }

        [Test]
        public void TestLinkNavigation()
        {
            var link = driver.FindElement(_pricingLink);
            Thread.Sleep(1000);
            link.Click();
            Thread.Sleep(1000);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("https://evrasia.spb.ru/menu/", driver.Url);
        }

        [Test]
        public void TestLogin()
        {
            Thread.Sleep(1000);
            var s1 = driver.FindElement(_loginButton);
            s1.Click();
            Thread.Sleep(1000);
            var s2 = driver.FindElement(_emailInput);
            s2.SendKeys("9965026192");
            Thread.Sleep(1000);
            var s3 = driver.FindElement(_passwordInput);
            s3.SendKeys("lgu50202");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("+7 (996) 502-61-92", s2.GetAttribute("value"));
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("lgu50202", s3.GetAttribute("value"));
            Thread.Sleep(1000);
            var s4 = driver.FindElement(_loginButton2);
            s4.Click();
            Thread.Sleep(1000);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("https://evrasia.spb.ru/account/", driver.Url);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}