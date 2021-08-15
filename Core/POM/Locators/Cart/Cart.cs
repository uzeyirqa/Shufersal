using System;
using OpenQA.Selenium;

namespace Core.POM.Locators.Cart
{
    public class Cart
    {
        internal static readonly By CartForm = By.XPath("//*[@id='cartContainer']");
        internal static readonly By Price = By.CssSelector("div[class='price']");
        internal static readonly By Comment = By.CssSelector("div[class='comment']");
    }
}
