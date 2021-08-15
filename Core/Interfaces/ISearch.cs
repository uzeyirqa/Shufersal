namespace Core.Interfaces
{
    public interface ISearch
    {
        /// <summary>
        /// This method searches for a specified product
        /// </summary>
        /// <param name="product"></param>
        /// <param name="brand"></param>
        /// <param name="price"></param>
        public void SearchForDairyProduct(string product, string brand = null, int price = 0);
        
        /// <summary>
        /// This method adds some product to cart
        /// </summary>
        /// <param name="product"></param>
        public void AddToCart(string product);
    }
}