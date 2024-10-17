using ECommerce.Common.Interfaces;
using ECommerce.Contracts.Interfaces;
using ECommerce.Modules.Orders.Domain;
using Microsoft.Extensions.Logging;

namespace ECommerce.Modules.Orders.Services;

public class OrderService : IOrderService
{
  private readonly IRepository<Order> _orderRepository;
  private readonly ILogger<OrderService> _logger;
  private readonly IProductCatalogService _productCatalogService;

  public OrderService(IRepository<Order> orderRepository, 
    ILogger<OrderService> logger,
    IProductCatalogService productCatalogService)
  {
    _orderRepository = orderRepository;
    _logger = logger;
    _productCatalogService = productCatalogService;
  }

  public async Task CreateOrderAsync(Guid customerId, List<OrderItem> items)
  {
    try
    {
      var products = await Task.WhenAll(items.Select(async item => 
      {
        var product = await _productCatalogService.GetProductByIdAsync(item.ProductId);
        return product ?? throw new Exception($"Product with id {item.ProductId} not found");
      }));

      var order = new Order(customerId, items);
      await _orderRepository.AddAsync(order);
      await _orderRepository.SaveChangesAsync();
    } catch (Exception ex)
    {
      _logger.LogError(ex, "Error creating order");
    }    
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
