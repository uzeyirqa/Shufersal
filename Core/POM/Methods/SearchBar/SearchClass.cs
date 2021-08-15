using System;
using Core.POM.Locators.SearchBarLocator;
using HelperProject;
using OpenQA.Selenium.Interactions;

namespace Core.POM.Methods.SearchBar
{
    public static class SearchClass
    {
        public static void SetPrice()
        {
            var sliderA = SeleniumWrapper.FindElement(SearchBarLocator.PriceSliderMax);
            Actions move = new Actions(SeleniumWrapper.GetDriver());
            move.DragAndDropToOffset(sliderA, 100, 0);
            move.Build().Perform();
        }
    }
}
