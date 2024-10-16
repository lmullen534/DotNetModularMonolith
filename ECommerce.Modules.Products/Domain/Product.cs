// ECommerce.Modules.Products/Domain/Product.cs
using ECommerce.Common.Domain;

namespace ECommerce.Modules.Products.Domain;

public class Product : Entity
{
  public string Name { get; private set; }
  public decimal Price { get; private set; }

  public Product(string name, decimal price)
  {
    Id = Guid.NewGuid();
    Name = name;
    Price = price;
  }
}
