using ECommerce.Common.Interfaces;
using ECommerce.Contracts.Interfaces;
using ECommerce.Modules.Orders.Domain;
using ECommerce.Modules.Orders.Result;
using Microsoft.Extensions.Logging;

namespace ECommerce.Modules.Orders.Services;

internal class OrderService(IRepository<Order> orderRepository,
  ILogger<OrderService> logger,
  IProductCatalogService productCatalogService,
  ICustomerCatalogService customerCatalogService) : IOrderService
{
  private readonly IRepository<Order> _orderRepository = orderRepository;
  private readonly ILogger<OrderService> _logger = logger;
  private readonly IProductCatalogService _productCatalogService = productCatalogService;
  private readonly ICustomerCatalogService _customerCatalogService = customerCatalogService;

  public async Task<OrderResult> CreateOrderAsync(Guid customerId, List<OrderItem> items)
  {
    try
    {
      var products = await Task.WhenAll(items.Select(async item =>
      {
        var product = await _productCatalogService.GetProductByIdAsync(item.ProductId);
        return product ?? throw new Exception($"Product with id {item.ProductId} not found");
      }));

      var customer = await _customerCatalogService.GetCustomerByIdAsync(customerId) ?? 
        throw new Exception($"Customer with id {customerId} not found");
      
      var order = new Order(customerId, items);
      await _orderRepository.AddAsync(order);
      await _orderRepository.SaveChangesAsync();

      return new OrderResult
      {
        Success = true
      };
    } catch (Exception ex)
    {
      _logger.LogError(ex, "Error creating order");
      return new OrderResult
      {
        Success = false,
        Message = ex.Message
      };
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
