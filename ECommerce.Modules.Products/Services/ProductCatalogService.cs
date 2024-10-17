using ECommerce.Contracts.Interfaces;
using ECommerce.Contracts.DTOs;
using ECommerce.Modules.Products.Domain;

namespace ECommerce.Modules.Products.Services;

internal class ProductCatalogService : IProductCatalogService
{
    private readonly IProductService _productService;

    public ProductCatalogService(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<ProductDto?> GetProductByIdAsync(Guid productId)
    {
        var product = await _productService.GetProductByIdAsync(productId);
        return product == null ? null : new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };
    }
}
