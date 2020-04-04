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
    public class CreateProductHandlerTests
    {
        private readonly IProductsRepository repository;
        private readonly CreateProductHandler handler;

        public CreateProductHandlerTests()
        {
            repository = Substitute.For<IProductsRepository>();
            handler = new CreateProductHandler(repository);
        }

        [Fact]
        public async Task GivenNonExistingProduct_WhenCreateProductCommandSent_ThenProductCreated()
        {
            var command = new CreateProduct(Guid.NewGuid(), "New name", 52.49m);
            repository.ExistsAsync(command.ProductId).Returns(false);

            await handler.HandleAsync(command);

            await repository.Received().AddAsync(Arg.Any<Product>());
        }

        [Fact]
        public async Task GivenExistingProduct_WhenCreateProductCommandSent_ThenExceptionThrown()
        {
            var command = new CreateProduct(Guid.NewGuid(), "New name", 52.49m);
            repository.ExistsAsync(command.ProductId).Returns(true);

            var exception = await Record.ExceptionAsync(async () => await handler.HandleAsync(command));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<ProductAlreadyExistsException>();
        }
    }
}
