using System;
using OpenQA.Selenium;

namespace Core.POM.Locators.DiaryProductsFormLocators
{
    public static class DiaryProductsFormLocators
    {
        internal static readonly By DiaryBox = By.CssSelector("# tileCollapse_A01_7");
        internal static readonly By MilkProductsLink = By.CssSelector("//*[@id='subTileCollapse_A01_7']/li[1]/a");
    }
}
