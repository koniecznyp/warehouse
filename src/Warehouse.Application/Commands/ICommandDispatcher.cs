using System.Threading.Tasks;

namespace Warehouse.Application.Commands
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T: class, ICommand; 
    }
}