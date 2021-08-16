using System;
using Core.POM.Locators.Header;
using HelperProject;
using OpenQA.Selenium;

namespace Core.POM.Methods.Header
{
    public static class Header
    {
        public static void ClickSuperMarketLink()
        {
            SeleniumWrapper.WaitElementClickable(HeaderLocators.SuperMarketLink);
            SeleniumWrapper.FindElement(HeaderLocators.SuperMarketLink).Click();
        } 
    }
}
