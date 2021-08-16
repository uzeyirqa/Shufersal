using System;
using Core.POM.Locators.DiaryProductsFormLocators;
using HelperProject;
using OpenQA.Selenium;

namespace Core.POM.Methods.DairyProductsForm
{
    public static class DiaryProductsForm
    {
        public static void FindDairyBox() => 
            SeleniumWrapper.FindElement(DiaryProductsFormLocators.DiaryBox);

        public static void MilkLinkClick()
        {

        }
            
        
    }
}
