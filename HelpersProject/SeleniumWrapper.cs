using Castle.Core.Internal;
using HelperProject.Context;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using HelpersProject.Helpers;

namespace HelperProject
{
    //Auxiliary class for selenium methods
    public static class SeleniumWrapper
    {
        public static IWebDriver GetDriver()
        {
            if (!FeatureDictionary.Current.ContainsKey("webDriver"))
                FeatureDictionary.Current["webDriver"] = new WebDriverHelper().CreateWebDriver();
            return (IWebDriver)FeatureDictionary.Current["webDriver"];
        }

        public static IWebElement FindElement(By locator, int waitTime = 25, bool waitPageLoad = true)
        {
            WaitElementVisible(locator, waitTime, waitPageLoad);
            return GetDriver().FindElement(locator);
        }

        public static IWebElement FindElementByText(string text)
        {
            return FindElement(By.XPath($"//*[contains(text(), '{text}')]"));
        }

        public static IWebElement FindElementByText(ReadOnlyCollection<IWebElement> elements, string text)
        {
            return elements.First(i => i.Text.Equals(text));
        }

        public static ReadOnlyCollection<IWebElement> FindElements(By locator, int waitTime = 25, bool waitPageLoad = true)
        {
            var url = GetDriver().Url;
            WaitElementVisible(locator, waitTime, waitPageLoad);
            return GetDriver().FindElements(locator);
        }

        public static void WaitForPageUpdated()
        {
            WaitPageIsLoaded();
            try { WaitAjaxIsNotComplete(); }
            catch { WaitRequestsIsNotComplete(); }
            WaitLoaderIsDisabled();
        }

        public static void WaitPageIsLoaded(int waitTime = 30)
        {
            var executor = (IJavaScriptExecutor)GetDriver();

            for (var i = 0; i < waitTime * 4; i++)
            {
                if (((string)executor.ExecuteScript("return document.readyState")).Equals("complete"))
                    return;
                Thread.Sleep(250);
            }

            throw new Exception($"Page has not been loaded in {waitTime} seconds");
        }

        public static void WaitAjaxIsNotComplete(int waitTime = 60)
        {
            for (var i = 0; i < waitTime * 4; i++)
            {
                if ((long)((IJavaScriptExecutor)GetDriver()).ExecuteScript("return jQuery.active") == 0)
                    return;
                Thread.Sleep(250);
            }
        }

        /// <summary>
        /// This method finds parent of element by class name.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static IWebElement GetElementParentByClass(IWebElement element, string className)
        {
            try
            {
                var parent = element;
                do
                    parent = parent.FindElement(By.XPath("./parent::*"));
                while (!parent.GetAttribute("class").Equals(className));

                return parent;
            }
            catch
            {
                return null;
            }
        }

        public static void WaitRequestsIsNotComplete(int waitTime = 60)
        {
            var scriptToExecute = "var performance = window.performance || window.mozPerformance || window.msPerformance || window.webkitPerformance || {}; var network = performance.getEntries() || {}; return network.length;";
            var netData = (long)((IJavaScriptExecutor)GetDriver()).ExecuteScript(scriptToExecute);

            for (var i = 0; i < waitTime; i++)
            {
                Thread.Sleep(500);
                var waitNetData = (long)((IJavaScriptExecutor)GetDriver()).ExecuteScript(scriptToExecute);
                if (waitNetData.Equals(netData))
                    i = waitTime;
                else
                    netData = waitNetData;
            }
        }

        public static void WaitLoaderIsDisabled(int waitTime = 30)
        {
            var executor = (IJavaScriptExecutor)GetDriver();

            for (var i = 0; i <= waitTime * 4; i++)
            {
                if (executor.ExecuteScript("return document.querySelector('div.loader')") == null)
                    return;
                Thread.Sleep(250);
            }

            throw new Exception($"Spinner did not disabled within {waitTime} seconds");
        }
        

        /// <summary>
        /// Wait spinner is disappear.
        /// </summary>
        /// <param name="timeout">Maximum waiting time</param>
        public static void WaitForSpinner(int timeout = 15)
        {
            try
            {
                new WebDriverWait(GetDriver(), TimeSpan.FromSeconds(timeout)).Until(
#pragma warning disable 618
                    ExpectedConditions.InvisibilityOfElementLocated(
#pragma warning restore 618
                        By.XPath("//div[contains(@class,'spinner-overlay sp-grey visible')]")));
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException("Spinner did not disappear within " + timeout + " seconds");
            }
        }

