using System;

namespace Warehouse.Application.Exceptions
{
    public class ProductAlreadyExistsException : AppException
    {
        public override string Code => "product_already_exists";
        public Guid Id { get; }
        
        public ProductAlreadyExistsException(Guid id) : base($"Product with id {id} already exists")
        {
            Id = id;
        }
    }
}