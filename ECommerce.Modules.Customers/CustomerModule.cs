using ECommerce.Common.Infrastructure;
using ECommerce.Common.Interfaces;
using ECommerce.Contracts.Interfaces;
using ECommerce.Modules.Customers.Domain;
using ECommerce.Modules.Customers.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Modules.Customers;

public static class CustomerModule
{
    public static IServiceCollection AddCustomerModule(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddSingleton<ICustomerService, CustomerService>();
    services.AddSingleton<ICustomerCatalogService, CustomerCatalogService>();
    services.AddSingleton(typeof(IRepository<Customer>), typeof(InMemoryRepository<Customer>));
    return services;
  }
}
