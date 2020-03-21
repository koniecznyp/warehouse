using System;

namespace Warehouse.Infrastructure.Mongo.Documents
{
    internal sealed class ProductDocument
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}