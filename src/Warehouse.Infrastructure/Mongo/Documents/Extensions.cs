using Warehouse.Application.Dto;
using Warehouse.Core.Entities;

namespace Warehouse.Infrastructure.Mongo.Documents
{
    internal static class Extensions
    {
        public static ProductDocument AsDocument(this Product entity)
            => new ProductDocument
            {
                Id = entity.Id.Value,
                Name = entity.Name,
                Price = entity.Price
            };

        public static ProductDto AsDto(this ProductDocument document)
            => new ProductDto
            {
                Id = document.Id,
                Name = document.Name,
                Price = document.Price
            };

        public static Product AsEntity(this ProductDocument document)
            => new Product(document.Id, document.Name, document.Price);
    }
}