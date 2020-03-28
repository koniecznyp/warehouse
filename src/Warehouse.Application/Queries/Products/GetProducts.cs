using System.Collections.Generic;
using Warehouse.Application.Dto;

namespace Warehouse.Application.Queries.Products
{
    public class GetProducts : IQuery<IEnumerable<ProductDto>>
    {
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
    }
}
