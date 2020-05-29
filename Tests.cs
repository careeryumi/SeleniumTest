using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Text.RegularExpressions;

//Yumi Lee
//April 20 2020

namespace YLeeFinalExam
{
    public class Tests
    {
        IWebDriver driver;

        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();
            driver.Manage().Cookies.DeleteAllCookies();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);

            driver.Navigate().GoToUrl("https://www.calculator.net/triangle-calculator.html");

        }

        [TearDown]
        public void closeBrowser()
        {
            if (driver != null)
            {
                driver.Quit();
            }

        }

        [Test]
        public void test_InputLargeDegree_ExpectedResultError()
        {
            //Arrange
            IWebElement vbField = driver.FindElement(By.Name("vb"));

            Thread.Sleep(3000);

            //Act
            vbField.SendKeys("180");

            Thread.Sleep(3000);

            driver.FindElement(By.XPath("//*[@id='content']/table[1]/tbody/tr/td/table/tbody/tr[5]/td/input")).Click();

            string expectedError = "Angle \"b\" too big.";

            String actual_error = driver.FindElement(By.XPath("//*[@id='content']/p[2]/font")).Text;

            Thread.Sleep(3000);

            //Assert
            Assert.AreEqual(expectedError, actual_error);

        }


        [Test]
        public void test_Searchbar_ExpectedResultIs_VolumeLink()
        {
            //Arrange
            IWebElement searchBar = driver.FindElement(By.Name("calcSearchTerm"));
            searchBar.Clear();

            Thread.Sleep(3000);

            //Act
            searchBar.SendKeys("volume");

            Thread.Sleep(3000);

            driver.FindElement(By.Id("bluebtn")).Click();

            string expectedLink = "Volume Calculator";

            String actualLinkText = driver.FindElement(By.XPath("//*[@id='calcSearchOut']/div/a")).Text;

            Thread.Sleep(3000);

            //Assert
            Assert.AreEqual(expectedLink, actualLinkText);

        }

        [Test]
        public void test_Calculate_ExpectedResultIs_RightIsoscelesTriangle()
        {
            //Arrange
            IWebElement fieldvc = driver.FindElement(By.Name("vc"));
            fieldvc.Clear();

            Thread.Sleep(3000);

            //Act
            fieldvc.SendKeys("90");

            Thread.Sleep(3000);

            driver.FindElement(By.XPath("//*[@id='content']/table[1]/tbody/tr/td/table/tbody/tr[5]/td/input")).Click();

            Thread.Sleep(3000);

            string expectedResult = "Right Isosceles Triangle";

            String actual_result = driver.FindElement(By.XPath("//*[@id='content']/table[1]/tbody/tr/td[1]/h3")).Text;

            Thread.Sleep(3000);

            //Assert
            Assert.AreEqual(expectedResult, actual_result);

        }

        [Test]
        public void test_ClearButton_ExpectedResultIs_EmptyString()
        {
            //Arrange
            IWebElement fieldvc = driver.FindElement(By.Name("vc"));
            fieldvc.SendKeys("15");

            Thread.Sleep(3000);

            //Act

            driver.FindElement(By.XPath("//*[@id='content']/table[1]/tbody/tr/td/table/tbody/tr[5]/td/img")).Click();

            Thread.Sleep(3000);

            String actual_result = driver.FindElement(By.Name("vc")).Text;

            string expectedResult = "";

            Thread.Sleep(3000);

            //Assert
            Assert.AreEqual(expectedResult, actual_result);

        }

        [Test]
        public void test_DropDown_ExpectedResultIs_radian()
        {
            //Arrange
            new SelectElement(driver.FindElement(By.Name("angleunits"))).SelectByValue("r");

            Thread.Sleep(3000);

            //Act
            IWebElement dropdown = driver.FindElement(By.Name("angleunits"));
            SelectElement selectedValue = new SelectElement(dropdown);

            string actualText = selectedValue.SelectedOption.Text;

            string expectedText = "radian";

            //Assert
            Assert.AreEqual(expectedText, expectedText);

            Thread.Sleep(3000);

        }

    }
}
