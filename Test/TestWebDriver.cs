using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestFixture]

    public class TestWebDriver
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new EdgeDriver();
            driver.Navigate().GoToUrl("https://yame.vn/");
        }
        [TearDown]
        public void Teardown()
        {
            driver.Quit();

        }

        [Test]
        public void TestSearch_AoThun()
        {
            TestSearch("Áo Thun");
        }

        [Test]
        public void TestSearch_aothun()
        {
            TestSearch("áo thun");
        }


        private void TestSearch(string keyword)
        {

            string originalUrl = driver.Url;

            var searchIcon = driver.FindElement(By.XPath("/html/body/nav/div/div[1]/div[2]/div[1]/button/span[1]"));
            searchIcon.Click();

            var searchBar = driver.FindElement(By.Id("keyword"));
            searchBar.Clear();
            searchBar.SendKeys(keyword);
            var buttonSearch = driver.FindElement(By.XPath("/html/body/nav/div/div[1]/div[2]/div[1]/div/div/div/div/form/button"));
            buttonSearch.Click();


            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                wait.Until(d => !d.Url.Equals(originalUrl));
                string title = driver.Title;

                // Assertion: Kiểm tra xem tiêu đề có chứa từ khóa hay không
                Assert.IsTrue(title.Contains(keyword), $"Tiêu đề không chứa từ khóa '{keyword}'");



                driver.Navigate().GoToUrl("https://yame.vn/"); // Quay về trang chủ
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail($"Kết quả tìm kiếm cho '{keyword}': Không tìm thấy (Timeout)");
            }
        }
    }
}
