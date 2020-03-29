using System.Threading.Tasks;
using Warehouse.Application.Exceptions;
using Warehouse.Core.Repositories;

namespace Warehouse.Application.Commands.Products.Handlers
{
    public class UpdateProductHandler : ICommandHandler<UpdateProduct>
    {
        private readonly IProductsRepository _repository;

        public UpdateProductHandler(IProductsRepository repository)
        {
            _repository = repository;
        }
        
        public async Task HandleAsync(UpdateProduct command)
        {
            var product = await _repository.GetAsync(command.ProductId);
            if (product is null)
            {
                throw new ProductNotFoundException(command.ProductId);
            }
            product.SetName(command.Name);
            product.SetPrice(command.Price);
            await _repository.UpdateAsync(product);
        }
    }
}