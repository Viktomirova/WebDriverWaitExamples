using NUnit.Framework;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

using SeleniumExtras.WaitHelpers;

using System;
using System.Threading;

namespace WebDriverWaitExamples
{
    public class WaitTests
    {
        private WebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://www.uitestpractice.com/Students/Contact");
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15)); // For ExplicitWait
        }

        [TearDown]
        public void CloseDriver()
        {
            driver.Close();
        }

        [Test]
        public void Test_Wait_ThreadSleep()
        {
            // Bad practice!!! Test duration > 18.8sec
            driver.FindElement(By.PartialLinkText("This is")).Click();
            Thread.Sleep(15000);
            string text_element = driver.FindElement(By.ClassName("ContactUs")).Text;
            Assert.IsNotEmpty(text_element);
        }
        
        [Test]
        public void Test_Wait_ImplicitWait()
        {
            // Test duration 11.5sec
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            driver.FindElement(By.PartialLinkText("This is")).Click();
            string text_element = driver.FindElement(By.ClassName("ContactUs")).Text;
            Assert.IsNotEmpty(text_element);
        }
        
        [Test]
        public void Test_Wait_ExplicitWait()
        {
            // Test duration 9.4sec
            driver.FindElement(By.PartialLinkText("This is")).Click();
            var text_element = wait.Until(d =>
            {
                return d.FindElement(By.ClassName("ContactUs")).Text;
            });
            Assert.IsNotEmpty(text_element);
        }
        
        [Test]
        public void Test_Wait_ExpectedConditions()
        {
            // Test duration 9.5sec
            driver.FindElement(By.PartialLinkText("This is")).Click();
            string text_element = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("ContactUs"))).Text;
            Assert.IsNotEmpty(text_element);
        }
    }
}