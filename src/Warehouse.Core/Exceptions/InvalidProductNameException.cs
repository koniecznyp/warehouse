namespace Warehouse.Core.Exceptions
{
    public class InvalidProductNameException : DomainException
    {
        public override string Code => "invalid_product_name";
        public InvalidProductNameException() 
            : base("Invalid product name")
        {
        }
    }
}