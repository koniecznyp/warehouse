using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Warehouse.Infrastructure.Mongo.Documents
{
    internal sealed class ProductDocument
    {
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}