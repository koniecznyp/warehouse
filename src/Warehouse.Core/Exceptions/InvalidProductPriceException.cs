namespace Warehouse.Core.Exceptions
{
    public class InvalidProductPriceException : DomainException
    {
        public override string Code => "invalid_product_price";
        public InvalidProductPriceException() 
            : base("Invalid product price")
        {
        }
    }
}