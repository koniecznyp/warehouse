using System.Threading.Tasks;

namespace Warehouse.Application.Commands
{
    public interface ICommandHandler<T> where T: class, ICommand
    {
        Task HandleAsync(T command);
    }
}