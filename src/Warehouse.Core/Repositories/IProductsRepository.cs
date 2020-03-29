using System.Threading.Tasks;
using Warehouse.Core.Entities;

namespace Warehouse.Core.Repositories
{
    public interface IProductsRepository
    {
        Task<Product> GetAsync(AggregateId id);
        Task<bool> ExistsAsync(AggregateId id);
        Task AddAsync(Product product);
        Task DeleteAsync(AggregateId id);
        Task UpdateAsync(Product product);
    }
}