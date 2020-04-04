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
    public class DeleteProductHandlerTests
    {
        private readonly IProductsRepository repository;
        private readonly DeleteProductHandler handler;

        public DeleteProductHandlerTests()
        {
            repository = Substitute.For<IProductsRepository>();
            handler = new DeleteProductHandler(repository);
        }

        [Fact]
        public async Task GivenExistingProduct_WhenDeleteProductCommandSent_ThenProductDeleted()
        {
            var command = new DeleteProduct(Guid.NewGuid());
            repository.ExistsAsync(command.ProductId).Returns(true);

            await handler.HandleAsync(command);

            await repository.Received().DeleteAsync(Arg.Any<AggregateId>());
        }

        [Fact]
        public async Task GivenNonExistingProduct_WhenDeleteProductCommandSent_ThenExceptionThrown()
        {
            var command = new DeleteProduct(Guid.NewGuid());
            repository.ExistsAsync(command.ProductId).Returns(false);

            var exception = await Record.ExceptionAsync(async () => await handler.HandleAsync(command));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<ProductNotFoundException>();
        }
    }
}
