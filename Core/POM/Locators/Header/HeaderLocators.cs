using OpenQA.Selenium;

namespace Core.POM.Locators.Header
{
    public static class HeaderLocators
    {
        internal static readonly By SuperMarketLink = By.CssSelector("#categoryMenu > li:nth-child(1)> a");
    }
}
