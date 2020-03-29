using System;
using Warehouse.Core.Exceptions;

namespace Warehouse.Core.Entities
{
    public class Product : AggregateRoot
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product(Guid id, string name, decimal price)
        {
            Id = id;
            SetName(name);
            SetPrice(price);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidProductNameException();
            }
            Name = name;
        }

        public void SetPrice(decimal price)
        {
            if(price < 1)
            {
                throw new InvalidProductPriceException();
            }
            Price = price;
        }

        public static Product Create(Guid id, string name, decimal price)
            => new Product(id, name, price);
    }
}