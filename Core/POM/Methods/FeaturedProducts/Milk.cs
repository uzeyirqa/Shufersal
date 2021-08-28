using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Core.POM.Locators.FeaturedProductsLocators;
using Core.POM.Methods.DairyProductsForm;
using Core.POM.Models;
using HelperProject;
using HelpersProject.Helpers;
using OpenQA.Selenium;

namespace Core.POM.Methods.FeaturedProducts
{
    public class Milk : Grid<Products>
    {
        public override void SelectItem(Products itemModel=null)
        {
            var milkItems = GetItemsAsElements();

            foreach (var milk in milkItems)
                if (SeleniumWrapper.IsElementContainsElement(milk, FeaturedProductsL.Price))
                {
                    if (GetAllPrices().First() == getCheapestMilk())
                    {
                        SeleniumWrapper.HoverMouseOnElement(milk);
                        FeaturedProducts.ClickAddToCartBtn();  
                    }
                }

            throw new Exception("It is not possible to click on item ");
        }

        protected override List<IWebElement> GetItemsAsElements() =>
            SeleniumWrapper.FindElements(FeaturedProductsL.ProductsBox).ToList();

        public List<IWebElement> GetAllPrices() =>
            SeleniumWrapper.FindElements(FeaturedProductsL.Price)
                .Select(price =>  price).ToList();

        public IWebElement getCheapestMilk()
        {
           return GetItemsAsElements().First();
        }
    }
}