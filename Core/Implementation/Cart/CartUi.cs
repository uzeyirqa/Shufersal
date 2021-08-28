using System;
using AngleSharp.Text;
using Core.Interfaces;
using _cart=Core.POM.Methods.Cart.Cart;
using Core.POM.Locators.Cart;
using HelperProject.Logging;

namespace Core.Implimentation.Cart
{
    public class CartUi : ICart
    {
        public double VerifyCartPrice()
        {
            double price= double.Parse(_cart.getTotalPrice());
            Logger.Info($"total price is {price}");
            return price;
        }

        public bool IsShippingIncluded()
        {
            if (_cart.getComment() == "כולל דמי משלוח/שרות")
                return true;
            return false;
        }
    }
}