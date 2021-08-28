using OpenQA.Selenium;

namespace Core.POM.Locators
{
    public class ModalWindowL
    {
        internal static readonly By CloseModalWindow =
            By.CssSelector("[id='assortmentModal'] button[class='btnClose']");
    }
}