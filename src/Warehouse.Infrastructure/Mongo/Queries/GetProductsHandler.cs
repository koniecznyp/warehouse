using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Application.Dto;
using Warehouse.Application.Queries;
using Warehouse.Application.Queries.Products;
using Warehouse.Infrastructure.Mongo.Documents;

namespace Warehouse.Infrastructure.Mongo.Queries
{
    public class GetProductsHandler : IQueryHandler<GetProducts, IEnumerable<ProductDto>>
    {
        private readonly IMongoDatabase database;

        public GetProductsHandler(IMongoDatabase database)
        {
            this.database = database;
        }

        public async Task<IEnumerable<ProductDto>> HandleAsync(GetProducts query)
        {
            var from = Builders<ProductDocument>.Filter.Gte("Price", query.PriceFrom ?? 0);
            var to = Builders<ProductDocument>.Filter.Lte("Price", query.PriceTo ?? decimal.MaxValue);

            var products = await database.GetCollection<ProductDocument>("products")
                .Find(from & to).ToListAsync();

            return products.Select(x => x.AsDto());
        }
    }
}
