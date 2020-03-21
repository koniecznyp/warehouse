using System.Threading.Tasks;
using MongoDB.Driver;
using Warehouse.Core.Entities;
using Warehouse.Core.Repositories;
using Warehouse.Infrastructure.Mongo.Documents;

namespace Warehouse.Infrastructure.Mongo.Repositories
{
    public class ProductsMongoRepository : IProductsRepository
    {
        private readonly IMongoDatabase _repository;

        public ProductsMongoRepository(IMongoDatabase repository)
        {
            _repository = repository;
        }

        private IMongoCollection<ProductDocument> _products 
            => _repository.GetCollection<ProductDocument>("products");

        public async Task AddAsync(Product product)
            => await _products.InsertOneAsync(product.AsDocument());

        public async Task<bool> ExistsAsync(AggregateId id)
            => await _products.Find(x => x.Id == id).AnyAsync();
    }
}