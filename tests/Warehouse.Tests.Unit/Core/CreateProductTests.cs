using FluentAssertions;
using Warehouse.Core.Entities;
using Warehouse.Core.Exceptions;
using Xunit;

namespace Warehouse.Tests.Unit.Core
{
    public class CreateProductTests
    {
        [Fact]
        public void GivenEmptyProuctName_WhenCreateProduct_ThenExceptionThrown()
        {
            var id = new AggregateId();
            var name = string.Empty;
            var price = 25.99m;

            var exception = Record.Exception(() => Product.Create(id, name, price));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidProductNameException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5.99)]
        public void GivenWrongProductPrice_WhenCreateProduct_ThenExceptionThrown(decimal price)
        {
            var id = new AggregateId();
            var name = "Name";

            var exception = Record.Exception(() => Product.Create(id, name, price));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidProductPriceException>();
        }

        [Fact]
        public void GivenValidProductParameters_WhenCreateProduct_ThenProductCreated()
        {
            var id = new AggregateId();
            var name = "Name";
            var price = 25.99m;

            var product = Product.Create(id, name, price);

            product.Should().NotBeNull();
            product.Id.Should().Be(id);
            product.Name.Should().Be(name);
            product.Price.Should().Be(price);
        }
    }
}
