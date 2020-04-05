using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Api;
using Warehouse.Application.Commands.Products;
using Warehouse.Infrastructure.Mongo.Documents;
using Warehouse.Tests.EndToEnd.Factories;
using Warehouse.Tests.EndToEnd.Fixtures;
using Xunit;

namespace Warehouse.Tests.EndToEnd.Products
{
    public class CreateProductTests : IDisposable, IClassFixture<ProductsApplicationFactory<Program>>
    {
        private readonly HttpClient httpClient;
        private readonly MongoFixture<ProductDocument, Guid> mongoFixture;

        public CreateProductTests(ProductsApplicationFactory<Program> factory)
        {
            mongoFixture = new MongoFixture<ProductDocument, Guid>("products");
            factory.Server.AllowSynchronousIO = true;
            httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task GivenValidProductsParameters_WhenCreateProductCommandSent_ThenReturnCreatedStatusCode()
        {
            var command = new CreateProduct(Guid.NewGuid(), "Name", 24.99m);

            var response = await httpClient.PostAsync("products", GetContent(command));

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task GivenValidProductsParameters_WhenCreateProductCommandSent_ThenReturnLocationHeaderWithProductId()
        {
            var command = new CreateProduct(Guid.NewGuid(), "Name", 24.99m);

            var response = await httpClient.PostAsync("products", GetContent(command));
            var header = response.Headers.FirstOrDefault(x => x.Key == "Location").Value.First();

            header.Should().NotBeNull();
            header.Should().Be($"products/{command.ProductId}");
        }

        [Fact]
        public async Task GivenValidProductsParameters_WhenCreateProductCommandSent_ThenCreatedDocumentStoredInDatabase()
        {
            var command = new CreateProduct(Guid.NewGuid(), "Name", 24.99m);

            await httpClient.PostAsync("products", GetContent(command));

            var document = await mongoFixture.GetAsync(command.ProductId);
            document.Should().NotBeNull();
            document.Id.Should().Be(command.ProductId);
            document.Name.Should().Be(command.Name);
            document.Price.Should().Be(command.Price);
        }

        public void Dispose()
        {
            mongoFixture.Dispose();
        }

        private static StringContent GetContent(object value)
            => new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
    }
}
