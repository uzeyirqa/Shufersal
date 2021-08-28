using Core.POM.Locators;
using HelperProject;

namespace Core.POM.Methods.ModalWindow
{
    public static class ModalWindow
    {
        public static void ClickCloseBtnModalWindow()
        {
            SeleniumWrapper.WaitElementVisible(ModalWindowL.CloseModalWindow);
            SeleniumWrapper.FindElement(ModalWindowL.CloseModalWindow).Click();
        } 
    }
}