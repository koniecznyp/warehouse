using System.Threading.Tasks;
using Warehouse.Core.Entities;

namespace Warehouse.Core.Repositories
{
    public interface IProductsRepository
    {
        Task<bool> ExistsAsync(AggregateId id);
        Task AddAsync(Product product);     
    }
}