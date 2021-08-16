using Core.Interfaces;
using Core.POM.Methods.DairyProductsForm;
using Core.POM.Methods.Header;
using Core.POM.Methods.PopUp;
using Core.POM.Methods.SubMenu;
using HelperProject;

namespace Core.Implimentation.Search
{
    public class SearchUi : ISearch
    {
        public void SearchForDairyProduct(string product, string brand = null, int price = 0)
        {
            SeleniumWrapper.RefreshPage();
            Header.ClickSuperMarketLink();
            SeleniumWrapper.WaitPageIsLoaded();
            SubMenu.HoverMouseOnDairyLink();
          
        }

        public void AddToCart(string product)
        {
            throw new System.NotImplementedException();
        }
    }
}