namespace Core.Interfaces
{
    public interface ISearch
    {
        /// <summary>
        /// This method searches for a specified product
        /// </summary>
        public void SearchForDairyProduct();
        
        /// <summary>
        /// This method adds some product to cart
        /// </summary>
        public void AddToCart();
    }
}