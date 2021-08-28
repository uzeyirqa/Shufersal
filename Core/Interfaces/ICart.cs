namespace Core.Interfaces
{
    public interface ICart
    {
        /// <summary>
        /// This method verifies the total price.
        /// </summary>
        public double VerifyCartPrice();

        public bool IsShippingIncluded();
    }
}