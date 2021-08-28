using TechTalk.SpecFlow;
using  Core.Implimentation.General;
using HelperProject;
using OpenQA.Selenium;
using Should;

namespace Core.Steps
{
    [Binding]
    public class ShoppingSteps
    {
        [Given(@"Shufersal website is open")]
        public void GivenShufersalWebsiteIsOpen() =>
            InterfacesOfShuf.HomePageUniversal.OpenShufersalHomePage();
        
        [When(@"I search for the cheapest milk")]
        public void WhenISearchForTheCheapestMilk()=>
           InterfacesOfShuf.SearchUniversal.SearchForDairyProduct();
        
        [When(@"add to the cart")]
        public void WhenAddTheMilkToTheCart()=>
           InterfacesOfShuf.SearchUniversal.AddToCart();

        [Then(@"Then the price of the milk with shipping cost is displayed in my cart")]
        public void ThenThenThePriceOfTheMilkWithShippingCostIsDisplayedInMyCart()
        {
            InterfacesOfShuf.CartUniversal.VerifyCartPrice().ShouldBeGreaterThanOrEqualTo(30);
            InterfacesOfShuf.CartUniversal.IsShippingIncluded().ShouldBeTrue();
        }
    }
}