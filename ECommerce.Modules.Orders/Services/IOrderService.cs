
using ECommerce.Common.Interfaces;
using ECommerce.Modules.Orders.Domain;

namespace ECommerce.Modules.Orders.Services;

public interface IOrderService
{
  Task CreateOrderAsync(Guid customerId, List<OrderItem> items);
  Task<Order> GetOrderByIdAsync(Guid orderId);
  Task<List<Order>> GetAllOrdersAsync();
}
