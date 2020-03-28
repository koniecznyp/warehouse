using System;
using Warehouse.Application.Dto;

namespace Warehouse.Application.Queries.Products
{
    public class GetProduct : IQuery<ProductDto>
    {
        public Guid ProductId { get; set; }

        public GetProduct(Guid id)
        {
            ProductId = id;
        }
    }
}
