using FluentValidation;
using Warehouse.Application.Commands.Products;

namespace Warehouse.Infrastructure.Validators
{
    public class UpdateProductValidator : AbstractValidator<UpdateProduct>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0m);
        }
    }
}
