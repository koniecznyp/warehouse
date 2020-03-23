using System;

namespace Warehouse.Application.Exceptions
{
    public class ProductNotFoundException : AppException
    {
        public override string Code => "product_not_found";
        public Guid Id { get; }
        
        public ProductNotFoundException(Guid id) : base($"Product with id {id} was not found")
        {
            Id = id;
        }
    }
}