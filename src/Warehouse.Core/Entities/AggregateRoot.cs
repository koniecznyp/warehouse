namespace Warehouse.Core.Entities
{
    public abstract class AggregateRoot
    {
        public AggregateId Id { get; protected set; }
        public int Version { get; protected set; }
    }
}