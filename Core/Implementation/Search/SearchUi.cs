using Core.Interfaces;
using Core.POM.Locators.DiaryProductsFormLocators;
using cart=Core.POM.Methods.Cart.Cart;
using Core.POM.Locators.FeaturedProductsLocators;
using Core.POM.Methods.DairyProductsForm;
using Core.POM.Methods.FeaturedProducts;
using Core.POM.Methods.Header;
using Core.POM.Methods.ModalWindow;
using Core.POM.Methods.PopUp;
using Core.POM.Methods.SubMenu;
using HelperProject;
using HelperProject.Logging;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;

namespace Core.Implimentation.Search
{
    public class SearchUi : ISearch
    {
        public void SearchForDairyProduct()
        {
            SeleniumWrapper.RefreshPage();
            Header.ClickSuperMarketLink();
            SeleniumWrapper.WaitPageIsLoaded();
            SubMenu.HoverMouseOnDairyLink();
            DiaryProductsForm.MilkLinkClick();
            FeaturedProducts.FilterByPriceAsc();
            var milk = new Milk();
            SeleniumWrapper.HoverMouseOnElement(milk.getCheapestMilk());
            Logger.Info("Search finished");
        }

        public void AddToCart()
        {
            FeaturedProducts.ClickAddToCartBtn();
            ModalWindow.ClickCloseBtnModalWindow();
            Logger.Info("Product added to cart");
        }
    }
}