using Core.POM.Locators.PopUp;
using HelperProject;
using HelperProject.Context;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Core.POM.Methods.PopUp
{
    public class PopUp
    {
        public static void ClickCloseBtnPopUp()
        {
          SeleniumWrapper.RefreshPage();
        }
    }
}