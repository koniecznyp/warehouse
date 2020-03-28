using MongoDB.Driver;
using System.Threading.Tasks;
using Warehouse.Application.Dto;
using Warehouse.Application.Queries;
using Warehouse.Application.Queries.Products;
using Warehouse.Infrastructure.Mongo.Documents;

namespace Warehouse.Infrastructure.Mongo.Queries
{
    public class GetProductHandler : IQueryHandler<GetProduct, ProductDto>
    {
        private readonly IMongoDatabase database;

        public GetProductHandler(IMongoDatabase database)
        {
            this.database = database;
        }

        public async Task<ProductDto> HandleAsync(GetProduct query)
        {
            var product = await database.GetCollection<ProductDocument>("products")
                .Find(r => r.Id == query.ProductId)
                .SingleOrDefaultAsync();
            return product?.AsDto();
        }
    }
}
