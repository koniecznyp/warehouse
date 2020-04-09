using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Warehouse.Api;
using Warehouse.Application.Dto;
using Warehouse.Infrastructure.Mongo.Documents;
using Warehouse.Tests.EndToEnd.Factories;
using Warehouse.Tests.EndToEnd.Fixtures;
using Xunit;

namespace Warehouse.Tests.EndToEnd.Products
{
    public class GetProductsTests : 
        IClassFixture<ProductsApplicationFactory<Program>>,
        IClassFixture<ProductFixture>
    {
        private readonly HttpClient httpClient;
        private ProductFixture fixture;

        public GetProductsTests(ProductFixture productFixture, ProductsApplicationFactory<Program> factory)
        {
            fixture = productFixture;
            factory.Server.AllowSynchronousIO = true;
            httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task GivenNonExistingProduct_WhenGetProduct_ThenReturnedNotFoundStatusCode()
        {
            var productId = Guid.NewGuid();

            var response = await httpClient.GetAsync($"products/{productId}");

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GivenExistingProduct_WhenGetProduct_ThenReturnedProductDto()
        {
            var productId = Guid.NewGuid();
            var name = "Test product";
            var price = 29.99m;
            await AddProductAsync(productId, name, price);

            var response = await httpClient.GetAsync($"products/{productId}");

            response.Should().NotBeNull();
            var content = await response.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<ProductDto>(content);
            dto.Id.Should().Be(productId);
            dto.Name.Should().Be(name);
            dto.Price.Should().Be(price);
        }

        private Task AddProductAsync(Guid id, string name, decimal price)
        {
            return fixture.AddAsync(new ProductDocument()
            {
                Id = id,
                Name = name,
                Price = price
            });
        }
    }
}
