using System;
using Core.Context;
using Core.Implimentation.Cart;
using Core.Implimentation.HomePage;
using Core.Implimentation.Search;
using Core.Interfaces;

namespace Core.Implimentation.General
{
    public static class InterfacesOfShuf
    {
        public static ICart CartUniversal { get; }
        public static ICart CartApi => new CartApi();
        public static ICart CartUi => new CartUi();
        public static IHomePage HomePageUniversal { get; }
        public static IHomePage HomePageApi => new HomePageApi();
        public static IHomePage HomePageUi => new HomePageUi();
        public static  ISearch SearchUniversal { get; }
        public static ISearch SearchApi => new SearchApi();
        public static ISearch SearchUi => new SearchUi();
        
        static InterfacesOfShuf()
        {

            if (GlobalConstants.ImplementationType.Equals("Api"))
            {
                CartUniversal = CartApi;
                HomePageUniversal = HomePageApi;
                SearchUniversal = SearchApi;
            }
            else if (GlobalConstants.ImplementationType.Equals("Gui"))
            {
                CartUniversal = CartUi;
                HomePageUniversal = HomePageUi;
                SearchUniversal = SearchUi;
            }
            else
                throw new ArgumentException(
                    $"ERROR!!! Unsupported implementation type - {GlobalConstants.ImplementationType}.");
        }
    }
}