        /// <summary>
        /// Waiting for an element to visible.
        /// </summary>
        /// <param name="locator">The locator of the element</param>
        /// <param name="waitTime">Maximum waiting time</param>
        /// <param name="waitPageLoad"></param>
        public static void WaitElementVisible(By locator, int waitTime = 25, bool waitPageLoad = true)
        {
            if (waitPageLoad)
                WaitForPageUpdated();

            try
            {
                new WebDriverWait(GetDriver(), TimeSpan.FromSeconds(waitTime))
#pragma warning disable 618
                    .Until(ExpectedConditions.ElementIsVisible(locator));
#pragma warning restore 618
            }
            catch (WebDriverException)
            {
                throw new WebDriverException($"The element with locator -> {locator} <- did not visible for {waitTime} seconds");
            }
        }

        /// <summary>
        /// Waiting for an element to clickable.
        /// </summary>
        /// <param name="locator">The locator of the element.</param>
        /// <param name="waitTime">Maximum waiting time.</param>
        public static void WaitElementClickable(By locator, int waitTime = 25)
        {
            try
            {
                new WebDriverWait(GetDriver(), TimeSpan.FromSeconds(waitTime)).Until(
#pragma warning disable 618
                    ExpectedConditions.ElementToBeClickable(locator));
#pragma warning restore 618
            }
            catch (WebDriverException)
            {
                throw new WebDriverException("The element did not visible and clickable for " + waitTime + " seconds");
            }
        }

        /// <summary>
        /// Waiting for an element to clickable.
        /// </summary>
        /// <param name="element">The locator of the element.</param>
        /// <param name="waitTime">Maximum waiting time.</param>
        public static void WaitElementClickable(IWebElement element, int waitTime = 25)
        {
            try
            {
                new WebDriverWait(GetDriver(), TimeSpan.FromSeconds(waitTime)).Until(
#pragma warning disable 618
                    ExpectedConditions.ElementToBeClickable(element));
#pragma warning restore 618
            }
            catch (WebDriverException)
            {
                throw new WebDriverException("The element did not visible and clickable for " + waitTime + " seconds");
            }
        }

        /// <summary>
        /// Waiting for an element to invisible.
        /// </summary>
        /// <param name="locator">The locator of the element</param>
        /// <param name="waitTime">Maximum waiting time</param>
        public static void WaitElementInvisible(By locator, int waitTime = 5)
        {
            try
            {
                new WebDriverWait(GetDriver(), TimeSpan.FromSeconds(waitTime))
#pragma warning disable 618
                    .Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
#pragma warning restore 618
            }
            catch (WebDriverException)
            {
                throw new WebDriverException("The element is visible for " + waitTime + " seconds");
            }
        }

