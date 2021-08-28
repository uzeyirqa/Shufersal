using System;
using OpenQA.Selenium;

namespace Core.POM.Locators.DiaryProductsFormLocators
{
    public static class DiaryProductsFormLocators
    {
        internal static readonly By DiaryBox = By.Id("[id='mCSB_3]");
        internal static readonly By MilkProductsLink = By.XPath("//*[@id='subTileCollapse_A01_7']/li[1]/a");
        internal static readonly By DownBtn = By.XPath("//*[@id='mCSB_5_scrollbar_vertical']/a[2]");
    }
}
