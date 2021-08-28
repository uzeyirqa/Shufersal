using System.Text.RegularExpressions;
using Core.POM.Locators;
using Core.POM.Locators.Cart;
using HelperProject;
using HelperProject.Logging;

namespace Core.POM.Methods.Cart
{
    public class Cart
    {
        public static string getTotalPrice()
        {
            string  priceInIls= SeleniumWrapper.FindElement(CartL.Price).GetAttribute("innerText");
            string priceDbl =  Regex.Match(priceInIls, @"\d+\.\d+").Value;
            return priceDbl;
        }

        public static string getComment() => SeleniumWrapper.FindElement(CartL.Comment).Text;

    }
}