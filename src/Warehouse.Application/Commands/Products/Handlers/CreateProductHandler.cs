using System.Threading.Tasks;
using Warehouse.Application.Exceptions;
using Warehouse.Core.Entities;
using Warehouse.Core.Repositories;

namespace Warehouse.Application.Commands.Products.Handlers
{
    public class CreateProductHandler : ICommandHandler<CreateProduct>
    {
        private readonly IProductsRepository _repository;

        public CreateProductHandler(IProductsRepository repository)
        {
            _repository = repository;
        }
        
        public async Task HandleAsync(CreateProduct command)
        {
            if(await _repository.ExistsAsync(command.ProductId))
            {
                throw new ProductAlreadyExistsException(command.ProductId);
            }
            var product = Product.Create(command.ProductId, command.Name, command.Price);
            await _repository.AddAsync(product);
        }
    }
}