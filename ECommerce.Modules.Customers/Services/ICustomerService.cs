using ECommerce.Modules.Customers.Domain;

namespace ECommerce.Modules.Customers.Services;

public interface ICustomerService
{
  Task<Customer> GetCustomerByIdAsync(Guid customerId);
  Task<List<Customer>> GetAllCustomersAsync();
  Task AddCustomerAsync(Customer customer);
}
