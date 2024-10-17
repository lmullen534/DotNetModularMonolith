using ECommerce.Contracts.DTOs;

namespace ECommerce.Contracts.Interfaces;

public interface IProductCatalogService
{
    Task<ProductDto?> GetProductByIdAsync(Guid id);
}
