using ECommerce.Common.Interfaces;
using ECommerce.Modules.Orders.Domain;
using Microsoft.Extensions.Logging;

namespace ECommerce.Modules.Orders.Services;

public class OrderService : IOrderService
{
  private readonly IRepository<Order> _orderRepository;
  private readonly ILogger<OrderService> _logger;

  public OrderService(IRepository<Order> orderRepository, ILogger<OrderService> logger)
  {
    _orderRepository = orderRepository;
    _logger = logger;
  }

  public async Task CreateOrderAsync(Guid customerId, List<OrderItem> items)
  {
    var order = new Order(customerId, items);
    await _orderRepository.AddAsync(order);
    await _orderRepository.SaveChangesAsync();
  }

  public async Task<Order> GetOrderByIdAsync(Guid orderId)
  {
    return await _orderRepository.GetByIdAsync(orderId);
  }

  public async Task<List<Order>> GetAllOrdersAsync()
  {
    var orders = await _orderRepository.GetAllAsync();
    _logger.LogInformation($"Number of orders to be returned: {orders.Count}");
    return orders;
  }
}
