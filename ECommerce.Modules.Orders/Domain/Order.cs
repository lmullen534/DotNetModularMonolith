using ECommerce.Common.Domain;

namespace ECommerce.Modules.Orders.Domain
{
  public class Order : Entity
  {
    public Guid CustomerId { get; private set; }
    public List<OrderItem> Items { get; private set; }

    public Order(Guid customerId, List<OrderItem> items)
    {
      Id = Guid.NewGuid();
      CustomerId = customerId;
      Items = items;
    }

    public decimal GetTotal() => Items.Sum(i => i.Price * i.Quantity);
  }

  public class OrderItem
  {
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }

    public OrderItem(Guid productId, int quantity, decimal price)
    {
      ProductId = productId;
      Quantity = quantity;
      Price = price;
    }
  }
}
