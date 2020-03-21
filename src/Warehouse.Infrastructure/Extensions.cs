using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Warehouse.Core.Repositories;
using Warehouse.Infrastructure.Mongo;
using Warehouse.Infrastructure.Mongo.Repositories;

namespace Warehouse.Infrastructure
{
    public static class Extensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductsRepository, ProductsMongoRepository>();

            services.AddMongoDb(configuration);
        }

        private static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoSettings>(configuration.GetSection("mongo"));
            services.AddSingleton<MongoClient>(c =>
            {
                var options = c.GetService<IOptions<MongoSettings>>();
                return new MongoClient(options.Value.ConnectionString);
            });
            services.AddScoped<IMongoDatabase>(c => 
            {
                var options = c.GetService<IOptions<MongoSettings>>();
                var client = c.GetService<MongoClient>();
                return client.GetDatabase(options.Value.Database);
            });
        }
    }
}