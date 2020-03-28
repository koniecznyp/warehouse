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
                Version = entity.Version,
                Name = entity.Name,
                Price = entity.Price
            };

        public static ProductDto AsDto(this ProductDocument entity)
            => new ProductDto
            {
                Name = entity.Name,
                Price = entity.Price
            };
    }
}