        public static bool IsElementContainsElement(IWebElement element, By locator)
        {
            try
            {
                element.FindElement(locator);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Element is displayed
        /// </summary>
        /// <param name="locator">The locator of the element</param>
        /// <param name="waitTime">Maximum waiting time</param>
        /// <param name="waitPageLoad"></param>
        public static bool IsElementVisible(By locator, int waitTime = 3, bool waitPageLoad = true)
        {
            try
            {
                WaitElementVisible(locator, waitTime, waitPageLoad);
                return true;
            }
            catch (WebDriverException)
            {
                return false;
            }
        }

        public static bool IsElementDisabled(By locator)
        {
            return IsElementDisabled(FindElement(locator));
        }

        public static bool IsElementDisabled(IWebElement element)
        {
            return bool.Parse(element.GetAttribute("disabled") ?? "false");
        }
        public static bool IsElementDisabledByClass(By locator)
        {
            return FindElement(locator).GetAttribute("class").Equals("disabled");
        }

        public static bool IsElementReadOnly(By locator)
        {
            return IsElementReadOnly(FindElement(locator));
        }

        public static bool IsElementReadOnly(IWebElement element)
        {
            return bool.Parse(element.GetAttribute("readOnly") ?? "false");
        }

        /// <summary>
        /// Element is clickable.
        /// </summary>
        /// <param name="locator">The locator of the element.</param>
        /// <param name="waitTime">Maximum waiting time.</param>
        public static bool IsElementClickable(By locator, int waitTime = 3)
        {
            bool res;
            try
            {
                WaitElementClickable(locator, waitTime);
                res = true;
            }
            catch (WebDriverException)
            {
                res = false;
            }

            return res;
        }

        /// <summary>
        /// Element is clickable.
        /// </summary>
        /// <param name="element">The locator of the element.</param>
        /// <param name="waitTime">Maximum waiting time.</param>
        public static bool IsElementClickable(IWebElement element, int waitTime = 3)
        {
            bool res;
            try
            {
                WaitElementClickable(element, waitTime);
                res = true;
            }
            catch (WebDriverException)
            {
                res = false;
            }

            return res;
        }

        public static IWebElement GetParent(IWebElement element)
        {
            return element.FindElement(By.XPath("./parent::*"));
        }

        public static IWebElement GetParent(IWebElement element, int number)
        {
            for (var i = 0; i < number; i++)
                element = element.FindElement(By.XPath("./parent::*"));

            return element;
        }
        

        public static void SelectBrowserTab(int indexTab)
        {
            GetDriver().SwitchTo().Window(GetDriver().WindowHandles[indexTab - 1]);
        }

        public static int GetIndexCurrentTab()
        {
            var currentTab = GetDriver().CurrentWindowHandle;
            var allTab = GetDriver().WindowHandles;
            var res = 1;

            for (var i = 0; i < allTab.Count; i++)
            {
                if (!allTab[i].Equals(currentTab)) continue;
                res = i;
                break;
            }

            return ++res;
        }

        public static void ClickOnElementByMouseClick(IWebElement element)
        {
            new Actions(GetDriver()).Click(element).Perform();
        }

        /// <summary>
        /// This method hover mouse over the element.
        /// </summary>
        /// <param name="elem">pass the element</param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        public static void HoverMouseOnElement(IWebElement elem, int offsetX = 50, int offsetY = 30)
        {
            var actions = new Actions(GetDriver());
            actions.MoveToElement(elem, offsetX, offsetY).Build().Perform();
        }

        /// <summary>
        /// This method first clamps the left mouse button and then moves to a specific point in the found element.
        /// </summary>
        /// <param name="element">The element on which the action will be carried out.</param>
        /// <param name="x">Starting point from where it will begin.</param>
        /// <param name="y">Starting point from where it will begin.</param>
        /// <param name="a">The end point where it will be finished.</param>
        /// <param name="b">The end point where it will be finished.</param>
        public static void MovePressedLeftMouseBtn(IWebElement element, int x, int y, int a, int b)
        {
            new Actions(GetDriver()).MoveToElement(element, x, y).ClickAndHold().MoveToElement(element, a, b).Release().Build().Perform();
        }

        /// <summary>
        /// This method scrolling to the up of page.
        /// </summary>
        public static void ScrollUp()
        {
            GetDriver().ExecuteJavaScript("window.scrollTo(0, 0)");
        }

        /// <summary>
        /// This method scrolling to the down of page.
        /// </summary>
        public static void ScrollDown()
        {
            GetDriver().ExecuteJavaScript("window.scrollTo(0, document.body.scrollHeight)");
        }

        /// <summary>
        /// This method scrolling to the down of element by locator.
        /// </summary>
        public static void ScrollDown(By locator)
        {
            var data = new Regex("By.*: (.*)").Match(locator.ToString()).Groups[1];
            GetDriver().ExecuteJavaScript($"document.querySelector('{data}').scrollTo(0, document.querySelector('{data}').scrollHeight)");
        }

        /// <summary>
        /// This method take horizontal scrolling to the element by locator.
        /// </summary>
        public static void HorizontalScrollToElement(By toElement, string scrollingElement = "document")
        {
            var toLocator = new Regex("By.*: (.*)").Match(toElement.ToString()).Groups[1];
            GetDriver().ExecuteJavaScript($"document.querySelector('{scrollingElement}').scrollTo(document.querySelector('{toLocator}').offsetLeft,0)");
        }

        /// <summary>
        /// This method take horizontal scrolling to the element.
        /// </summary>
        public static void OffsetHorizontalScroll(IWebElement element, string scrollingElement = "document")
        {
            GetDriver().ExecuteJavaScript($"document.querySelector('{scrollingElement}').scrollTo({element.Location.X},0)");
        }

        /// <summary>
        /// This method scroll in element by pixels.
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="offSetX"></param>
        /// <param name="offSetY"></param>
        public static void ScrollByPixels(By locator, int offSetX, int offSetY)
        {
            var toLocator = new Regex("By.*: (.*)").Match(locator.ToString()).Groups[1];
            GetDriver().ExecuteJavaScript($"document.querySelector('{toLocator}').scrollTo({offSetX},{offSetY})");
        }

        /// <summary>
        /// This method move to element if element is visible on DOM.
        /// </summary>
        public static void MoveToElement(IWebElement element)
        {
            var actions = new Actions(GetDriver());
            actions.MoveToElement(element);
            actions.Perform();
        }
        

        /// <summary>
        /// Switching to parent Frame 
        /// </summary>
        public static void SwitchToParentFrame()
        {
            GetDriver().SwitchTo().ParentFrame();
        }

        /// <summary>
        /// Switching to Frame by Web element.
        /// </summary>
        /// <param name="element"></param>
        public static void SwitchToFrame(IWebElement element)
        {
            WaitForSpinner();
            GetDriver().SwitchTo().Frame(element);
        }

        public static void SwitchToFrame(By locator) =>
            GetDriver().SwitchTo().Frame(GetDriver().FindElement(locator));


        /// <summary>
        /// Clear and send value to any field.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void ClearAndSendValue(IWebElement element, string value)
        {
            element.Clear();
            element.SendKeys(value);
        }

        public static void ClearAndSendValue(By locator, string text)
        {
            var element = GetDriver().FindElement(locator);
            element.Clear();
            element.SendKeys(text);
        }
        
        /// <summary>
        /// Select element from drop-down.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="elementName"></param>
        public static void SelectValueFromDropDown(IWebElement element, string elementName)
        {
            var isElementSelected = false;

            if (elementName.Equals("random", StringComparison.InvariantCultureIgnoreCase))
                elementName = GetRandomValuesFromDropDown(element);
            switch (element.TagName.ToLower())
            {
                case "select":
                    var selectElement = new SelectElement(element);
                    for (var i = 0; i < selectElement.Options.Count; i++)
                        if (selectElement.Options[i].Text.Contains(elementName))
                        {
                            selectElement.SelectByIndex(i);
                            isElementSelected = true;
                            break;
                        }
                    break;
                case "button":
                    element.Click();
                    foreach (var i in GetParent(element).FindElements(By.CssSelector("ul>li>a")))
                        if (i.Text.Contains(elementName) || i.GetAttribute("innerText").ToLower().Equals(elementName.ToLower()))
                        {
                            i.Click();
                            isElementSelected = true;
                            break;
                        }
                    break;
                case "div":
                    element.Click();
                    if (element.GetAttribute("class").Contains("container"))
                    {
                        FindElement(By.XPath($"//*[@id='defaultModal']//div[contains(@class,'option')]//span[text()='{elementName}']"))
                            .Click();
                        isElementSelected = true;
                    }
                    else
                    {
                        var dropDownList = TryFindElements(By.CssSelector("div.item"), out var dropDown)
                            ? dropDown
                            : FindElements(By.CssSelector(".xc-select_list .xc-select__item")).ToList();

                        foreach (var i in dropDownList.Where(i =>
                            i.Text.Contains(elementName) || i.GetAttribute("innerText").Contains(elementName)))
                        {
                            i.Click();
                            isElementSelected = true;
                            break;
                        }
                    }
                    break;
                case "input":
                    element.SendKeys(elementName);
                    element.SendKeys(Keys.Enter);
                    isElementSelected = true;
                    break;
                default:
                    throw new WebDriverException($"{element.TagName} does not exist inside an {element}");
            }

            if (!isElementSelected)
                throw new Exception($"Element with name '{elementName}' does not selected.");
        }

        /// <summary>
        /// Return all values from drop-down.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static List<string> GetValuesFromDropDown(IWebElement element)
        {
            var resValues = new List<string>();

            switch (element.TagName)
            {
                case "select":
                    resValues.AddRange(new SelectElement(element).Options.Select(i => i.Text));
                    break;
                case "button":
                    element.Click();
                    resValues.AddRange(GetParent(element).FindElements(By.CssSelector("ul>li>a")).Select(i => i.Text.IsNullOrEmpty() ? i.GetAttribute("value") : i.Text));
                    element.Click();
                    break;
                case "div":
                    if (element.GetAttribute("class").Contains("container"))
                    {
                        element.Click();
                        resValues.AddRange(FindElements(By.CssSelector("#defaultModal div[class$='option']")).Select(i => i.Text));
                        element.Click();
                    }
                    else
                        foreach (var i in element.FindElements(By.CssSelector("div.item")))
                            resValues.Add(i.Text);
                    break;
                default:
                    throw new WebDriverException($"{element.TagName} does not exist inside an {element}");
            }

            return resValues;
        }

        /// <summary>
        /// This method can be used only for drop down which have attribute select otherwise will be an error.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetSelectedValueFromDropDown(IWebElement element) =>
            new SelectElement(element).SelectedOption.Text;

        /// <summary>
        /// Return random value from drop-down.
        /// Save value to ScenarioDictionary.Current["random"].
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetRandomValuesFromDropDown(IWebElement element)
        {
            var randomElementValue = GetValuesFromDropDown(element)[new Random().Next(GetValuesFromDropDown(element).Count)];
            FeatureDictionary.Current["random"] = randomElementValue;
            return randomElementValue;
        }

        /// <summary>
        /// Clear input text.
        /// </summary>
        /// <param name="element">The IWebElement of the input text</param>
        public static void ClearInputText(IWebElement element)
        {
            element.SendKeys(Keys.End);

            while (element.GetAttribute("value").Length > 0)
            {
                element.SendKeys(Keys.Backspace);
                Thread.Sleep(10);
            }
        }

        public static void ClickOnElementUsingJs(By locator)
        {
            var regexLocator = new Regex("By.*: (.*)").Match(locator.ToString()).Groups[1];
            GetDriver().ExecuteJavaScript($"$('{regexLocator}').click()");
        }

        public static string GetHrefFromElement(By locator)
        {
            return FindElement(locator).GetAttribute("href");
        }

        public static string GetHrefFromElement(IWebElement element)
        {
            return element.GetAttribute("href");
        }

        public static bool ElementIsLink(By locator)
        {
            return FindElement(locator).GetAttribute("href") != null;
        }

        public static bool TryFindElementsInElement(IWebElement element, By locator, out List<IWebElement> webElements)
        {
            try
            {
                webElements = element.FindElements(locator).ToList();
                if (webElements.Count == 0)
                    throw new Exception("Result of found elements failure.");

                return true;
            }
            catch
            {
                webElements = null;
                return false;
            }
        }

        public static bool TryFindElementInElement(IWebElement element, By locator, out IWebElement webElement)
        {
            try
            {
                webElement = element.FindElement(locator);
                return true;
            }
            catch
            {
                webElement = null;
                return false;
            }
        }

        public static bool TryFindElements(By locator, out List<IWebElement> webElements, int waitTime = 25, bool waitPageLoad = true)
        {
            try
            {
                webElements = FindElements(locator, waitTime, waitPageLoad).ToList();
                return true;
            }
            catch
            {
                webElements = null;
                return false;
            }
        }

        public static bool TryFindElement(By locator, out IWebElement webElement, int waitTime = 25, bool waitPageLoad = true)
        {
            try
            {
                webElement = FindElement(locator, waitTime, waitPageLoad);
                return true;
            }
            catch
            {
                webElement = null;
                return false;
            }
        }

        /// <summary>
        /// Get known whether the specified page is open.
        /// </summary>
        /// <param name="pageName">As in url.</param>
        /// <returns></returns>
        public static bool IsOpenPage(string pageName)
        {
            return GetDriver().Url.Contains(pageName);
        }


        /// <summary>
        /// Get known current page url.
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUrl()
        {
            WaitForSpinner();
            return GetDriver().Url;
        }

        /// <summary>
        /// Close current page.
        /// </summary>
        /// <returns></returns>
        public static void CloseCurrentPage()
        {
            for (var tabs = GetDriver().WindowHandles; tabs.Count > 1; tabs = GetDriver().WindowHandles)
            {
                GetDriver().SwitchTo().Window(tabs[tabs.Count - 1]);
                GetDriver().Close();
            }
            GetDriver().SwitchTo().Window(GetDriver().WindowHandles[0]);
        }

        public static void GoToPreviousTab()
        {
            var tabs = GetDriver().WindowHandles;

            GetDriver().SwitchTo().Window(tabs[tabs.ToList().FindIndex(window => window.Equals(GetDriver().CurrentWindowHandle)) - 1]);

        }

        /// <summary>
        /// This method refresh page.
        /// </summary>
        public static void RefreshPage()
        {
            GetDriver().Navigate().Refresh();

            WaitForSpinner();
            WaitPageIsLoaded();
            WaitAjaxIsNotComplete();
            WaitLoaderIsDisabled();

        }

        public static void SwitchToLastBrowserTab()
        {
            GetDriver().SwitchTo().Window(GetDriver().WindowHandles.Last());

        }

        public static int GetCountOfBrowserTab()
        {
            return GetDriver().WindowHandles.Count;
        }

        public static void GoToUrl(string url)
        {
            GetDriver().Navigate().GoToUrl(url);
        }

        public static void OpenNewTab()
        {
            ((IJavaScriptExecutor)GetDriver()).ExecuteScript("window.open();");
            SwitchToLastBrowserTab();

        }

        public static bool IsTextVisibleOnPage(string text) =>
            IsElementVisible(By.XPath($"//*[contains(text(), '{text}')]"));
    }
}
