using System;
using HelperProject.Context;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.Extensions;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace HelpersProject.Helpers
{
    public class WebDriverHelper
    {


        public IWebDriver CreateWebDriver()
        {
            var browserType = TestContext.Parameters.Get("browser");

            IWebDriver driver;

            switch (browserType.ToLower())
            {
                case "chrome":
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArguments("--browser.download.folderList=2");
                    chromeOptions.AddArguments("--browser.helperApps.neverAsk.saveToDisk");
                    chromeOptions.AddArgument("--start-maximized");
                    chromeOptions.AddArgument("--no-sandbox");
                    chromeOptions.AddArgument("--test-type=ui");
                    chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
                    chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
                    chromeOptions.AddUserProfilePreference("safebrowsing.enabled", true);
                    chromeOptions.AddExcludedArgument("enable-popup-blocking");
                    new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
                    driver = new ChromeDriver(chromeOptions);
                    break;
                case "firefox":
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    driver = new FirefoxDriver();
                    driver.Manage().Window.Maximize();
                    break;
                case "edge":
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    driver = new EdgeDriver();
                    driver.Manage().Window.Maximize();
                    break;
                case "ie":
                    new DriverManager().SetUpDriver(new InternetExplorerConfig());
                    driver = new InternetExplorerDriver();
                    driver.Manage().Window.Maximize();
                    break;
                default:
                    throw new ArgumentException($"Browser with name {browserType} not supported");
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            //driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(15);

            return driver;
        }

        public void CloseAllWebDrivers()
        {
            foreach (var entry in FeatureDictionary.All)
                if (entry.Value.ContainsKey("webDriver"))
                    CloseWebDriver((IWebDriver)entry.Value["webDriver"]);
        }

        public void CloseWebDriver(IWebDriver webDriver)
        {
            webDriver.Dispose();
            webDriver.Quit();
        }

        public void AfterScenario(IWebDriver webDriver)
        {
            // Open new empty tab.
            webDriver.ExecuteJavaScript("window.open('');");

            // Close all tabs but one tab and delete all cookies.
            for (var tabs = webDriver.WindowHandles; tabs.Count > 1; tabs = webDriver.WindowHandles)
            {
                webDriver.SwitchTo().Window(tabs[0]);
                webDriver.Manage().Cookies.DeleteAllCookies();
                webDriver.Close();
            }

            // Switch to empty tab.
            webDriver.SwitchTo().Window(webDriver.WindowHandles[0]);
        }
    }
}
