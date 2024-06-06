using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumTests.PageObjects
{
    public class AutomationTestingPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public AutomationTestingPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        // Page Elements
        public IWebElement Title => _driver.FindElement(By.TagName("title"));
        public IWebElement Heading => _driver.FindElement(By.TagName("h1"));
        public IWebElement InputName => _driver.FindElement(By.Id("firstname"));
        public IWebElement TextAreaMessage => _driver.FindElement(By.TagName("textarea"));
        public SelectElement Dropdown => new SelectElement(_driver.FindElement(By.Id("gender")));
        public IWebElement Checkbox => _driver.FindElement(By.Id("checkbox1"));
        public IWebElement RadioButton1 => _driver.FindElement(By.Id("red"));
        public IWebElement SubmitButton => _driver.FindElement(By.Id("submitbutton"));
        public IWebElement SubmitResult => _wait.Until(drv => drv.FindElement(By.Id("submitResult")));
        public IWebElement Link => _driver.FindElement(By.LinkText("Our Policy"));
        public IWebElement Image => _driver.FindElement(By.TagName("img"));

        // Actions
        public void GoToPage()
        {
            _driver.Navigate().GoToUrl("https://automationintesting.com/selenium/testpage/");
        }

        public void EnterName(string name)
        {
            InputName.SendKeys(name);
        }

        public void EnterMessage(string message)
        {
            TextAreaMessage.SendKeys(message);
        }

        public void SelectDropdownOption(string optionText)
        {
            Dropdown.SelectByText(optionText);
        }

        public void CheckCheckbox()
        {
            if (!Checkbox.Selected)
            {
                Checkbox.Click();
            }
        }

        public void SelectRadioButton()
        {
            RadioButton1.Click();
        }

        public void SubmitForm()
        {
            SubmitButton.Click();
        }

        public void ClickLink()
        {
            Link.Click();
        }
    }
}
