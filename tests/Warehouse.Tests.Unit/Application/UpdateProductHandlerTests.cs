using FluentAssertions;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Warehouse.Application.Commands.Products;
using Warehouse.Application.Commands.Products.Handlers;
using Warehouse.Application.Exceptions;
using Warehouse.Core.Entities;
using Warehouse.Core.Repositories;
using Xunit;

namespace Warehouse.Tests.Unit.Application
{
    public class UpdateProductHandlerTests
    {
        private readonly IProductsRepository repository;
        private readonly UpdateProductHandler handler;

        public UpdateProductHandlerTests()
        {
            repository = Substitute.For<IProductsRepository>();
            handler = new UpdateProductHandler(repository);
        }

        [Fact]
        public async Task GivenExistingProduct_WhenUpdateProductCommandSent_ThenProductUpdated()
        {
            var product = Product.Create(Guid.NewGuid(), "Old name", 10.99m);
            var command = new UpdateProduct(product.Id, "New name", 34.99m);
            repository.GetAsync(command.ProductId).Returns(product);

            await handler.HandleAsync(command);

            await repository.Received().UpdateAsync(product);
        }

        [Fact]
        public async Task GivenNonExistingProduct_WhenUpdateProductCommandSent_ThenExceptionThrown()
        {
            var command = new UpdateProduct(Guid.NewGuid(), "New name", 34.99m);

            var exception = await Record.ExceptionAsync(async () => await handler.HandleAsync(command));
            exception.Should().NotBeNull();
            exception.Should().BeOfType<ProductNotFoundException>();
        }
    }
}
