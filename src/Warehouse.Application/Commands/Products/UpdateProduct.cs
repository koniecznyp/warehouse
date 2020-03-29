using System;
using Newtonsoft.Json;

namespace Warehouse.Application.Commands.Products
{
    public class UpdateProduct : ICommand
    {
        public Guid ProductId { get; }
        public string Name { get; }
        public decimal Price { get; }

        [JsonConstructor]
        public UpdateProduct(Guid productId, string name, decimal price)
        {
            ProductId = productId == Guid.Empty ? Guid.NewGuid() : productId;
            Name = name;
            Price = price;
        }
    }
}