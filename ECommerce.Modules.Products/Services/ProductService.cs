using ECommerce.Common.Interfaces;
using ECommerce.Modules.Products.Domain;
using Microsoft.Extensions.Logging;

namespace ECommerce.Modules.Products.Services;

public class ProductService : IProductService
{
  private readonly IRepository<Product> _productRepository;
  private readonly ILogger<ProductService> _logger;

  public ProductService(IRepository<Product> productRepository, ILogger<ProductService> logger)
  {
    _productRepository = productRepository;
    _logger = logger;
  }

  public async Task<Product> GetProductByIdAsync(Guid productId)
  {
    return await _productRepository.GetByIdAsync(productId);
  }

  public async Task<List<Product>> GetAllProductsAsync()
  {
    var products = await _productRepository.GetAllAsync();
    _logger.LogInformation($"Number of products to be returned: {products.Count}");
    return products;
  }

  public async Task AddProductAsync(Product product)
  {
    await _productRepository.AddAsync(product);
  }
}
