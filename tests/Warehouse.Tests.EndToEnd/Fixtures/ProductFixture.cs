using System;
using System.Threading.Tasks;
using Warehouse.Infrastructure.Mongo.Documents;

namespace Warehouse.Tests.EndToEnd.Fixtures
{
    public class ProductFixture : IDisposable
    {
        private readonly MongoFixture<ProductDocument, Guid> mongoFixture;

        public ProductFixture()
        {
            mongoFixture = new MongoFixture<ProductDocument, Guid>("products");
        }

        public async Task AddAsync(ProductDocument product)
            => await mongoFixture.AddAsync(product);

        public async Task<ProductDocument> GetAsync(Guid id)
            => await mongoFixture.GetAsync(id);

        public void Dispose()
        {
            mongoFixture.Dispose();
        }
    }
}
