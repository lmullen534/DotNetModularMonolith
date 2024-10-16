using ECommerce.Modules.Products.Domain;

namespace ECommerce.Modules.Products.Services;

public interface IProductService
{
  Task<Product> GetProductByIdAsync(Guid productId);
  Task<List<Product>> GetAllProductsAsync();
  Task AddProductAsync(Product product);
}
