using System;
using HelperProject;
using Core.POM.Locators.SubMenuLocators;

namespace Core.POM.Methods.SubMenu
{
    public static class SubMenu
    {
        public static void HoverMouseOnDairyLink() => SeleniumWrapper.HoverMouseOnElement(SeleniumWrapper.FindElement(SubMenuLocators.MilkProductsLink));
    }
}
