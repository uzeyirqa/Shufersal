using System.Collections.Generic;
using Core.POM.Locators.FeaturedProductsLocators;
using HelperProject;
using OpenQA.Selenium;

namespace Core.POM.Methods.FeaturedProducts
{
    /// <summary>
    /// This interface contains methods common for all product grids.
    /// </summary>
    /// <typeparam name="T">T is type of entity in the modal window.</typeparam>
    public abstract class Grid<T> 
        where T : class
    {

        /// <summary>
        /// This method selects item on the grid.
        /// </summary>
        /// <param name="itemModel">Model of the item to be selected.</param>
        /// <returns>Object of modal window.</returns>
        public abstract void SelectItem(T itemModel);

        /*/// <summary>
        /// This method gets information about all the items on the Grid.
        /// </summary>
        /// <returns></returns>
        public List<T> GetAllItemsInfo()
        {
            var window = typeof(ProductBox<T>);
            var listOfItems = new List<T>();

            foreach (var item in GetItemsAsElements())
            {
                item.Click();
                listOfItems.Add(window.GetEntity());
            }

            return listOfItems;
        }*/
        
        /// <summary>
        /// This method gets price of the product on product block.
        /// </summary>
        /// <returns></returns>
        public string GetProductPrice() =>
            SeleniumWrapper.FindElement(FeaturedProductsL.Price).Text;

        /// <summary>
        /// This method clicks on Add New Item button.
        /// </summary>
        /// <returns>Object of modal window.</returns>
        public void ClickAddNewItemBtn()
        {
            SeleniumWrapper.FindElement(FeaturedProductsL.AddQuantityBtn).Click();
        }

        /// <summary>
        /// This method clicks on add to cart button.
        /// </summary>
       
            

        public string GetProductDescription() =>
            SeleniumWrapper.FindElement(FeaturedProductsL.Description).Text;
        
        protected abstract List<IWebElement> GetItemsAsElements();
    }
}