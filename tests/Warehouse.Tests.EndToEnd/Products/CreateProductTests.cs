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
using Warehouse.Tests.EndToEnd.Factories;
using Warehouse.Tests.EndToEnd.Fixtures;
using Xunit;

namespace Warehouse.Tests.EndToEnd.Products
{
    public class CreateProductTests :
        IClassFixture<ProductsApplicationFactory<Program>>,
        IClassFixture<ProductFixture>
    {
        private readonly HttpClient httpClient;
        private ProductFixture fixture;

        public CreateProductTests(ProductFixture productFixture, ProductsApplicationFactory<Program> factory)
        {
            fixture = productFixture;
            factory.Server.AllowSynchronousIO = true;
            httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task GivenValidProductParameters_WhenCreateProductCommandSent_ThenReturnCreatedStatusCode()
        {
            var command = new CreateProduct(Guid.NewGuid(), "Name", 24.99m);

            var response = await httpClient.PostAsync("products", GetContent(command));

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task GivenValidProductParameters_WhenCreateProductCommandSent_ThenReturnLocationHeaderWithProductId()
        {
            var command = new CreateProduct(Guid.NewGuid(), "Name", 24.99m);

            var response = await httpClient.PostAsync("products", GetContent(command));
            var header = response.Headers.FirstOrDefault(x => x.Key == "Location").Value.First();

            header.Should().NotBeNull();
            header.Should().Be($"products/{command.ProductId}");
        }

        [Fact]
        public async Task GivenValidProductParameters_WhenCreateProductCommandSent_ThenCreatedDocumentStoredInDatabase()
        {
            var command = new CreateProduct(Guid.NewGuid(), "Name", 24.99m);

            await httpClient.PostAsync("products", GetContent(command));

            var document = await fixture.GetAsync(command.ProductId);
            document.Should().NotBeNull();
            document.Id.Should().Be(command.ProductId);
            document.Name.Should().Be(command.Name);
            document.Price.Should().Be(command.Price);
        }

        private static StringContent GetContent(object value)
            => new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
    }
}
