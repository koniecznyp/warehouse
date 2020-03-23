using System.Threading.Tasks;
using Warehouse.Application.Exceptions;
using Warehouse.Core.Entities;
using Warehouse.Core.Repositories;

namespace Warehouse.Application.Commands.Products.Handlers
{
    public class DeleteProductHandler : ICommandHandler<DeleteProduct>
    {
        private readonly IProductsRepository _repository;

        public DeleteProductHandler(IProductsRepository repository)
        {
            _repository = repository;
        }
        
        public async Task HandleAsync(DeleteProduct command)
        {
            if(!await _repository.ExistsAsync(command.ProductId))
            {
                throw new ProductNotFoundException(command.ProductId);
            }

            await _repository.DeleteAsync(command.ProductId);
        }
    }
}