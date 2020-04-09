using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using Warehouse.Infrastructure.Mongo;

namespace Warehouse.Tests.EndToEnd.Fixtures
{
    public class MongoFixture<TEntity, TKey> : IDisposable where TEntity: IIdentifiable<TKey>
    {
        private readonly IMongoClient client;
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<TEntity> collection;
        private readonly string databaseName;
        private readonly string collectionName;
        private bool disposed;

        public MongoFixture(string collectionName)
        {
            var options = OptionsExtensions.GetOptions<MongoOptions>("mongo");
            client = new MongoClient(options.ConnectionString);
            databaseName = options.Database;
            this.collectionName = collectionName;
            database = client.GetDatabase(databaseName);
            collection = database.GetCollection<TEntity>(collectionName);
            disposed = false;
        }

        public Task<TEntity> GetAsync(TKey id)
            => collection.Find(d => d.Id.Equals(id)).SingleOrDefaultAsync();

        public Task AddAsync(TEntity entity)
            => collection.InsertOneAsync(entity);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                client.DropDatabase(databaseName);
            }
            disposed = true;
        }
    }
}
