using System;
using OpenQA.Selenium;

namespace Core.POM.Locators.FeaturedProductsLocators
{
    public static class FeaturedProductsL
    {
        internal static readonly By FilterDropDown =
            By.CssSelector("button[class='btn dropdown-toggle bs-placeholder btn-default']");
        
        internal static readonly By ProductsBox = By.CssSelector("[class='miglog-prod miglog-sellingmethod-by_unit']");

        internal static readonly By AddToCartBtn =
            By.CssSelector("button[class='btn js-add-to-cart js-enable-btn miglog-btn-add']");

        internal static readonly By AddQuantityBtn =
            By.CssSelector("button[class='btnTouchspin bootstrap-touchspin-up']");

        internal static readonly By RemoveQuantityBtn =
            By.CssSelector("button[class='btnTouchspin bootstrap-touchspin-down']");

        internal static readonly By Price = By.CssSelector("span[class='price']");

        internal static readonly By Description = By.CssSelector("div[class='text']");
        
        
    }
}
