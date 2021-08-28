using System;
using Core.POM.Locators.FeaturedProductsLocators;
using HelperProject;

namespace Core.POM.Methods.FeaturedProducts
{
    public static class FeaturedProducts
    {
        public static void FilterByPriceAsc()
        {
            SeleniumWrapper.SelectValueFromDropDown(SeleniumWrapper.FindElement(FeaturedProductsL.FilterDropDown),
                "מחיר: נמוך עד גבוה");
        }
        public static void ClickAddToCartBtn()
        {
            var button=SeleniumWrapper.FindElement(FeaturedProductsL.AddToCartBtn);
            if (button.Displayed)
            {
                button.Click();
            }
        }

    }
}
