using TechTalk.SpecFlow;
using  Core.Implimentation.General;
using HelperProject;
using OpenQA.Selenium;

namespace Core.Steps
{
    [Binding]
    public class ShoppingSteps
    {
        [Given(@"Shufersal website is open")]
        public void GivenShufersalWebsiteIsOpen() =>
            InterfacesOfShuf.HomePageUniversal.OpenShufersalHomePage();
        
        [When(@"I search for the cheapest (.*)")]
        public void WhenISearchForTheCheapestMilk(string milk)=>
           InterfacesOfShuf.SearchUniversal.SearchForDairyProduct(milk);
        
        [When(@"add the (.*) to the cart")]
        public void WhenAddTheMilkToTheCart(string milk)=>
           InterfacesOfShuf.SearchUniversal.AddToCart(milk);

        [Then(@"Then the price of the milk with shipping cost is displayed in my cart")]
        public void ThenThenThePriceOfTheMilkWithShippingCostIsDisplayedInMyCart() =>
            InterfacesOfShuf.CartUniversal.VerifyCartPrice();
    }
}