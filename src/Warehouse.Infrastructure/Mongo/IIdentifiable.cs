namespace Warehouse.Infrastructure.Mongo
{
    public interface IIdentifiable<T>
    {
        T Id { get; }
    }
}
