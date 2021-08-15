namespace Core.Interfaces
{
    public interface ICart
    {
        /// <summary>
        /// This method verifies product price.
        /// </summary>
        /// <param name="price"></param>
        public void VerifyCartPrice(int price=0);
    }
}