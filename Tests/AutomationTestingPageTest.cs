using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTests.PageObjects;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using AventStack.ExtentReports.Model;

namespace SeleniumTests.Tests
{
    [TestFixture]
    public class AutomationTestingPageTests
    {
        private ExtentReports _extent;
        private ExtentTest _test;
        private IWebDriver _driver;
        private AutomationTestingPage _page;

        [OneTimeSetUp]
        public void Setup1()
        {
            try
            {
                var reportPath = Path.Combine("C:\\Users\\kamir\\source\\repos\\Selenium_Test", "Reports", "extentReport.html");
                Directory.CreateDirectory(Path.GetDirectoryName(reportPath));
                var htmlReporter = new ExtentSparkReporter(reportPath);
                _extent = new ExtentReports();
                _extent.AttachReporter(htmlReporter);
                Console.WriteLine("ExtentReports initialized successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during OneTimeSetUp: {ex.Message}");
                throw;
            }
        }

        [SetUp]
        public void Setup2()
        {
            try
            {
                _driver = new ChromeDriver();
                _driver.Manage().Window.Maximize();
                _page = new AutomationTestingPage(_driver);
                _page.GoToPage();
                _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
                Console.WriteLine("Test setup completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during SetUp: {ex.Message}");
                throw;
            }
        }

        [TearDown]
        public void Teardown()
        {
            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace) ? "" : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace);
                Status logstatus;

                switch (status)
                {
                    case TestStatus.Failed:
                        logstatus = Status.Fail;
                        _test.Log(logstatus, "Test ended with " + logstatus + stacktrace);
                        break;
                    case TestStatus.Skipped:
                        logstatus = Status.Skip;
                        _test.Log(logstatus, "Test ended with " + logstatus);
                        break;
                    default:
                        logstatus = Status.Pass;
                        _test.Log(logstatus, "Test ended with " + logstatus);
                        break;
                }

                _driver.Quit();
                _extent.Flush();
                Console.WriteLine("Test teardown and report flush completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during TearDown: {ex.Message}");
                throw;
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            try
            {
                _extent.Flush();
                Console.WriteLine("ExtentReports flushed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during OneTimeTearDown: {ex.Message}");
                throw;
            }
        }

        [Test]
        public void Test1_VerifyPageTitle()
        {
            try
            {
                ClassicAssert.AreEqual("Selenium Test Page | Automation in Testing", _driver.Title);
                _test.Log(Status.Pass, "Verified page title successfully.");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, ex.ToString());
                throw;
            }
        }

        [Test]
        public void Test2_VerifyHeadingText()
        {
            try
            {
                ClassicAssert.AreEqual("SELENIUM TEST PAGE", _page.Heading.Text);
                _test.Log(Status.Pass, "Verified heading text successfully.");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, ex.ToString());
                throw;
            }
        }

        [Test]
        public void Test3_VerifyTextInput()
        {
            try
            {
                _page.EnterName("John Doe");
                ClassicAssert.AreEqual("John Doe", _page.InputName.GetAttribute("value"));
                _test.Log(Status.Pass, "Verified text input successfully.");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, ex.ToString());
                throw;
            }
        }

        [Test]
        public void Test4_VerifyTextAreaInput()
        {
            try
            {
                _page.EnterMessage("This is a test message.");
                ClassicAssert.AreEqual("This is a test message.", _page.TextAreaMessage.GetAttribute("value"));
                _test.Log(Status.Pass, "Verified text area input successfully.");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, ex.ToString());
                throw;
            }
        }

        [Test]
        public void Test5_VerifyDropdownSelection()
        {
            try
            {
                _page.SelectDropdownOption("Male");
                ClassicAssert.AreEqual("Male", _page.Dropdown.SelectedOption.Text);
                _test.Log(Status.Pass, "Verified dropdown selection successfully.");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, ex.ToString());
                throw;
            }
        }

        [Test]
        public void Test6_VerifyCheckboxSelection()
        {
            try
            {
                _page.CheckCheckbox();
                ClassicAssert.IsTrue(_page.Checkbox.Selected);
                _test.Log(Status.Pass, "Verified checkbox selection successfully.");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, ex.ToString());
                throw;
            }
        }

        [Test]
        public void Test7_VerifyRadioButtonSelection()
        {
            try
            {
                _page.SelectRadioButton();
                ClassicAssert.IsTrue(_page.RadioButton1.Selected);
                _test.Log(Status.Pass, "Verified radio button selection successfully.");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, ex.ToString());
                throw;
            }
        }

        [Test]
        public void Test8_VerifyButtonClick()
        {
            try
            {
                _page.SubmitForm();
                var js = (IJavaScriptExecutor)_driver;
                var scrollTop = (long)js.ExecuteScript("return window.scrollY;");
                ClassicAssert.AreEqual(0, scrollTop, "The page did not scroll to the top.");

                _test.Log(Status.Pass, "Verified button click and page scroll to top successfully.");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, ex.ToString());
                throw;
            }
        }

        [Test]
        public void Test9_VerifyLinkNavigation()
        {
            try
            {
                _page.ClickLink();
                ClassicAssert.AreEqual("https://automationintesting.com/selenium/testpage/#", _driver.Url);
                _test.Log(Status.Pass, "Verified link navigation successfully.");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, ex.ToString());
                throw;
            }
        }

        [Test]
        public void Test10_VerifyImagePresence()
        {
            try
            {
                ClassicAssert.IsTrue(_page.Image.Displayed);
                _test.Log(Status.Pass, "Verified image presence successfully.");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, ex.ToString());
                throw;
            }
        }
    }
}
