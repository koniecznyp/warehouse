using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Warehouse.Infrastructure.Mongo.Documents
{
    public class ProductDocument : IIdentifiable<Guid>
    {
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}