using FluentValidation;
using Warehouse.Application.Commands.Products;

namespace Warehouse.Infrastructure.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProduct>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0m);
        }
    }
}
